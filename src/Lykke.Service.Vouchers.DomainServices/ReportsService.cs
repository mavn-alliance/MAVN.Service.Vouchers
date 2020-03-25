using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.Domain.Repositories;
using Lykke.Service.Vouchers.Domain.Services;

namespace Lykke.Service.Vouchers.DomainServices
{
    public class ReportsService : IReportsService
    {
        private readonly IVouchersRepository _vouchersRepository;

        public ReportsService(IVouchersRepository vouchersRepository)
        {
            _vouchersRepository = vouchersRepository;
        }

        public async Task<SpendRuleVouchersReport> GetSpendRuleVouchersAsync(Guid spendRuleId)
        {
            var totalVouchersTask = _vouchersRepository.GetTotalAsync(spendRuleId);
            var totalVouchersInStockTask =
                _vouchersRepository.GetTotalByStatusAsync(spendRuleId, VoucherStatus.InStock);

            await Task.WhenAll(totalVouchersTask, totalVouchersInStockTask);

            return new SpendRuleVouchersReport
            {
                Total = totalVouchersTask.Result, InStock = totalVouchersInStockTask.Result
            };
        }
    }
}
