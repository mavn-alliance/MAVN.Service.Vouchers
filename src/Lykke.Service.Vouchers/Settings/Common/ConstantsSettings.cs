using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Settings.Common
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ConstantsSettings
    {
        public string TokenSymbol { get; set; }

        public string BaseCurrencyCode { get; set; }
    }
}
