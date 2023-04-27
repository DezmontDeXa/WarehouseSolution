using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyCameraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endpoint",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "UseSsl",
                table: "Cameras");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "Cameras",
                newName: "Link");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Cameras",
                newName: "Ip");

            migrationBuilder.AddColumn<string>(
                name: "Endpoint",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseSsl",
                table: "Cameras",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
