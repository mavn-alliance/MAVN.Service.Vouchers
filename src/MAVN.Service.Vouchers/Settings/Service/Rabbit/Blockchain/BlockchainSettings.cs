using JetBrains.Annotations;

namespace MAVN.Service.Vouchers.Settings.Service.Rabbit.Blockchain
{
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public class BlockchainSettings
    {
        public string VouchersPaymentsContractAddress { get; set; }

        public string MasterWalletAddress { get; set; }
    }
}
