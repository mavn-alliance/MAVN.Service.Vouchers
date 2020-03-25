using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.Vouchers.MsSqlRepositories.Migrations
{
    public partial class VoucherCodeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "vouchers",
                table: "vouchers",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "vouchers",
                table: "vouchers",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");
        }
    }
}
