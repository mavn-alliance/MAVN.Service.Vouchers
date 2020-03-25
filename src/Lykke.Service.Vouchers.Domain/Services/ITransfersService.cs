using System;
using System.Threading.Tasks;
using Falcon.Numerics;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface ITransfersService
    {
        Task<Transfer> GetByIdAsync(Guid transferId);

        Task CreateAsync(Guid customerId, Guid spendRuleId, Guid voucherId, Money18 amount);

        Task CompleteAsync(Guid transferId);
    }
}
