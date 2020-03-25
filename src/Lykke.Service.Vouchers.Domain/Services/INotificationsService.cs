using System;
using System.Threading.Tasks;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface INotificationsService
    {
        Task SendVoucherSoldAsync(Guid voucherId);
    }
}
