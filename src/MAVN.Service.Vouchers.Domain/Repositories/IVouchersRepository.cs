using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Repositories
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
