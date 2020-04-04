using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.Vouchers.Client.Models.Reports;
using Refit;

namespace MAVN.Service.Vouchers.Client.Api
{
    /// <summary>
    /// The operations contract for reports.
    /// </summary>
    [PublicAPI]
    public interface IReportsApi
    {
        /// <summary>
        /// Returns report of sold vouchers.
        /// </summary>
        /// <param name="spendRuleId">The spend rule identifier.</param>
        /// <returns>The report of sold vouchers.</returns>
        [Get("/api/reports/spendRuleVouchers")]
        Task<SpendRuleVouchersReportModel> GetSpendRuleVouchersAsync(Guid spendRuleId);
    }
}
