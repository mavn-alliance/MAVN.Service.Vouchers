using System;
using JetBrains.Annotations;

namespace MAVN.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents information of customer buy request.
    /// </summary>
    [PublicAPI]
    public class VoucherBuyModel
    {
        /// <summary>
        /// The customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The spend rule identifier.
        /// </summary>
        public Guid SpendRuleId { get; set; }
    }
}
