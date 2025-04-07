using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEEEST.Migrations
{
    /// <inheritdoc />
    public partial class ooo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "ElapsedSeconds",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "PurchasesJson",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "RentalType",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "TimeLeft",
                table: "Actives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Actives",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ElapsedSeconds",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Actives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RentalType",
                table: "Actives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Actives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimeLeft",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
