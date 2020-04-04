using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Repositories
{
    public interface ITransfersRepository
    {
        Task<Transfer> GetByIdAsync(Guid transferId);

        Task InsertAsync(Transfer transfer);

        Task UpdateAsync(Transfer transfer);
    }
}
