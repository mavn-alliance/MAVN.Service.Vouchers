using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.QuorumTransactionWatcher.Contract;
using Lykke.PrivateBlockchain.Definitions;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.Vouchers.Domain.Services;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts;

namespace MAVN.Service.Vouchers.Rabbit.Subscribers
{
    public class UndecodedEventSubscriber : JsonRabbitSubscriber<UndecodedEvent>
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;
        private readonly EventTopicDecoder _eventTopicDecoder;
        private readonly string _transferReceivedEventSignature;
        private readonly string _transferAcceptedEventSignature;
        private readonly string _transferRejectedEventSignature;
        private readonly string _contractAddress;

        public UndecodedEventSubscriber(
            IPurchaseService purchaseService,
            IOperationsService operationsService,
            ISettingsService settingsService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _purchaseService = purchaseService;
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);

            _eventTopicDecoder = new EventTopicDecoder();

            _transferReceivedEventSignature = $"0x{ABITypedRegistry.GetEvent<TransferReceivedEventDTO>().Sha3Signature}";
            _transferAcceptedEventSignature = $"0x{ABITypedRegistry.GetEvent<TransferAcceptedEventDTO>().Sha3Signature}";
            _transferRejectedEventSignature = $"0x{ABITypedRegistry.GetEvent<TransferRejectedEventDTO>().Sha3Signature}";
            _contractAddress = settingsService.GetContractAddress();
        }

        protected override async Task ProcessMessageAsync(UndecodedEvent message)
        {
            if (!message.OriginAddress.Equals(_contractAddress, StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                var signature = message.Topics[0];

                if (signature.Equals(_transferReceivedEventSignature, StringComparison.OrdinalIgnoreCase))
                {
                    var @event = _eventTopicDecoder.DecodeTopics<TransferReceivedEventDTO>(
                        message.Topics.Select(o => (object) o).ToArray(), message.Data);

                    await _purchaseService.ConfirmAsync(Guid.Parse(@event.TransferId));

                    _log.Info("Transfer receiver event handled.", context: $"transferId: {@event.TransferId}");
                }
                else if (signature.Equals(_transferAcceptedEventSignature, StringComparison.OrdinalIgnoreCase))
                {
                    var @event = _eventTopicDecoder.DecodeTopics<TransferAcceptedEventDTO>(
                        message.Topics.Select(o => (object)o).ToArray(), message.Data);

                    await _operationsService.HandleSucceededTransferAsync(Guid.Parse(@event.TransferId));
                }
                else if (signature.Equals(_transferRejectedEventSignature, StringComparison.OrdinalIgnoreCase))
                {
                    var @event = _eventTopicDecoder.DecodeTopics<TransferRejectedEventDTO>(
                        message.Topics.Select(o => (object)o).ToArray(), message.Data);

                    await _operationsService.HandleFailedTransferAsync(Guid.Parse(@event.TransferId));
                }
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occurred while processing undecoded event.",
                    $"transactionHash: {message.TransactionHash}");
            }
        }
    }
}
