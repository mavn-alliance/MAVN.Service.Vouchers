using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.Vouchers.Client
{
    /// <summary>
    /// Vouchers client settings.
    /// </summary>
    [PublicAPI]
    public class VouchersServiceClientSettings
    {
        /// <summary>
        /// The service url.
        /// </summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl { get; set; }
    }
}
