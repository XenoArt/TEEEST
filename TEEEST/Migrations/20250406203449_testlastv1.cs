using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEEEST.Migrations
{
    /// <inheritdoc />
    public partial class testlastv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentPrice",
                table: "Actives",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ElapsedSeconds",
                table: "Actives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFixedRental",
                table: "Actives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PricePerHour",
                table: "Actives",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "ElapsedSeconds",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "IsFixedRental",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
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
    }
}
