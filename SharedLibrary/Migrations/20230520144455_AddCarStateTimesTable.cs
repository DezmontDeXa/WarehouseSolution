using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddCarStateTimesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarStateTimers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    CarStateId = table.Column<int>(type: "int", nullable: true),
                    TimeControledStateId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStateTimers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarStateTimers_CarStates_CarStateId",
                        column: x => x.CarStateId,
                        principalTable: "CarStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarStateTimers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarStateTimers_TimeControledStates_TimeControledStateId",
                        column: x => x.TimeControledStateId,
                        principalTable: "TimeControledStates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarStateTimers_CarId",
                table: "CarStateTimers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStateTimers_CarStateId",
                table: "CarStateTimers",
                column: "CarStateId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStateTimers_TimeControledStateId",
                table: "CarStateTimers",
                column: "TimeControledStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarStateTimers");
        }
    }
}
