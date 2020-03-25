using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Reports
{
    /// <summary>
    /// Represents report of sold spend rule vouchers.
    /// </summary>
    [PublicAPI]
    public class SpendRuleVouchersReportModel
    {
        /// <summary>
        /// The total number of vouchers.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The number of vouchers in stock.
        /// </summary>
        public int InStock { get; set; }
    }
}
