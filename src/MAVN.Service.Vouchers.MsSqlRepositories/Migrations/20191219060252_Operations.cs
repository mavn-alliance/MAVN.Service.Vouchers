using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.Vouchers.MsSqlRepositories.Migrations
{
    public partial class Operations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_operations_transfer_id",
                schema: "vouchers",
                table: "operations",
                column: "transfer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operations",
                schema: "vouchers");
        }
    }
}
