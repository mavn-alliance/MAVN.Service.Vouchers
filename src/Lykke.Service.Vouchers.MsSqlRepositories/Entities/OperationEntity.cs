using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Entities
{
    [Table("operations")]
    public class OperationEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("transfer_id")]
        public Guid TransferId { get; set; }

        [Column("type")]
        public OperationType Type { get; set; }

        [Column("status")]
        public OperationStatus Status { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }
    }
}
