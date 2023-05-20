using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addCarStateContext2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarStateId",
                table: "CarStateContexts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarStateContexts_CarStateId",
                table: "CarStateContexts",
                column: "CarStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarStateContexts_CarStates_CarStateId",
                table: "CarStateContexts",
                column: "CarStateId",
                principalTable: "CarStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarStateContexts_CarStates_CarStateId",
                table: "CarStateContexts");

            migrationBuilder.DropIndex(
                name: "IX_CarStateContexts_CarStateId",
                table: "CarStateContexts");

            migrationBuilder.DropColumn(
                name: "CarStateId",
                table: "CarStateContexts");
        }
    }
}
