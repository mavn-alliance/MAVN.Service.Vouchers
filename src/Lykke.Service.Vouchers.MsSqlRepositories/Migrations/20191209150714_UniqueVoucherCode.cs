using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Migrations
{
    public partial class UniqueVoucherCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_vouchers_code",
                schema: "vouchers",
                table: "vouchers",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vouchers_code",
                schema: "vouchers",
                table: "vouchers");
        }
    }
}
