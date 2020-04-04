using JetBrains.Annotations;
using MAVN.Service.Vouchers.Settings.Service.Rabbit.Publishers;
using MAVN.Service.Vouchers.Settings.Service.Rabbit.Subscribers;

namespace MAVN.Service.Vouchers.Settings.Service.Rabbit
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitSettings
    {
        public RabbitSubscribers Subscribers { get; set; }

        public RabbitPublishers Publishers { get; set; }
    }
}
