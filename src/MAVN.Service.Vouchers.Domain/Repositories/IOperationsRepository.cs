using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Repositories
{
    public interface IOperationsRepository
    {
        Task<Operation> GetByTransferIdAsync(Guid operationId, OperationType type);

        Task InsertAsync(Operation operation);

        Task UpdateAsync(Guid operationId, OperationStatus status);

        Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId);
    }
}
