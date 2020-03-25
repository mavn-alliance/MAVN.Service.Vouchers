using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models
{
    /// <summary>
    /// Specifies voucher service error codes.
    /// </summary>
    [PublicAPI]
    public enum VoucherErrorCode
    {
        /// <summary>
        /// Unspecified error.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the customer not found.
        /// </summary>
        CustomerNotFound,

        /// <summary>
        /// Indicates that the spend rule not found.
        /// </summary>
        SpendRuleNotFound,

        /// <summary>
        /// Indicates that the spend rule price invalid.
        /// </summary>
        InvalidSpendRulePrice,

        /// <summary>
        /// Indicates that the spend rule vertical invalid.
        /// </summary>
        InvalidSpendRuleVertical,

        /// <summary>
        /// Indicates that the customer has no enough tokens.
        /// </summary>
        NoEnoughTokens,

        /// <summary>
        /// Indicates that the voucher code already exist.
        /// </summary>
        CodeAlreadyExist,

        /// <summary>
        /// Indicates that the an error occured during converting voucher price.
        /// </summary>
        InvalidConversion,

        /// <summary>
        /// Indicates that the customer wallet blocked.
        /// </summary>
        CustomerWalletBlocked,

        /// <summary>
        /// Indicates that the customer wallet does not exist in blockchain.
        /// </summary>
        CustomerWalletDoesNotExist,
        
        /// <summary>
        /// Indicates that there are no vouchers in stock.
        /// </summary>
        NoVouchersInStock
    }
}
