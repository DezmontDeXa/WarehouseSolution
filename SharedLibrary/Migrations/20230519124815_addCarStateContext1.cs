using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addCarStateContext1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarStateContextId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStateContexts_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId",
                principalTable: "CarStateContexts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStateContexts_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarStateContextId",
                table: "Cars");
        }
    }
}
