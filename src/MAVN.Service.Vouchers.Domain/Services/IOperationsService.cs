using System;
using System.Threading.Tasks;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.Domain.Services
{
    public interface IOperationsService
    {
        Task AddAsync(Guid operationId, Guid transferId, OperationType type);

        Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId);

        Task HandleSucceededTransferAsync(Guid transferId);

        Task HandleFailedTransferAsync(Guid transferId);
    }
}
