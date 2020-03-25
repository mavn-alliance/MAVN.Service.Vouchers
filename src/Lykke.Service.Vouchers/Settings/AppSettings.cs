using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.Campaign.Client;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.EligibilityEngine.Client;
using Lykke.Service.PrivateBlockchainFacade.Client;
using Lykke.Service.Vouchers.Settings.Common;
using Lykke.Service.Vouchers.Settings.service;
using Lykke.Service.WalletManagement.Client;

namespace Lykke.Service.Vouchers.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public VouchersSettings VouchersService { get; set; }

        public ConstantsSettings Constants { get; set; }

        public CampaignServiceClientSettings CampaignService { get; set; }

        public CustomerProfileServiceClientSettings CustomerProfileService { get; set; }

        public EligibilityEngineServiceClientSettings EligibilityEngineService { get; set; }

        public PrivateBlockchainFacadeServiceClientSettings PrivateBlockchainFacadeService { get; set; }

        public WalletManagementServiceClientSettings WalletManagementService { get; set; }
    }
}
