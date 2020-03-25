using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Repositories
{
    public interface IOperationsRepository
    {
        Task<Operation> GetByTransferIdAsync(Guid operationId, OperationType type);

        Task InsertAsync(Operation operation);

        Task UpdateAsync(Guid operationId, OperationStatus status);

        Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId);
    }
}
