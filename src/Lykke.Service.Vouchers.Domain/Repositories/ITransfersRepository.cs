using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Repositories
{
    public interface ITransfersRepository
    {
        Task<Transfer> GetByIdAsync(Guid transferId);

        Task InsertAsync(Transfer transfer);

        Task UpdateAsync(Transfer transfer);
    }
}
