using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;

namespace MAVN.Service.Vouchers.MsSqlRepositories.Entities
{
    [Table("customer_vouchers")]
    public class CustomerVoucherEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("voucher_id")]
        public Guid VoucherId { get; set; }

        [Required]
        [Column("amount_in_tokens")]
        public Money18 AmountInTokens { get; set; }

        [Column("amount_in_base_currency")]
        public decimal AmountInBaseCurrency { get; set; }

        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; }
    }
}
