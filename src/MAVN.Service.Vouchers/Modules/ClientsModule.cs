using Autofac;
using JetBrains.Annotations;
using MAVN.Service.Campaign.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.EligibilityEngine.Client;
using MAVN.Service.PrivateBlockchainFacade.Client;
using MAVN.Service.Vouchers.Settings;
using MAVN.Service.WalletManagement.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.Vouchers.Modules
{
    [UsedImplicitly]
    public class ClientsModule : Module
    {
        private readonly AppSettings _settings;

        public ClientsModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterCampaignClient(_settings.CampaignService);
            builder.RegisterCustomerProfileClient(_settings.CustomerProfileService);
            builder.RegisterEligibilityEngineClient(_settings.EligibilityEngineService, null);
            builder.RegisterPrivateBlockchainFacadeClient(_settings.PrivateBlockchainFacadeService, null);
            builder.RegisterWalletManagementClient(_settings.WalletManagementService, null);
        }
    }
}
