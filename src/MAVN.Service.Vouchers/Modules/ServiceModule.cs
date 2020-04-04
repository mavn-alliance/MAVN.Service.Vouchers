using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.Vouchers.Services;
using MAVN.Service.Vouchers.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.Vouchers
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly AppSettings _settings;

        public ServiceModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DomainServices.AutofacModule(
                _settings.VouchersService.Blockchain.MasterWalletAddress,
                _settings.VouchersService.Blockchain.VouchersPaymentsContractAddress,
                _settings.Constants.TokenSymbol,
                _settings.Constants.BaseCurrencyCode));
            builder.RegisterModule(
                new MsSqlRepositories.AutofacModule(_settings.VouchersService.Db.DataConnectionString));

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}
