using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Vouchers.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vouchers");

            migrationBuilder.CreateTable(
                name: "vouchers",
                schema: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    code = table.Column<string>(type: "varchar(15)", nullable: false),
                    status = table.Column<short>(nullable: false),
                    spend_rule_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_vouchers",
                schema: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    voucher_id = table.Column<Guid>(nullable: false),
                    amount_in_tokens = table.Column<string>(nullable: false),
                    amount_in_base_currency = table.Column<decimal>(nullable: false),
                    purchase_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_vouchers_vouchers_voucher_id",
                        column: x => x.voucher_id,
                        principalSchema: "vouchers",
                        principalTable: "vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                schema: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    spend_rule_id = table.Column<Guid>(nullable: false),
                    voucher_id = table.Column<Guid>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    status = table.Column<short>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfers", x => x.id);
                    table.ForeignKey(
                        name: "FK_transfers_vouchers_voucher_id",
                        column: x => x.voucher_id,
                        principalSchema: "vouchers",
                        principalTable: "vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operations",
                schema: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    transfer_id = table.Column<Guid>(nullable: false),
                    type = table.Column<short>(nullable: false),
                    status = table.Column<short>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_operations_transfers_transfer_id",
                        column: x => x.transfer_id,
                        principalSchema: "vouchers",
                        principalTable: "transfers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_customer_id",
                schema: "vouchers",
                table: "customer_vouchers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_voucher_id",
                schema: "vouchers",
                table: "customer_vouchers",
                column: "voucher_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operations_transfer_id",
                schema: "vouchers",
                table: "operations",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_voucher_id",
                schema: "vouchers",
                table: "transfers",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_code",
                schema: "vouchers",
                table: "vouchers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_spend_rule_id",
                schema: "vouchers",
                table: "vouchers",
                column: "spend_rule_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_vouchers",
                schema: "vouchers");

            migrationBuilder.DropTable(
                name: "operations",
                schema: "vouchers");

            migrationBuilder.DropTable(
                name: "transfers",
                schema: "vouchers");

            migrationBuilder.DropTable(
                name: "vouchers",
                schema: "vouchers");
        }
    }
}
