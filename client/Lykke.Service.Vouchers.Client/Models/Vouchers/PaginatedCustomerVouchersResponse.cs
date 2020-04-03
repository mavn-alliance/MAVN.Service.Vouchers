using System.Collections.Generic;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Paginated response for customer vouchers
    /// </summary>
    public class PaginatedCustomerVouchersResponse
    {
        /// <summary>Customer vouchers</summary>
        public IReadOnlyList<CustomerVoucherModel> Vouchers { get; set; }

        /// <summary>Total count of the items</summary>
        public int TotalCount { get; set; }
    }
}
