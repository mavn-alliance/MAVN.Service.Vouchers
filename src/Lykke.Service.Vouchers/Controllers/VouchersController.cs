using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Service.Vouchers.Client.Api;
using Lykke.Service.Vouchers.Client.Models;
using Lykke.Service.Vouchers.Client.Models.Vouchers;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.Domain.Exceptions;
using Lykke.Service.Vouchers.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.Vouchers.Controllers
{
    [ApiController]
    [Route("api/vouchers")]
    public class VouchersController : ControllerBase, IVouchersApi
    {
        private readonly IVouchersService _vouchersService;
        private readonly IPurchaseService _purchaseService;
        private readonly IMapper _mapper;

        public VouchersController(IVouchersService vouchersService, IPurchaseService purchaseService, IMapper mapper)
        {
            _vouchersService = vouchersService;
            _purchaseService = purchaseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns voucher by identifier.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <returns>
        /// 200 - The voucher.
        /// </returns>
        [HttpGet("{voucherId}")]
        [ProducesResponseType(typeof(VoucherModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<VoucherModel> GetByIdAsync(Guid voucherId)
        {
            var voucher = await _vouchersService.GetByIdAsync(voucherId);

            var model = _mapper.Map<VoucherModel>(voucher);

            return model;
        }

        /// <summary>
        /// Returns vouchers by spend rule identifier.
        /// </summary>
        /// <param name="spendRuleId">The spend rule identifier.</param>
        /// <returns>
        /// 200 - The collection of spend rule vouchers.
        /// </returns>
        [HttpGet("/api/spendRules/{spendRuleId}/vouchers")]
        [ProducesResponseType(typeof(VoucherModel[]), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<VoucherModel>> GetBySpendRuleIdAsync(Guid spendRuleId)
        {
            var vouchers = await _vouchersService.GetBySpendRuleIdAsync(spendRuleId);

            var model = _mapper.Map<List<VoucherModel>>(vouchers);

            return model;
        }

        /// <summary>
        /// Returns vouchers by customer identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="pagination">Pagination data.</param>
        /// <returns>
        /// 200 - The collection of customer vouchers.
        /// </returns>
        [HttpGet("/api/customers/{customerId}/vouchers")]
        [ProducesResponseType(typeof(PaginatedCustomerVouchersResponse), (int)HttpStatusCode.OK)]
        public async Task<PaginatedCustomerVouchersResponse> GetByCustomerIdAsync(Guid customerId, [FromQuery] PaginationModel pagination)
        {
            var vouchers = await _vouchersService.GetByCustomerIdAsync(customerId, _mapper.Map<PageInfo>(pagination));

            var model = _mapper.Map<PaginatedCustomerVouchersResponse>(vouchers);

            return model;
        }

        /// <summary>
        /// Creates vouchers.
        /// </summary>
        /// <param name="model">The vouchers creation information.</param>
        /// <remarks>
        /// Error codes:
        /// - **SpendRuleNotFound**
        /// - **InvalidSpendRuleVertical**
        /// - **CodeAlreadyExist**
        /// </remarks>
        /// <returns>
        /// 200 - The vouchers creation result.
        /// 404 - An error occurred while creation vouchers.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(VoucherCreateResultModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<VoucherCreateResultModel> AddAsync([FromBody] VoucherCreateModel model)
        {
            try
            {
                await _vouchersService.AddAsync(model.SpendRuleId, model.Codes);
            }
            catch (SpendRuleNotFoundException)
            {
                return new VoucherCreateResultModel(VoucherErrorCode.SpendRuleNotFound);
            }
            catch (InvalidSpendRuleVerticalException)
            {
                return new VoucherCreateResultModel(VoucherErrorCode.InvalidSpendRuleVertical);
            }
            catch (CodeAlreadyExistException)
            {
                return new VoucherCreateResultModel(VoucherErrorCode.CodeAlreadyExist);
            }

            return new VoucherCreateResultModel(VoucherErrorCode.None);
        }

        /// <summary>
        /// Creates vouchers.
        /// </summary>
        /// <param name="model">The vouchers creation information.</param>
        /// <remarks>
        /// Error codes:
        /// - **CustomerNotFound**
        /// - **SpendRuleNotFound**
        /// - **InvalidSpendRulePrice**
        /// - **InvalidSpendRuleVertical**
        /// - **NoEnoughTokens**
        /// - **InvalidConversion**
        /// - **CustomerWalletBlocked**
        /// - **CustomerWalletDoesNotExist**
        /// - **NoVouchersInStock**
        /// </remarks>
        /// <returns>
        /// 200 - The vouchers purchase result.
        /// 404 - An error occurred while purchasing vouchers.
        /// </returns>
        [HttpPost("/api/customers")]
        [ProducesResponseType(typeof(VoucherBuyResultModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<VoucherBuyResultModel> BuyAsync([FromBody] VoucherBuyModel model)
        {
            Voucher voucher;

            try
            {
                voucher = await _purchaseService.BuyAsync(model.CustomerId, model.SpendRuleId);
            }
            catch (CustomerNotFoundException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.CustomerNotFound);
            }
            catch (SpendRuleNotFoundException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.SpendRuleNotFound);
            }
            catch (InvalidSpendRulePriceException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.InvalidSpendRulePrice);
            }
            catch (InvalidSpendRuleVerticalException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.InvalidSpendRuleVertical);
            }
            catch (NoEnoughTokensException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.NoEnoughTokens);
            }
            catch (InvalidConversionException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.InvalidConversion);
            }
            catch (CustomerWalletBlockedException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.CustomerWalletBlocked);
            }
            catch (CustomerWalletDoesNotExistException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.CustomerWalletDoesNotExist);
            }
            catch (NoVouchersInStockException)
            {
                return new VoucherBuyResultModel(VoucherErrorCode.NoVouchersInStock);
            }

            return new VoucherBuyResultModel(voucher.Id, voucher.Code);
        }
    }
}
