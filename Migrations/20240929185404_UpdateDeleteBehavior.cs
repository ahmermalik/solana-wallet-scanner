using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace walletscanner.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_Address",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_Address",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ThresholdAmount",
                table: "Alerts");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Transactions",
                newName: "NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                newName: "IX_Transactions_NetworkId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Alerts",
                newName: "AlertId");

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "WhaleActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "TrendingTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromWalletId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToWalletId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "TopTraders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "Tokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "TokenMetrics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "DumpEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Alerts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Alerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "Alerts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Alerts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    NetworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RPC_Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explorer_URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.NetworkId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhaleActivities_NetworkId",
                table: "WhaleActivities",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_NetworkId_Address",
                table: "Wallets",
                columns: new[] { "NetworkId", "Address" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrendingTokens_NetworkId",
                table: "TrendingTokens",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TrendingTokens_Rank",
                table: "TrendingTokens",
                column: "Rank");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromWalletId",
                table: "Transactions",
                column: "FromWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToWalletId",
                table: "Transactions",
                column: "ToWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TopTraders_NetworkId",
                table: "TopTraders",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_NetworkId_Address",
                table: "Tokens",
                columns: new[] { "NetworkId", "Address" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenMetrics_NetworkId",
                table: "TokenMetrics",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_DumpEvents_NetworkId",
                table: "DumpEvents",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_NetworkId",
                table: "Alerts",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_WalletId",
                table: "Alerts",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Networks_Name",
                table: "Networks",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Networks_NetworkId",
                table: "Alerts",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Wallets_WalletId",
                table: "Alerts",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_DumpEvents_Networks_NetworkId",
                table: "DumpEvents",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenMetrics_Networks_NetworkId",
                table: "TokenMetrics",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Networks_NetworkId",
                table: "Tokens",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopTraders_Networks_NetworkId",
                table: "TopTraders",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Networks_NetworkId",
                table: "Transactions",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_FromWalletId",
                table: "Transactions",
                column: "FromWalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_ToWalletId",
                table: "Transactions",
                column: "ToWalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrendingTokens_Networks_NetworkId",
                table: "TrendingTokens",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Networks_NetworkId",
                table: "Wallets",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WhaleActivities_Networks_NetworkId",
                table: "WhaleActivities",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Networks_NetworkId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Wallets_WalletId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_DumpEvents_Networks_NetworkId",
                table: "DumpEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenMetrics_Networks_NetworkId",
                table: "TokenMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Networks_NetworkId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TopTraders_Networks_NetworkId",
                table: "TopTraders");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Networks_NetworkId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_FromWalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_ToWalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TrendingTokens_Networks_NetworkId",
                table: "TrendingTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Networks_NetworkId",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_WhaleActivities_Networks_NetworkId",
                table: "WhaleActivities");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropIndex(
                name: "IX_WhaleActivities_NetworkId",
                table: "WhaleActivities");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_NetworkId_Address",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_TrendingTokens_NetworkId",
                table: "TrendingTokens");

            migrationBuilder.DropIndex(
                name: "IX_TrendingTokens_Rank",
                table: "TrendingTokens");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FromWalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToWalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TopTraders_NetworkId",
                table: "TopTraders");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_NetworkId_Address",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_TokenMetrics_NetworkId",
                table: "TokenMetrics");

            migrationBuilder.DropIndex(
                name: "IX_DumpEvents_NetworkId",
                table: "DumpEvents");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_NetworkId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_WalletId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "WhaleActivities");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "TrendingTokens");

            migrationBuilder.DropColumn(
                name: "FromWalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToWalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "TopTraders");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "TokenMetrics");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "DumpEvents");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Alerts");

            migrationBuilder.RenameColumn(
                name: "NetworkId",
                table: "Transactions",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_NetworkId",
                table: "Transactions",
                newName: "IX_Transactions_WalletId");

            migrationBuilder.RenameColumn(
                name: "AlertId",
                table: "Alerts",
                newName: "Id");

            migrationBuilder.AddColumn<decimal>(
                name: "ThresholdAmount",
                table: "Alerts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Address",
                table: "Wallets",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Address",
                table: "Tokens",
                column: "Address",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
