using System;
using Falcon.Numerics;

namespace MAVN.Service.Vouchers.Contract
{
    /// <summary>
    /// Represents an event that raised when tokens for the voucher purchase are used in blockchain.
    /// </summary>
    public class VoucherTokensUsedEvent
    {
        /// <summary>The payment transfer identifier.</summary>
        public Guid TransferId { get; set; }

        /// <summary>The customer identifier.</summary>
        public Guid CustomerId { get; set; }

        /// <summary>The spend rule identifier.</summary>
        public Guid SpendRuleId { get; set; }

        /// <summary>The sold voucher identifier.</summary>
        public Guid VoucherId { get; set; }

        /// <summary>The amount of tokens paid.</summary>
        public Money18 Amount { get; set; }

        /// <summary>The date and time of transfer.</summary>
        public DateTime Timestamp { get; set; }
    }
}
