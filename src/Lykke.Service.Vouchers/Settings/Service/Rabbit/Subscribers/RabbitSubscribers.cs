using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Vouchers.Settings.Service.Rabbit.Subscribers
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitSubscribers
    {
        [AmqpCheck]
        public string UndecodedEventConnString { get; set; }

        [AmqpCheck]
        public string TransactionSucceededConnString { get; set; }

        [AmqpCheck]
        public string TransactionFailedConnString { get; set; }
    }
}
