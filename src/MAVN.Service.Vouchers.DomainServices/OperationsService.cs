using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.Vouchers.Contract;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Exceptions;
using MAVN.Service.Vouchers.Domain.Repositories;
using MAVN.Service.Vouchers.Domain.Services;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class OperationsService : IOperationsService
    {
        private readonly IOperationsRepository _operationsRepository;
        private readonly ITransfersRepository _transfersRepository;
        private readonly IRabbitPublisher<VoucherTokensUsedEvent> _voucherTokensUsedEventPublisher;
        private readonly ILog _log;

        public OperationsService(
            IOperationsRepository operationsRepository,
            ITransfersRepository transfersRepository,
            IRabbitPublisher<VoucherTokensUsedEvent> voucherTokensUsedEventPublisher,
            ILogFactory logFactory)
        {
            _operationsRepository = operationsRepository;
            _transfersRepository = transfersRepository;
            _voucherTokensUsedEventPublisher = voucherTokensUsedEventPublisher;
            _log = logFactory.CreateLog(this);
        }

        public Task AddAsync(Guid operationId, Guid transferId, OperationType type)
        {
            var operation = new Operation
            {
                Id = operationId,
                TransferId = transferId,
                Type = type,
                Status = OperationStatus.Created,
                Created = DateTime.UtcNow
            };

            return _operationsRepository.InsertAsync(operation);
        }

        public async Task HandleSucceededTransferAsync(Guid transferId)
        {
            var operation = await _operationsRepository.GetByTransferIdAsync(transferId, OperationType.Accept);

            if (operation == null)
                return;

            await _operationsRepository.UpdateAsync(operation.Id, OperationStatus.Succeeded);

            var transfer = await _transfersRepository.GetByIdAsync(operation.TransferId);
            if (transfer == null)
                throw new TransferNotFoundException();

            var evt = new VoucherTokensUsedEvent
            {
                TransferId = operation.TransferId,
                CustomerId = transfer.CustomerId,
                SpendRuleId = transfer.SpendRuleId,
                Amount = transfer.Amount,
                Timestamp = transfer.Created,
                VoucherId = transfer.VoucherId,
            };
            await _voucherTokensUsedEventPublisher.PublishAsync(evt);

            _log.Info("Operation completed.", context:
                $"transferId: {operation.Id}; type: {operation.Type}; transferId: {operation.TransferId}");
        }

        public async Task HandleFailedTransferAsync(Guid transferId)
        {
            var operation = await _operationsRepository.GetByTransferIdAsync(transferId, OperationType.Accept);

            if (operation == null)
                return;

            await _operationsRepository.UpdateAsync(operation.Id, OperationStatus.Failed);

            _log.Error(message: "Operation failed.", context:
                $"transferId: {operation.Id}; type: {operation.Type}; transferId: {operation.TransferId}");
        }

        public async Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId)
        {
            await _operationsRepository.ProcessTransferAndAcceptOperationsAsync(transferId, acceptOperationId);
        }
    }
}
