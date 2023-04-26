using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyInCarState_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarStateId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStateId",
                table: "Cars",
                column: "CarStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStates_CarStateId",
                table: "Cars",
                column: "CarStateId",
                principalTable: "CarStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStates_CarStateId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStateId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarStateId",
                table: "Cars");
        }
    }
}
