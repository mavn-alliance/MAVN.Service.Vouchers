using System;
using System.Threading.Tasks;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface IOperationsService
    {
        Task AddAsync(Guid operationId, Guid transferId, OperationType type);

        Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId);

        Task HandleSucceededTransferAsync(Guid transferId);

        Task HandleFailedTransferAsync(Guid transferId);
    }
}
