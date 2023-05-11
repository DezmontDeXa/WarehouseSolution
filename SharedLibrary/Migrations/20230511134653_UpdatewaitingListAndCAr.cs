using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdatewaitingListAndCAr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Camera",
                table: "WaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "WaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WaitingLists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "WaitingLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfArrival",
                table: "WaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "WaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ship",
                table: "WaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Camera",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "PurposeOfArrival",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Ship",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "Driver",
                table: "Cars");
        }
    }
}
