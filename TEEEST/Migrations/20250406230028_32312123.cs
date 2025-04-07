using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEEEST.Migrations
{
    /// <inheritdoc />
    public partial class _32312123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFixed",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "PayLater",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "PurchasesJson",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "TimeInSeconds",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "TimePassedInSeconds",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Actives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFixed",
                table: "Actives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PayLater",
                table: "Actives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Actives",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesJson",
                table: "Actives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeInSeconds",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimePassedInSeconds",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Actives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
