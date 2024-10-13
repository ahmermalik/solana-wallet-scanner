using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace walletscanner.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBalanceToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Balance",
                table: "WalletHoldings",
                type: "nvarchar(max)",
                precision: 25,
                scale: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(25,6)",
                oldPrecision: 25,
                oldScale: 6,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "WalletHoldings",
                type: "decimal(25,6)",
                precision: 25,
                scale: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldPrecision: 25,
                oldScale: 6);
        }
    }
}
