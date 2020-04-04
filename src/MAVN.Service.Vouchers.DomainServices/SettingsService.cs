using MAVN.Service.Vouchers.Domain.Services;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class SettingsService : ISettingsService
    {
        private readonly string _masterWalletAddress;
        private readonly string _vouchersPaymentsContractAddress;
        private readonly string _tokenSymbol;
        private readonly string _baseCurrencyCode;

        public SettingsService(string masterWalletAddress,
            string vouchersPaymentsContractAddress,
            string tokenSymbol,
            string baseCurrencyCode)
        {
            _masterWalletAddress = masterWalletAddress;
            _vouchersPaymentsContractAddress = vouchersPaymentsContractAddress;
            _tokenSymbol = tokenSymbol;
            _baseCurrencyCode = baseCurrencyCode;
        }

        public string GetMasterWalletAddress()
            => _masterWalletAddress;

        public string GetContractAddress()
            => _vouchersPaymentsContractAddress;

        public string GetTokenCurrencyCode()
            => _tokenSymbol;

        public string GetBaseCurrencyCode()
            => _baseCurrencyCode;
    }
}
