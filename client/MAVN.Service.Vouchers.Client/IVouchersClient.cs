using JetBrains.Annotations;
using MAVN.Service.Vouchers.Client.Api;

namespace MAVN.Service.Vouchers.Client
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
