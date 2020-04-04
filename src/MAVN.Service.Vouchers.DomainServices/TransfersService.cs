using System;
using System.Threading.Tasks;
using Common.Log;
using Falcon.Numerics;
using Lykke.Common.Log;
using Lykke.Service.PrivateBlockchainFacade.Client;
using Lykke.Service.PrivateBlockchainFacade.Client.Models;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Exceptions;
using MAVN.Service.Vouchers.Domain.Repositories;
using MAVN.Service.Vouchers.Domain.Services;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class TransfersService : ITransfersService
    {
        private readonly ITransfersRepository _transfersRepository;
        private readonly IOperationsService _operationsService;
        private readonly IEncoderService _encoderService;
        private readonly ISettingsService _settingsService;
        private readonly IPrivateBlockchainFacadeClient _privateBlockchainFacadeClient;
        private readonly ILog _log;

        public TransfersService(
            ITransfersRepository transfersRepository,
            IOperationsService operationsService,
            IEncoderService encoderService,
            ISettingsService settingsService,
            IPrivateBlockchainFacadeClient privateBlockchainFacadeClient,
            ILogFactory logFactory)
        {
            _transfersRepository = transfersRepository;
            _operationsService = operationsService;
            _encoderService = encoderService;
            _settingsService = settingsService;
            _privateBlockchainFacadeClient = privateBlockchainFacadeClient;
            _log = logFactory.CreateLog(this);
        }

        public Task<Transfer> GetByIdAsync(Guid transferId)
        {
            return _transfersRepository.GetByIdAsync(transferId);
        }

        public async Task CreateAsync(Guid customerId, Guid spendRuleId, Guid voucherId, Money18 amount)
        {
            var transfer = new Transfer
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                SpendRuleId = spendRuleId,
                VoucherId = voucherId,
                Amount = amount,
                Status = TransferStatus.Pending,
                Created = DateTime.UtcNow
            };

            await _transfersRepository.InsertAsync(transfer);

            var additionalData = _encoderService.EncodeTransferData(transfer.SpendRuleId, transfer.Id);

            var transferResponse = await _privateBlockchainFacadeClient.GenericTransfersApi.GenericTransferAsync(
                new GenericTransferRequestModel
                {
                    Amount = amount,
                    AdditionalData = additionalData,
                    RecipientAddress = _settingsService.GetContractAddress(),
                    SenderCustomerId = customerId.ToString(),
                    TransferId = transfer.Id.ToString()
                });

            if (transferResponse.Error != TransferError.None)
            {
                _log.Error(message: "An error occurred while creating generic transfer.",
                    context:
                    $"transferId: {transfer.Id}; customerId: {transfer.CustomerId}; spendRuleId: {transfer.SpendRuleId}; error: {transferResponse.Error}");

                switch (transferResponse.Error)
                {
                    case TransferError.SenderWalletMissing:
                        throw new CustomerWalletDoesNotExistException();
                    case TransferError.NotEnoughFunds:
                        throw new NoEnoughTokensException();
                }
            }

            await _operationsService.AddAsync(
                Guid.Parse(transferResponse.OperationId),
                transfer.Id,
                OperationType.Transfer);

            _log.Info("Transfer created.", context:
                $"transferId: {transfer.Id}; customerId: {transfer.CustomerId}; spendRuleId: {transfer.SpendRuleId}; operationId: {transferResponse.OperationId}");
        }

        public async Task CompleteAsync(Guid transferId)
        {
            var transfer = await _transfersRepository.GetByIdAsync(transferId);

            if (transfer == null)
                throw new TransferNotFoundException();

            var additionalData = _encoderService.EncodeAcceptData(transfer.SpendRuleId, transfer.Id);

            var acceptResponse = await _privateBlockchainFacadeClient.OperationsApi.AddGenericOperationAsync(
                new GenericOperationRequest
                {
                    Data = additionalData,
                    SourceAddress = _settingsService.GetMasterWalletAddress(),
                    TargetAddress = _settingsService.GetContractAddress()
                });

            transfer.Status = TransferStatus.Completed;
            await _transfersRepository.UpdateAsync(transfer);
            await _operationsService.ProcessTransferAndAcceptOperationsAsync(transferId, acceptResponse.OperationId);

            _log.Info("Transfer completed.", context:
                $"transferId: {transfer.Id}; customerId: {transfer.CustomerId}; spendRuleId: {transfer.SpendRuleId}; operationId: {acceptResponse.OperationId}");
        }
    }
}
