using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Services
{
    public interface IReportsService
    {
        Task<SpendRuleVouchersReport> GetSpendRuleVouchersAsync(Guid spendRuleId);
    }
}
