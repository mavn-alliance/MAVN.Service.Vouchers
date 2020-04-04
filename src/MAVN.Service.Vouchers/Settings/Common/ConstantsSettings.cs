using JetBrains.Annotations;

namespace MAVN.Service.Vouchers.Settings.Common
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ConstantsSettings
    {
        public string TokenSymbol { get; set; }

        public string BaseCurrencyCode { get; set; }
    }
}
