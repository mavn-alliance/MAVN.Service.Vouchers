using System;
using Falcon.Numerics;

namespace Lykke.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Represents transfer of voucher purchase. 
    /// </summary>
    public class Transfer
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The spend rule identifier.
        /// </summary>
        public Guid SpendRuleId { get; set; }

        /// <summary>
        /// The voucher identifier.
        /// </summary>
        public Guid VoucherId { get; set; }

        /// <summary>
        /// The amount of tokens.
        /// </summary>
        public Money18 Amount { get; set; }

        /// <summary>
        /// Indicates status. 
        /// </summary>
        public TransferStatus Status { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
