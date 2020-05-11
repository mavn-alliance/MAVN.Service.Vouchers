using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.Campaign.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.EligibilityEngine.Client;
using MAVN.Service.PrivateBlockchainFacade.Client;
using MAVN.Service.Vouchers.Settings.Common;
using MAVN.Service.Vouchers.Settings.service;
using MAVN.Service.WalletManagement.Client;

namespace MAVN.Service.Vouchers.Settings
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
