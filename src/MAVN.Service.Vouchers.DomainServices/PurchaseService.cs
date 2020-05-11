using System;
using System.Threading.Tasks;
using Common;
using Common.Log;
using MAVN.Numerics;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.Campaign.Client;
using MAVN.Service.Campaign.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.EligibilityEngine.Client;
using MAVN.Service.EligibilityEngine.Client.Enums;
using MAVN.Service.EligibilityEngine.Client.Models.ConversionRate.Requests;
using MAVN.Service.PartnerManagement.Client.Models;
using MAVN.Service.PrivateBlockchainFacade.Client;
using MAVN.Service.Vouchers.Contract;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Exceptions;
using MAVN.Service.Vouchers.Domain.Services;
using MAVN.Service.WalletManagement.Client;
using MAVN.Service.WalletManagement.Client.Enums;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ITransfersService _transfersService;
        private readonly ISettingsService _settingsService;
        private readonly IVouchersService _vouchersService;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly IWalletManagementClient _walletManagementClient;
        private readonly ICampaignClient _campaignClient;
        private readonly IEligibilityEngineClient _eligibilityEngineClient;
        private readonly IPrivateBlockchainFacadeClient _privateBlockchainFacadeClient;
        private readonly IRabbitPublisher<VoucherTokensReservedEvent> _voucherTokensReservedEventPublisher;
        private readonly ILog _log;

        public PurchaseService(
            ITransfersService transfersService,
            ISettingsService settingsService,
            IVouchersService vouchersService,
            ICustomerProfileClient customerProfileClient,
            IWalletManagementClient walletManagementClient,
            ICampaignClient campaignClient,
            IEligibilityEngineClient eligibilityEngineClient,
            IPrivateBlockchainFacadeClient privateBlockchainFacadeClient,
            IRabbitPublisher<VoucherTokensReservedEvent> voucherTokensReservedEventPublisher,
            ILogFactory logFactory)
        {
            _transfersService = transfersService;
            _settingsService = settingsService;
            _vouchersService = vouchersService;
            _customerProfileClient = customerProfileClient;
            _walletManagementClient = walletManagementClient;
            _campaignClient = campaignClient;
            _eligibilityEngineClient = eligibilityEngineClient;
            _privateBlockchainFacadeClient = privateBlockchainFacadeClient;
            _voucherTokensReservedEventPublisher = voucherTokensReservedEventPublisher;
            _log = logFactory.CreateLog(this);
        }

        public async Task<Voucher> BuyAsync(Guid customerId, Guid spendRuleId)
        {
            var customerProfileResponse =
                await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(customerId.ToString());

            if (customerProfileResponse?.Profile == null)
                throw new CustomerNotFoundException();

            var customerWalletStatusResponse =
                await _walletManagementClient.Api.GetCustomerWalletBlockStateAsync(customerId.ToString());

            if (customerWalletStatusResponse.Status == CustomerWalletActivityStatus.Blocked)
                throw new CustomerWalletBlockedException();

            var spendRuleResult = await _campaignClient.BurnRules.GetByIdAsync(spendRuleId);

            if (spendRuleResult.ErrorCode == CampaignServiceErrorCodes.EntityNotFound)
                throw new SpendRuleNotFoundException();

            if (spendRuleResult.Vertical != Vertical.Retail)
                throw new InvalidSpendRuleVerticalException();

            if (!spendRuleResult.Price.HasValue)
                throw new InvalidSpendRulePriceException();

            var amount = await ConvertVoucherPriceToTokensAsync(customerId, spendRuleId, spendRuleResult.Price.Value);

            var customerBalanceResponse = await _privateBlockchainFacadeClient.CustomersApi.GetBalanceAsync(customerId);

            if (customerBalanceResponse.Total - customerBalanceResponse.Staked < amount)
                throw new NoEnoughTokensException();

            var voucher =
                await _vouchersService.ReserveAsync(spendRuleId, customerId, spendRuleResult.Price.Value, amount);

            try
            {
                await _transfersService.CreateAsync(customerId, spendRuleId, voucher.Id, amount);
            }
            catch
            {
                await _vouchersService.ReleaseAsync(voucher.Id);

                throw;
            }

            return voucher;
        }

        public async Task ConfirmAsync(Guid transferId)
        {
            var transfer = await _transfersService.GetByIdAsync(transferId);

            if (transfer == null)
                throw new TransferNotFoundException();

            var evt = new VoucherTokensReservedEvent
            {
                TransferId = transferId,
                CustomerId = transfer.CustomerId,
                SpendRuleId = transfer.SpendRuleId,
                Amount = transfer.Amount,
                Timestamp = transfer.Created,
                VoucherId = transfer.VoucherId,
            };
            await _voucherTokensReservedEventPublisher.PublishAsync(evt);

            await _vouchersService.SellAsync(transfer.VoucherId);

            await _transfersService.CompleteAsync(transferId);
        }

        private async Task<Money18> ConvertVoucherPriceToTokensAsync(Guid customerId, Guid spendRuleId, decimal price)
        {
            var conversionRequest = new ConvertAmountBySpendRuleRequest
            {
                CustomerId = customerId,
                Amount = price,
                FromCurrency = _settingsService.GetBaseCurrencyCode(),
                ToCurrency = _settingsService.GetTokenCurrencyCode(),
                SpendRuleId = spendRuleId
            };

            var conversionResponse =
                await _eligibilityEngineClient.ConversionRate.GetAmountBySpendRuleAsync(conversionRequest);

            if (conversionResponse.ErrorCode != EligibilityEngineErrors.None)
            {
                _log.Warning("An error occurred while converting amount.",
                    context: $"request: {conversionRequest.ToJson()}; error: {conversionResponse.ErrorCode}");

                throw new InvalidConversionException();
            }

            return conversionResponse.Amount;
        }
    }
}
