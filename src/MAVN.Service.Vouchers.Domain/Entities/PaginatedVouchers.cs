using System.Collections.Generic;

namespace MAVN.Service.Vouchers.Domain.Entities
{
    public class PaginatedVouchers
    {
        public IReadOnlyList<Voucher> Vouchers { get; set; }

        public int TotalCount { get; set; }
    }
}
