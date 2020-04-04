using Autofac;
using MAVN.Service.Vouchers.Domain.Services;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class AutofacModule : Module
    {
        private readonly string _masterWalletAddress;
        private readonly string _vouchersPaymentsContractAddress;
        private readonly string _tokenSymbol;
        private readonly string _baseCurrencyCode;

        public AutofacModule(
            string masterWalletAddress,
            string vouchersPaymentsContractAddress,
            string tokenSymbol,
            string baseCurrencyCode)
        {
            _masterWalletAddress = masterWalletAddress;
            _vouchersPaymentsContractAddress = vouchersPaymentsContractAddress;
            _tokenSymbol = tokenSymbol;
            _baseCurrencyCode = baseCurrencyCode;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EncoderService>()
                .As<IEncoderService>()
                .SingleInstance();

            builder.RegisterType<OperationsService>()
                .As<IOperationsService>()
                .SingleInstance();

            builder.RegisterType<PurchaseService>()
                .As<IPurchaseService>()
                .SingleInstance();

            builder.RegisterType<ReportsService>()
                .As<IReportsService>()
                .SingleInstance();

            builder.RegisterType<SettingsService>()
                .WithParameter("masterWalletAddress", _masterWalletAddress)
                .WithParameter("vouchersPaymentsContractAddress", _vouchersPaymentsContractAddress)
                .WithParameter("tokenSymbol", _tokenSymbol)
                .WithParameter("baseCurrencyCode", _baseCurrencyCode)
                .As<ISettingsService>()
                .SingleInstance();

            builder.RegisterType<TransfersService>()
                .As<ITransfersService>()
                .SingleInstance();

            builder.RegisterType<VouchersService>()
                .As<IVouchersService>()
                .SingleInstance();
        }
    }
}
