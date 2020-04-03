using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Falcon.Numerics;
using Lykke.Common.Log;
using Lykke.Service.Campaign.Client;
using Lykke.Service.Campaign.Client.Models.Enums;
using Lykke.Service.PartnerManagement.Client.Models;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.Domain.Exceptions;
using Lykke.Service.Vouchers.Domain.Repositories;
using Lykke.Service.Vouchers.Domain.Services;

namespace Lykke.Service.Vouchers.DomainServices
{
    public class VouchersService : IVouchersService
    {
        private readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);

        private readonly IVouchersRepository _vouchersRepository;
        private readonly ICampaignClient _campaignClient;
        private readonly ILog _log;

        public VouchersService(
            IVouchersRepository vouchersRepository,
            ICampaignClient campaignClient,
            ILogFactory logFactory)
        {
            _vouchersRepository = vouchersRepository;
            _campaignClient = campaignClient;
            _log = logFactory.CreateLog(this);
        }

        public Task<Voucher> GetByIdAsync(Guid voucherId)
        {
            return _vouchersRepository.GetByIdAsync(voucherId);
        }

        public Task<IReadOnlyList<Voucher>> GetBySpendRuleIdAsync(Guid spendRuleId)
        {
            return _vouchersRepository.GetBySpendRuleIdAsync(spendRuleId);
        }

        public Task<PaginatedVouchers> GetByCustomerIdAsync(Guid customerId, PageInfo pageInfo)
        {
            return _vouchersRepository.GetByCustomerIdAsync(
                customerId,
                (pageInfo.CurrentPage - 1) * pageInfo.PageSize,
                pageInfo.PageSize);
        }

        public async Task AddAsync(Guid spendRuleId, IReadOnlyList<string> codes)
        {
            var spendRule = await _campaignClient.BurnRules.GetByIdAsync(spendRuleId);

            if (spendRule.ErrorCode == CampaignServiceErrorCodes.EntityNotFound)
                throw new SpendRuleNotFoundException();

            if (spendRule.Vertical != Vertical.Retail)
                throw new InvalidSpendRuleVerticalException();

            var vouchers = codes
                .Select(o => new Voucher
                {
                    Id = Guid.NewGuid(), Code = o, Status = VoucherStatus.InStock, SpendRuleId = spendRuleId
                })
                .ToList();

            await _vouchersRepository.InsertAsync(vouchers);

            _log.Info("Vouchers added.", context: $"spendRuleId: {spendRuleId}, count: {codes.Count}");
        }

        public async Task<Voucher> ReserveAsync(Guid spendRuleId, Guid customerId, decimal amountInBaseCurrency,
            Money18 amountInTokens)
        {
            Voucher voucher;

            try
            {
                await _sync.WaitAsync();

                voucher = await _vouchersRepository.GetInStockAsync(spendRuleId);

                if (voucher == null)
                    throw new NoVouchersInStockException();

                voucher.Status = VoucherStatus.Reserved;
                voucher.CustomerVoucher = new CustomerVoucher
                {
                    CustomerId = customerId,
                    AmountInTokens = amountInTokens,
                    AmountInBaseCurrency = amountInBaseCurrency,
                    PurchaseDate = DateTime.UtcNow
                };

                await _vouchersRepository.UpdateAsync(voucher);

                _log.Info("Voucher reserved.", context: $"customerId: {customerId}; voucherId: {voucher.Id}");
            }
            finally
            {
                _sync.Release();
            }

            return voucher;
        }

        public async Task ReleaseAsync(Guid voucherId)
        {
            var voucher = await _vouchersRepository.GetByIdAsync(voucherId);

            if (voucher?.Status != VoucherStatus.Reserved)
                return;

            voucher.Status = VoucherStatus.InStock;
            voucher.CustomerVoucher = null;

            await _vouchersRepository.UpdateAsync(voucher);
        }

        public async Task SellAsync(Guid voucherId)
        {
            var voucher = await _vouchersRepository.GetByIdAsync(voucherId);

            if (voucher == null)
                throw new VoucherNotFoundException();

            if (voucher.Status != VoucherStatus.Reserved)
                throw new VoucherNotFoundException();

            voucher.Status = VoucherStatus.Sold;

            await _vouchersRepository.UpdateAsync(voucher);
        }
    }
}
