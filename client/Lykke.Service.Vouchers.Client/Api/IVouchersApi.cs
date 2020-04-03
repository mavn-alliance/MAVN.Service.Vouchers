using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.Vouchers.Client.Models;
using Lykke.Service.Vouchers.Client.Models.Vouchers;
using Refit;

namespace Lykke.Service.Vouchers.Client.Api
{
    /// <summary>
    /// The operations contract for vouchers.
    /// </summary>
    [PublicAPI]
    public interface IVouchersApi
    {
        /// <summary>
        /// Returns voucher by identifier.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <returns>The voucher.</returns>
        [Get("/api/vouchers/{voucherId}")]
        Task<VoucherModel> GetByIdAsync(Guid voucherId);

        /// <summary>
        /// Returns vouchers by spend rule identifier.
        /// </summary>
        /// <param name="spendRuleId">The spend rule identifier.</param>
        /// <returns>The collection of spend rule vouchers.</returns>
        [Get("/api/spendRules/{spendRuleId}/vouchers")]
        Task<IReadOnlyList<VoucherModel>> GetBySpendRuleIdAsync(Guid spendRuleId);

        /// <summary>
        /// Returns vouchers by customer identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="pagination">Pagination data.</param>
        /// <returns>The collection of customer vouchers.</returns>
        [Get("/api/customers/{customerId}/vouchers")]
        Task<PaginatedCustomerVouchersResponse> GetByCustomerIdAsync(Guid customerId, [Query] PaginationModel pagination);

        /// <summary>
        /// Creates vouchers.
        /// </summary>
        /// <param name="model">The vouchers creation information.</param>
        /// <returns>The vouchers creation result.</returns>
        [Post("/api/vouchers")]
        Task<VoucherCreateResultModel> AddAsync([Body] VoucherCreateModel model);
        
        /// <summary>
        /// Buys a voucher for customer.
        /// </summary>
        /// <param name="model">The voucher purchase information.</param>
        /// <returns>The vouchers purchase result.</returns>
        [Post("/api/customers")]
        Task<VoucherBuyResultModel> BuyAsync([Body] VoucherBuyModel model);
    }
}
