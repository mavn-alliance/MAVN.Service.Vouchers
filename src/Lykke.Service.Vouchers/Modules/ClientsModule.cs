using Autofac;
using JetBrains.Annotations;
using Lykke.Service.Campaign.Client;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.EligibilityEngine.Client;
using Lykke.Service.PrivateBlockchainFacade.Client;
using Lykke.Service.Vouchers.Settings;
using Lykke.Service.WalletManagement.Client;
using Lykke.SettingsReader;

namespace Lykke.Service.Vouchers.Modules
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
