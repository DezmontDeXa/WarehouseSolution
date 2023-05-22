using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddCarNotifies_AddFieldIsProcessed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "UnknownCarNotifies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "NotInListCarNotifies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "InspectionRequiredCarNotifies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "ExpiredListCarNotifies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "UnknownCarNotifies");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "NotInListCarNotifies");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "InspectionRequiredCarNotifies");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "ExpiredListCarNotifies");
        }
    }
}
