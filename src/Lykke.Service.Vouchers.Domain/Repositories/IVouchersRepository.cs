using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Repositories
{
    public interface IVouchersRepository
    {
        Task<Voucher> GetByIdAsync(Guid voucherId);

        Task<Voucher> GetInStockAsync(Guid spendRuleId);

        Task<IReadOnlyList<Voucher>> GetBySpendRuleIdAsync(Guid spendRuleId);

        Task<PaginatedVouchers> GetByCustomerIdAsync(
            Guid customerId,
            int skip,
            int take);

        Task<int> GetTotalAsync(Guid spendRuleId);

        Task<int> GetTotalByStatusAsync(Guid spendRuleId, VoucherStatus status);

        Task InsertAsync(IReadOnlyList<Voucher> vouchers);

        Task UpdateAsync(Voucher voucher);
    }
}
