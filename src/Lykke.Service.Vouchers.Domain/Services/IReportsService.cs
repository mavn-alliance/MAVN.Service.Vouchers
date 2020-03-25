using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface IReportsService
    {
        Task<SpendRuleVouchersReport> GetSpendRuleVouchersAsync(Guid spendRuleId);
    }
}
