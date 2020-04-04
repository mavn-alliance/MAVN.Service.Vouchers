using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.MsSqlRepositories.Entities
{
    [Table("transfers")]
    public class TransferEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("spend_rule_id")]
        public Guid SpendRuleId { get; set; }

        [Column("voucher_id")]
        public Guid VoucherId { get; set; }

        [Required]
        [Column("amount")]
        public Money18 Amount { get; set; }

        [Column("status")]
        public TransferStatus Status { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        public ICollection<OperationEntity> Operations { get; set; }
    }
}
