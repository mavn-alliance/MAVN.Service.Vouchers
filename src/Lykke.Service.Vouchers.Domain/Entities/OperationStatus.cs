namespace Lykke.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Specifies an operation processing status.
    /// </summary>
    public enum OperationStatus
    {
        /// <summary>
        /// Unspecified status.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the operation has been created and not processed.
        /// </summary>
        Created,

        /// <summary>
        /// Indicates that the operation processed successfully.
        /// </summary>
        Succeeded,

        /// <summary>
        /// Indicates that the operation failed.
        /// </summary>
        Failed
    }
}
