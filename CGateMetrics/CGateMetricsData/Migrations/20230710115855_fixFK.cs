using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGateMetricsData.Migrations
{
    /// <inheritdoc />
    public partial class fixFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buchungen_Fahrer_FahrerAusweisId",
                table: "Buchungen");

            migrationBuilder.DropForeignKey(
                name: "FK_Buchungen_Fahrzeuge_FahrzeugFahrgestellnummer",
                table: "Buchungen");

            migrationBuilder.DropIndex(
                name: "IX_Buchungen_FahrerAusweisId",
                table: "Buchungen");

            migrationBuilder.DropIndex(
                name: "IX_Buchungen_FahrzeugFahrgestellnummer",
                table: "Buchungen");

            migrationBuilder.DropColumn(
                name: "FahrerAusweisId",
                table: "Buchungen");

            migrationBuilder.DropColumn(
                name: "FahrzeugFahrgestellnummer",
                table: "Buchungen");

            migrationBuilder.AlterColumn<string>(
                name: "Fahrgestellnummer",
                table: "Buchungen",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AusweisId",
                table: "Buchungen",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_AusweisId",
                table: "Buchungen",
                column: "AusweisId");

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_Fahrgestellnummer",
                table: "Buchungen",
                column: "Fahrgestellnummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Buchungen_Fahrer_AusweisId",
                table: "Buchungen",
                column: "AusweisId",
                principalTable: "Fahrer",
                principalColumn: "AusweisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buchungen_Fahrzeuge_Fahrgestellnummer",
                table: "Buchungen",
                column: "Fahrgestellnummer",
                principalTable: "Fahrzeuge",
                principalColumn: "Fahrgestellnummer",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buchungen_Fahrer_AusweisId",
                table: "Buchungen");

            migrationBuilder.DropForeignKey(
                name: "FK_Buchungen_Fahrzeuge_Fahrgestellnummer",
                table: "Buchungen");

            migrationBuilder.DropIndex(
                name: "IX_Buchungen_AusweisId",
                table: "Buchungen");

            migrationBuilder.DropIndex(
                name: "IX_Buchungen_Fahrgestellnummer",
                table: "Buchungen");

            migrationBuilder.AlterColumn<string>(
                name: "Fahrgestellnummer",
                table: "Buchungen",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "AusweisId",
                table: "Buchungen",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "FahrerAusweisId",
                table: "Buchungen",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FahrzeugFahrgestellnummer",
                table: "Buchungen",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_FahrerAusweisId",
                table: "Buchungen",
                column: "FahrerAusweisId");

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_FahrzeugFahrgestellnummer",
                table: "Buchungen",
                column: "FahrzeugFahrgestellnummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Buchungen_Fahrer_FahrerAusweisId",
                table: "Buchungen",
                column: "FahrerAusweisId",
                principalTable: "Fahrer",
                principalColumn: "AusweisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buchungen_Fahrzeuge_FahrzeugFahrgestellnummer",
                table: "Buchungen",
                column: "FahrzeugFahrgestellnummer",
                principalTable: "Fahrzeuge",
                principalColumn: "Fahrgestellnummer",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
