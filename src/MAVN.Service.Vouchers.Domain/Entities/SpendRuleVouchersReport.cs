namespace MAVN.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Represents report of sold spend rule vouchers.
    /// </summary>
    public class SpendRuleVouchersReport
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
