using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addStorage1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_StorageId",
                table: "Cars",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Storages_StorageId",
                table: "Cars",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Storages_StorageId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_StorageId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Cars");
        }
    }
}
