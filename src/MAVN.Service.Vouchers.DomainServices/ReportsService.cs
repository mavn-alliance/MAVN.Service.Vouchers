using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Repositories;
using MAVN.Service.Vouchers.Domain.Services;

namespace MAVN.Service.Vouchers.DomainServices
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
