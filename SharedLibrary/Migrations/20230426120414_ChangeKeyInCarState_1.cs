using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyInCarState_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStates_CarStateId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarStates");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStateId",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarStates",
                columns: table => new
                {
                    iddd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStates", x => x.iddd);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStateId",
                table: "Cars",
                column: "CarStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStates_CarStateId",
                table: "Cars",
                column: "CarStateId",
                principalTable: "CarStates",
                principalColumn: "iddd");
        }
    }
}
