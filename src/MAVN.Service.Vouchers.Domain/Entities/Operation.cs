using System;

namespace MAVN.Service.Vouchers.Domain.Entities
{
    /// <summary>
    /// Represents a blockchain operation.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The transfer identifier.
        /// </summary>
        public Guid TransferId { get; set; }

        /// <summary>
        /// The operation type.
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// The operation status.
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
