using JetBrains.Annotations;
using MAVN.Service.Vouchers.Settings.Service.Db;
using MAVN.Service.Vouchers.Settings.Service.Rabbit;
using MAVN.Service.Vouchers.Settings.Service.Rabbit.Blockchain;

namespace MAVN.Service.Vouchers.Settings.service
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class VouchersSettings
    {
        public DbSettings Db { get; set; }

        public RabbitSettings Rabbit { get; set; }

        public BlockchainSettings Blockchain { get; set; }
    }
}
