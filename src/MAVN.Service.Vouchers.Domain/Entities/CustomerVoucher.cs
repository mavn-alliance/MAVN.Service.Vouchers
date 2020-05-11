using System;
using MAVN.Numerics;

namespace MAVN.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Represents information about customer voucher.
    /// </summary>
    public class CustomerVoucher
    {
        /// <summary>
        /// The customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

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
