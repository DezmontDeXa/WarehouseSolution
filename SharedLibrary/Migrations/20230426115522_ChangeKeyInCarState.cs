using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyInCarState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CarStates",
                newName: "iddd");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "CarStates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "CarStates");

            migrationBuilder.RenameColumn(
                name: "iddd",
                table: "CarStates",
                newName: "Id");
        }
    }
}
