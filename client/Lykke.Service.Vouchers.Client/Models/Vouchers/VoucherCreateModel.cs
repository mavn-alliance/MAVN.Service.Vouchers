using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents vouchers creation information.
    /// </summary>
    [PublicAPI]
    public class VoucherCreateModel
    {
        /// <summary>
        /// The spend rule identifier.
        /// </summary>
        public Guid SpendRuleId { get; set; }

        /// <summary>
        /// The collection of unique codes of vouchers.
        /// </summary>
        public IReadOnlyList<string> Codes { get; set; }
    }
}
