using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace walletscanner.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decimals = table.Column<int>(type: "int", nullable: true),
                    Liquidity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Volume24hUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceChangePercent24h = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LogoURI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalUsdValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "TokenMetrics",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    TopPerformingWallets = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrelationData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenMetrics", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_TokenMetrics_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrendingTokens",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: true),
                    Volume24hUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrendingTokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_TrendingTokens_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: true),
                    ThresholdAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlertType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId");
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DumpEvents",
                columns: table => new
                {
                    DumpEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    VolumeSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceDropPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DumpEvents", x => x.DumpEventId);
                    table.ForeignKey(
                        name: "FK_DumpEvents_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DumpEvents_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId");
                });

            migrationBuilder.CreateTable(
                name: "TopTraders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradeCount = table.Column<int>(type: "int", nullable: true),
                    TradeBuyCount = table.Column<int>(type: "int", nullable: true),
                    TradeSellCount = table.Column<int>(type: "int", nullable: true),
                    VolumeBuy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VolumeSell = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTraders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopTraders_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopTraders_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TxHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    BlockNumber = table.Column<long>(type: "bigint", nullable: true),
                    BlockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValueUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHoldings",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValueUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHoldings", x => new { x.WalletId, x.TokenId });
                    table.ForeignKey(
                        name: "FK_WalletHoldings_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletHoldings_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletMetrics",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Profitability = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WinLossRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AverageHoldTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostBasis = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradeFrequency = table.Column<int>(type: "int", nullable: true),
                    TradeSizeAverage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletMetrics", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_WalletMetrics_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WhaleActivities",
                columns: table => new
                {
                    WhaleActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhaleActivities", x => x.WhaleActivityId);
                    table.ForeignKey(
                        name: "FK_WhaleActivities_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhaleActivities_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_TokenId_AlertType",
                table: "Alerts",
                columns: new[] { "TokenId", "AlertType" });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserId",
                table: "Alerts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DumpEvents_TokenId",
                table: "DumpEvents",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_DumpEvents_WalletId",
                table: "DumpEvents",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Address",
                table: "Tokens",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopTraders_TokenId",
                table: "TopTraders",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_TopTraders_WalletId",
                table: "TopTraders",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BlockTime",
                table: "Transactions",
                column: "BlockTime");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TokenId",
                table: "Transactions",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHoldings_TokenId",
                table: "WalletHoldings",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Address",
                table: "Wallets",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhaleActivities_TokenId_Timestamp",
                table: "WhaleActivities",
                columns: new[] { "TokenId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_WhaleActivities_WalletId",
                table: "WhaleActivities",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "DumpEvents");

            migrationBuilder.DropTable(
                name: "TokenMetrics");

            migrationBuilder.DropTable(
                name: "TopTraders");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TrendingTokens");

            migrationBuilder.DropTable(
                name: "WalletHoldings");

            migrationBuilder.DropTable(
                name: "WalletMetrics");

            migrationBuilder.DropTable(
                name: "WhaleActivities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
