using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetAreaToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetAreaId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TargetAreaId",
                table: "Cars",
                column: "TargetAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Areas_TargetAreaId",
                table: "Cars",
                column: "TargetAreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Areas_TargetAreaId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TargetAreaId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TargetAreaId",
                table: "Cars");
        }
    }
}
