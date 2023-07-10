using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGateMetricsData.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fahrer",
                columns: table => new
                {
                    AusweisId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Vorname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Geburtsort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Geburtstag = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Anrede = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Firma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrer", x => x.AusweisId);
                });

            migrationBuilder.CreateTable(
                name: "Fahrzeuge",
                columns: table => new
                {
                    Fahrgestellnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hersteller = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kennzeichen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZulGesamtGewicht = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrzeuge", x => x.Fahrgestellnummer);
                });

            migrationBuilder.CreateTable(
                name: "Buchungen",
                columns: table => new
                {
                    BuchungsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UhrzeitIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UhrzeitOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AusweisId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fahrgestellnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GewichtIn = table.Column<int>(type: "int", nullable: false),
                    GewichtOut = table.Column<int>(type: "int", nullable: true),
                    Gefahrgut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FahrerAusweisId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FahrzeugFahrgestellnummer = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buchungen", x => x.BuchungsId);
                    table.ForeignKey(
                        name: "FK_Buchungen_Fahrer_FahrerAusweisId",
                        column: x => x.FahrerAusweisId,
                        principalTable: "Fahrer",
                        principalColumn: "AusweisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buchungen_Fahrzeuge_FahrzeugFahrgestellnummer",
                        column: x => x.FahrzeugFahrgestellnummer,
                        principalTable: "Fahrzeuge",
                        principalColumn: "Fahrgestellnummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_FahrerAusweisId",
                table: "Buchungen",
                column: "FahrerAusweisId");

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_FahrzeugFahrgestellnummer",
                table: "Buchungen",
                column: "FahrzeugFahrgestellnummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buchungen");

            migrationBuilder.DropTable(
                name: "Fahrer");

            migrationBuilder.DropTable(
                name: "Fahrzeuge");
        }
    }
}
