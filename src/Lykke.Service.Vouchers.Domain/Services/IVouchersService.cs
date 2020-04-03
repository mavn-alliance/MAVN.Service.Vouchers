using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Falcon.Numerics;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface IVouchersService
    {
        Task<Voucher> GetByIdAsync(Guid voucherId);

        Task<IReadOnlyList<Voucher>> GetBySpendRuleIdAsync(Guid spendRuleId);

        Task<PaginatedVouchers> GetByCustomerIdAsync(Guid customerId, PageInfo pageInfo);

        Task AddAsync(Guid spendRuleId, IReadOnlyList<string> codes);

        Task<Voucher> ReserveAsync(
            Guid spendRuleId,
            Guid customerId,
            decimal amountInBaseCurrency,
            Money18 amountInTokens);

        Task ReleaseAsync(Guid voucherId);
        
        Task SellAsync(Guid voucherId);
    }
}
