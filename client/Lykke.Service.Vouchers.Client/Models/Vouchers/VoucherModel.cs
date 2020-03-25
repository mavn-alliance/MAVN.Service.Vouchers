using System;
using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents a voucher.
    /// </summary>
    [PublicAPI]
    public class VoucherModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The voucher unique code that used to display.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Indicated the voucher status.
        /// </summary>
        public VoucherStatus Status { get; set; }

        /// <summary>
        /// The spend rule identifier that associated with voucher.
        /// </summary>
        public Guid SpendRuleId { get; set; }
    }
}
