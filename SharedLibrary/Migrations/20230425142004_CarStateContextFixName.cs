using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CarStateContextFixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarStatContext",
                table: "Cars",
                newName: "CarStateContext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarStateContext",
                table: "Cars",
                newName: "CarStatContext");
        }
    }
}
