namespace Lykke.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Specifies an blockchain operation.
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Unspecified operation.
        /// </summary>
        None,

        /// <summary>
        /// Indicates transfer blockchain operation.
        /// </summary>
        Transfer,

        /// <summary>
        /// Indicates accept blockchain operation.
        /// </summary>
        Accept
    }
}
