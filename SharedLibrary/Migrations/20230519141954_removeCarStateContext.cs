using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class removeCarStateContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStateContexts_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarStateContexts");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarStateContextId",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarStateContextId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarStateContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarStateId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStateContexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarStateContexts_CarStates_CarStateId",
                        column: x => x.CarStateId,
                        principalTable: "CarStates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStateContexts_CarStateId",
                table: "CarStateContexts",
                column: "CarStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStateContexts_CarStateContextId",
                table: "Cars",
                column: "CarStateContextId",
                principalTable: "CarStateContexts",
                principalColumn: "Id");
        }
    }
}
