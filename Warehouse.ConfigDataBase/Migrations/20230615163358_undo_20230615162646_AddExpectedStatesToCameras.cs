using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseConfigService.Migrations
{
    /// <inheritdoc />
    public partial class undo_20230615162646_AddExpectedStatesToCameras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_state_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car_state_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    camera_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_state_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_state_type_cameras_camera_id",
                        column: x => x.camera_id,
                        principalTable: "cameras",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_car_state_type_camera_id",
                table: "car_state_type",
                column: "camera_id");
        }
    }
}
