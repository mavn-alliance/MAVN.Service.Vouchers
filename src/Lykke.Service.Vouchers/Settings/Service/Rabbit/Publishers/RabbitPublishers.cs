using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Vouchers.Settings.Service.Rabbit.Publishers
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitPublishers
    {
        [AmqpCheck]
        public string VoucherTokensReservedConnString { get; set; }
    }
}
