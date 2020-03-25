using JetBrains.Annotations;
using Lykke.Service.Vouchers.Settings.Service.Rabbit.Publishers;
using Lykke.Service.Vouchers.Settings.Service.Rabbit.Subscribers;

namespace Lykke.Service.Vouchers.Settings.Service.Rabbit
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitSettings
    {
        public RabbitSubscribers Subscribers { get; set; }

        public RabbitPublishers Publishers { get; set; }
    }
}
