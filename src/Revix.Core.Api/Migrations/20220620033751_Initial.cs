using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revix.Core.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Volume24h = table.Column<decimal>(type: "numeric", nullable: true),
                    VolumeChange24h = table.Column<decimal>(type: "numeric", nullable: true),
                    PercentChange1h = table.Column<decimal>(type: "numeric", nullable: true),
                    PercentChange24h = table.Column<decimal>(type: "numeric", nullable: true),
                    PercentChange7d = table.Column<decimal>(type: "numeric", nullable: true),
                    MarketCap = table.Column<decimal>(type: "numeric", nullable: true),
                    MarketCapDominance = table.Column<decimal>(type: "numeric", nullable: true),
                    FullyDilutedMarketCap = table.Column<decimal>(type: "numeric", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    USDCurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    BTCCurrencyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quotes_Currencies_BTCCurrencyId",
                        column: x => x.BTCCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId");
                    table.ForeignKey(
                        name: "FK_Quotes_Currencies_USDCurrencyId",
                        column: x => x.USDCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId");
                });

            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    CryptocurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    CmcRank = table.Column<decimal>(type: "numeric", nullable: true),
                    NumMarketPairs = table.Column<decimal>(type: "numeric", nullable: true),
                    CirculatingSupply = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalSupply = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxSupply = table.Column<decimal>(type: "numeric", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SelfReportedCirculatingSupply = table.Column<string>(type: "text", nullable: true),
                    SelfReportedMarketCap = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    QuoteId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.CryptocurrencyId);
                    table.ForeignKey(
                        name: "FK_Cryptocurrencies_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId");
                    table.ForeignKey(
                        name: "FK_Cryptocurrencies_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrencies_PlatformId",
                table: "Cryptocurrencies",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrencies_QuoteId",
                table: "Cryptocurrencies",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BTCCurrencyId",
                table: "Quotes",
                column: "BTCCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_USDCurrencyId",
                table: "Quotes",
                column: "USDCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptocurrencies");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
