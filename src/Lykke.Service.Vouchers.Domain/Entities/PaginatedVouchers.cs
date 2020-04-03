using System.Collections.Generic;

namespace Lykke.Service.Vouchers.Domain.Entities
{
    public class PaginatedVouchers
    {
        public IReadOnlyList<Voucher> Vouchers { get; set; }

        public int TotalCount { get; set; }
    }
}
