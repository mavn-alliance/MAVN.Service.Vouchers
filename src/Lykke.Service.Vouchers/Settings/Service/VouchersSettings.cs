using JetBrains.Annotations;
using Lykke.Service.Vouchers.Settings.Service.Db;
using Lykke.Service.Vouchers.Settings.Service.Rabbit;
using Lykke.Service.Vouchers.Settings.Service.Rabbit.Blockchain;

namespace Lykke.Service.Vouchers.Settings.service
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class VouchersSettings
    {
        public DbSettings Db { get; set; }

        public RabbitSettings Rabbit { get; set; }

        public BlockchainSettings Blockchain { get; set; }
    }
}
