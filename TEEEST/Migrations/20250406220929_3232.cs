using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEEEST.Migrations
{
    /// <inheritdoc />
    public partial class _3232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PayLater",
                table: "Actives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimePassedInSeconds",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayLater",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "TimePassedInSeconds",
                table: "Actives");
        }
    }
}
