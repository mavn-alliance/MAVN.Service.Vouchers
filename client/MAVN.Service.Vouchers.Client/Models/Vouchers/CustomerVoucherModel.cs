using System;
using MAVN.Numerics;
using JetBrains.Annotations;

namespace MAVN.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents a customer voucher.
    /// </summary>
    [PublicAPI]
    public class CustomerVoucherModel
    {
        /// <summary>
        /// The voucher identifier.
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
        /// The amount in tokens that was spent while buying the voucher.
        /// </summary>
        public Money18 AmountInTokens { get; set; }

        /// <summary>
        /// The amount in base currency that was spent while buying the voucher.
        /// </summary>
        public decimal AmountInBaseCurrency { get; set; }

        /// <summary>
        /// The date and time of purchase.
        /// </summary>
        public DateTime PurchaseDate { get; set; }
    }
}
