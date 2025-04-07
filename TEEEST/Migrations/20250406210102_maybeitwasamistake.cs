using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEEEST.Migrations
{
    /// <inheritdoc />
    public partial class maybeitwasamistake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFixedRental",
                table: "Actives");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Actives",
                newName: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Actives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Actives");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Actives",
                newName: "PricePerHour");

            migrationBuilder.AddColumn<bool>(
                name: "IsFixedRental",
                table: "Actives",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
