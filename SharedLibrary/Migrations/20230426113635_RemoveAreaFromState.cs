using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAreaFromState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarStates_Areas_AreaId",
                table: "CarStates");

            migrationBuilder.DropIndex(
                name: "IX_CarStates_AreaId",
                table: "CarStates");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "CarStates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "CarStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarStates_AreaId",
                table: "CarStates",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarStates_Areas_AreaId",
                table: "CarStates",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }
    }
}
