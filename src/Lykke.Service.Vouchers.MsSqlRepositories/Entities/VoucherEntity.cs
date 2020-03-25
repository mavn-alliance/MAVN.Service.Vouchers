using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lykke.Service.Vouchers.Domain.Entities;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Entities
{
    [Table("vouchers")]
    public class VoucherEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("code", TypeName = "varchar(15)")]
        public string Code { get; set; }

        [Column("status")]
        public VoucherStatus Status { get; set; }

        [Column("spend_rule_id")]
        public Guid SpendRuleId { get; set; }

        public CustomerVoucherEntity CustomerVoucher { get; set; }
    }
}
