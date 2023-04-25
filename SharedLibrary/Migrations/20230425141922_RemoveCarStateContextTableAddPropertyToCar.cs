using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCarStateContextTableAddPropertyToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStateContext_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarStateContext");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarStateContextId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarStatContext",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarStatContext",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarStateContextId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarStateContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStateContext", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStateContext_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId",
                principalTable: "CarStateContext",
                principalColumn: "Id");
        }
    }
}
