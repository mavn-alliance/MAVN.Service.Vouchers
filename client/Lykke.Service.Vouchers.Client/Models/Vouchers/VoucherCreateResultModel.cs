using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents voucher creation result.
    /// </summary>
    [PublicAPI]
    public class VoucherCreateResultModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VoucherCreateResultModel"/>.
        /// </summary>
        public VoucherCreateResultModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VoucherCreateResultModel"/> with error code.
        /// </summary>
        public VoucherCreateResultModel(VoucherErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Specifies an error code of voucher creation.
        /// </summary>
        public VoucherErrorCode ErrorCode { get; set; }
    }
}
