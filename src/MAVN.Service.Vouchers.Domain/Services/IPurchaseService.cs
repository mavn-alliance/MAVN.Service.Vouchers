using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Services
{
    public interface IPurchaseService
    {
        Task<Voucher> BuyAsync(Guid customerId, Guid spendRuleId);

        Task ConfirmAsync(Guid transferId);
    }
}
