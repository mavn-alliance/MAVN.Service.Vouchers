using System.Data.Common;
using Lykke.Common.MsSql;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Context
{
    public class DataContext : MsSqlContext
    {
        private const string Schema = "vouchers";

        public DataContext()
            : base(Schema)
        {
        }

        public DataContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public DataContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        public DbSet<VoucherEntity> Vouchers { get; set; }

        public DbSet<CustomerVoucherEntity> CustomerVouchers { get; set; }

        public DbSet<TransferEntity> Transfers { get; set; }

        public DbSet<OperationEntity> Operations { get; set; }

        protected override void OnLykkeConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            // Vouchers

            modelBuilder.Entity<VoucherEntity>()
                .Property(o => o.Status)
                .HasConversion(new EnumToNumberConverter<VoucherStatus, short>());

            modelBuilder.Entity<VoucherEntity>()
                .HasOne(o => o.CustomerVoucher)
                .WithOne()
                .HasForeignKey<CustomerVoucherEntity>(o => o.VoucherId);

            modelBuilder.Entity<VoucherEntity>()
                .HasMany<TransferEntity>()
                .WithOne()
                .HasForeignKey(o => o.VoucherId);

            modelBuilder.Entity<VoucherEntity>()
                .HasIndex(o => o.SpendRuleId)
                .IsUnique(false);

            modelBuilder.Entity<VoucherEntity>()
                .HasIndex(o => o.Code)
                .IsUnique();

            // Customer vouchers

            modelBuilder.Entity<CustomerVoucherEntity>()
                .HasIndex(o => o.VoucherId)
                .IsUnique();

            modelBuilder.Entity<CustomerVoucherEntity>()
                .HasIndex(o => o.CustomerId)
                .IsUnique(false);

            // Transfers

            modelBuilder.Entity<TransferEntity>()
                .Property(o => o.Status)
                .HasConversion(new EnumToNumberConverter<TransferStatus, short>());

            modelBuilder.Entity<TransferEntity>()
                .HasMany(o => o.Operations)
                .WithOne()
                .HasForeignKey(o => o.TransferId);

            // Operations

            modelBuilder.Entity<OperationEntity>()
                .Property(o => o.Type)
                .HasConversion(new EnumToNumberConverter<OperationType, short>());

            modelBuilder.Entity<OperationEntity>()
                .Property(o => o.Status)
                .HasConversion(new EnumToNumberConverter<OperationStatus, short>());
        }
    }
}
