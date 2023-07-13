using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGateMetricsData.Migrations
{
    /// <inheritdoc />
    public partial class splitStandortPhase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StandortId",
                table: "Buchungen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Standort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Standortname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standort", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_StandortId",
                table: "Buchungen",
                column: "StandortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buchungen_Standort_StandortId",
                table: "Buchungen",
                column: "StandortId",
                principalTable: "Standort",
                principalColumn: "Id");

            migrationBuilder.Sql("INSERT INTO Standort SELECT DISTINCT(Standort) FROM Buchungen");

            migrationBuilder.Sql("" +
                "UPDATE Buchungen " +
                "SET StandortId = s.Id " +
                "FROM Buchungen b " +
                "INNER JOIN Standort s ON s.Standortname = b.Standort");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buchungen_Standort_StandortId",
                table: "Buchungen");

            migrationBuilder.DropTable(
                name: "Standort");

            migrationBuilder.DropIndex(
                name: "IX_Buchungen_StandortId",
                table: "Buchungen");

            migrationBuilder.DropColumn(
                name: "StandortId",
                table: "Buchungen");
        }
    }
}
