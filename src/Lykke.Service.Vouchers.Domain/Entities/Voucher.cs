using System;

namespace Lykke.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Represents a voucher.
    /// </summary>
    public class Voucher
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

        /// <summary>
        /// The customer that reserved or bought voucher.
        /// </summary>
        public CustomerVoucher CustomerVoucher { get; set; }
    }
}
