using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface IPurchaseService
    {
        Task<Voucher> BuyAsync(Guid customerId, Guid spendRuleId);

        Task ConfirmAsync(Guid transferId);
    }
}
