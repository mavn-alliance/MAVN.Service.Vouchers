using JetBrains.Annotations;
using Lykke.Service.Vouchers.Client.Api;

namespace Lykke.Service.Vouchers.Client
{
    /// <summary>
    /// The vouchers REST API client.
    /// </summary>
    [PublicAPI]
    public interface IVouchersClient
    {
        /// <summary>
        /// The reports API.
        /// </summary>
        IReportsApi Reports { get; }

        /// <summary>
        /// The vouchers API.
        /// </summary>
        IVouchersApi Vouchers { get; }
    }
}
