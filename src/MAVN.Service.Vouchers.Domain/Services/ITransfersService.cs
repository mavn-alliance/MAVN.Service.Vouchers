using System;
using System.Threading.Tasks;
using MAVN.Numerics;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Services
{
    public interface ITransfersService
    {
        Task<Transfer> GetByIdAsync(Guid transferId);

        Task CreateAsync(Guid customerId, Guid spendRuleId, Guid voucherId, Money18 amount);

        Task CompleteAsync(Guid transferId);
    }
}
