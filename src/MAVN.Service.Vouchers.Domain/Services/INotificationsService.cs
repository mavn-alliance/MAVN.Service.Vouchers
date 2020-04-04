using System;
using System.Threading.Tasks;

namespace MAVN.Service.Vouchers.Domain.Services
{
    public interface INotificationsService
    {
        Task SendVoucherSoldAsync(Guid voucherId);
    }
}
