using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGateMetricsData.Migrations
{
    /// <inheritdoc />
    public partial class splitStandoartPhase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Standort",
                table: "Buchungen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Standort",
                table: "Buchungen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
