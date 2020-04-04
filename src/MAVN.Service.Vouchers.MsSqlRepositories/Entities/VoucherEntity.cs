using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers.MsSqlRepositories.Entities
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
