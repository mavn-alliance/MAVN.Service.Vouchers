using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Migrations
{
    public partial class PurchaseDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount_in_base_currency",
                schema: "vouchers",
                table: "customer_vouchers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "amount_in_tokens",
                schema: "vouchers",
                table: "customer_vouchers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "purchase_date",
                schema: "vouchers",
                table: "customer_vouchers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_spend_rule_id",
                schema: "vouchers",
                table: "vouchers",
                column: "spend_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_customer_id",
                schema: "vouchers",
                table: "customer_vouchers",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vouchers_spend_rule_id",
                schema: "vouchers",
                table: "vouchers");

            migrationBuilder.DropIndex(
                name: "IX_customer_vouchers_customer_id",
                schema: "vouchers",
                table: "customer_vouchers");

            migrationBuilder.DropColumn(
                name: "amount_in_base_currency",
                schema: "vouchers",
                table: "customer_vouchers");

            migrationBuilder.DropColumn(
                name: "amount_in_tokens",
                schema: "vouchers",
                table: "customer_vouchers");

            migrationBuilder.DropColumn(
                name: "purchase_date",
                schema: "vouchers",
                table: "customer_vouchers");
        }
    }
}
