namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface ISettingsService
    {
        string GetBaseCurrencyCode();

        string GetTokenCurrencyCode();

        string GetContractAddress();

        string GetMasterWalletAddress();
    }
}
