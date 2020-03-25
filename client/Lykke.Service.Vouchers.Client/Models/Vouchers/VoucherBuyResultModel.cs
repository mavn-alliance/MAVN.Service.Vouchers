using System;
using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Represents voucher purchase result.
    /// </summary>
    [PublicAPI]
    public class VoucherBuyResultModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VoucherBuyResultModel"/>.
        /// </summary>
        public VoucherBuyResultModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VoucherBuyResultModel"/> with voucher code.
        /// </summary>
        public VoucherBuyResultModel(Guid id, string code)
        {
            Id = id;
            Code = code;
            ErrorCode = VoucherErrorCode.None;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VoucherBuyResultModel"/> with error code.
        /// </summary>
        public VoucherBuyResultModel(VoucherErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The voucher code.
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Specifies an error code of voucher purchase.
        /// </summary>
        public VoucherErrorCode ErrorCode { get; set; }
    }
}
