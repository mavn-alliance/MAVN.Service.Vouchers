namespace MAVN.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Specifies transfer status.
    /// </summary>
    public enum TransferStatus
    {
        /// <summary>
        /// Unspecified status.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the transfer created and waiting for confirmation.
        /// </summary>
        Pending,

        /// <summary>
        /// Indicates that the transfer confirmed.
        /// </summary>
        Completed
    }
}
