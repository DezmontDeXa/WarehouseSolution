using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCameraLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_car_detected_notifies_camera_camera_id",
                table: "car_detected_notifies");

            migrationBuilder.DropForeignKey(
                name: "fk_expired_list_car_notifies_camera_camera_id",
                table: "expired_list_car_notifies");

            migrationBuilder.DropForeignKey(
                name: "fk_not_in_list_car_notifies_camera_camera_id",
                table: "not_in_list_car_notifies");

            migrationBuilder.DropForeignKey(
                name: "fk_unknown_car_notifies_camera_camera_id",
                table: "unknown_car_notifies");

            migrationBuilder.DropTable(
                name: "camera");

            migrationBuilder.DropIndex(
                name: "ix_unknown_car_notifies_camera_id",
                table: "unknown_car_notifies");

            migrationBuilder.DropIndex(
                name: "ix_not_in_list_car_notifies_camera_id",
                table: "not_in_list_car_notifies");

            migrationBuilder.DropIndex(
                name: "ix_expired_list_car_notifies_camera_id",
                table: "expired_list_car_notifies");

            migrationBuilder.DropIndex(
                name: "ix_car_detected_notifies_camera_id",
                table: "car_detected_notifies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "camera",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_id = table.Column<int>(type: "integer", nullable: false),
                    direction = table.Column<int>(type: "integer", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_camera", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_unknown_car_notifies_camera_id",
                table: "unknown_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_not_in_list_car_notifies_camera_id",
                table: "not_in_list_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_expired_list_car_notifies_camera_id",
                table: "expired_list_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_detected_notifies_camera_id",
                table: "car_detected_notifies",
                column: "camera_id");

            migrationBuilder.AddForeignKey(
                name: "fk_car_detected_notifies_camera_camera_id",
                table: "car_detected_notifies",
                column: "camera_id",
                principalTable: "camera",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_expired_list_car_notifies_camera_camera_id",
                table: "expired_list_car_notifies",
                column: "camera_id",
                principalTable: "camera",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_not_in_list_car_notifies_camera_camera_id",
                table: "not_in_list_car_notifies",
                column: "camera_id",
                principalTable: "camera",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_unknown_car_notifies_camera_camera_id",
                table: "unknown_car_notifies",
                column: "camera_id",
                principalTable: "camera",
                principalColumn: "id");
        }
    }
}
