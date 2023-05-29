using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CarStateTimer_ChangeDateTimeToTicks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_barrier_infos_areas_area_id",
                table: "barrier_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_cameras_areas_area_id",
                table: "cameras");

            migrationBuilder.DropForeignKey(
                name: "fk_cameras_camera_roles_role_id",
                table: "cameras");

            migrationBuilder.DropForeignKey(
                name: "fk_car_state_timers_car_states_car_state_id",
                table: "car_state_timers");

            migrationBuilder.DropForeignKey(
                name: "fk_car_state_timers_cars_car_id",
                table: "car_state_timers");

            migrationBuilder.DropForeignKey(
                name: "fk_car_state_timers_time_controled_states_time_controled_state",
                table: "car_state_timers");

            migrationBuilder.DropForeignKey(
                name: "fk_cars_areas_area_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "fk_cars_areas_target_area_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "fk_cars_car_states_car_state_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "fk_cars_storages_storage_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "fk_storages_areas_area_id",
                table: "storages");

            migrationBuilder.DropForeignKey(
                name: "fk_time_controled_states_car_states_car_state_id",
                table: "time_controled_states");

            migrationBuilder.DropForeignKey(
                name: "fk_users_user_role_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropIndex(
                name: "ix_users_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_time_controled_states_car_state_id",
                table: "time_controled_states");

            migrationBuilder.DropIndex(
                name: "ix_storages_area_id",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "ix_cars_area_id",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "ix_cars_car_state_id",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "ix_cars_storage_id",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "ix_cars_target_area_id",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "ix_car_state_timers_car_id",
                table: "car_state_timers");

            migrationBuilder.DropIndex(
                name: "ix_car_state_timers_car_state_id",
                table: "car_state_timers");

            migrationBuilder.DropIndex(
                name: "ix_car_state_timers_time_controled_state_id",
                table: "car_state_timers");

            migrationBuilder.DropIndex(
                name: "ix_cameras_area_id",
                table: "cameras");

            migrationBuilder.DropIndex(
                name: "ix_cameras_role_id",
                table: "cameras");

            migrationBuilder.DropIndex(
                name: "ix_barrier_infos_area_id",
                table: "barrier_infos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "waiting_lists",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "unknown_car_notifies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "not_in_list_car_notifies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "logs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "inspection_required_car_notifies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "expired_list_car_notifies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "car_state_timers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<long>(
                name: "start_time_ticks",
                table: "car_state_timers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "car_detected_notifies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "start_time_ticks",
                table: "car_state_timers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "waiting_lists",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "unknown_car_notifies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "not_in_list_car_notifies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "logs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "inspection_required_car_notifies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "expired_list_car_notifies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "car_state_timers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "car_detected_notifies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    permissions = table.Column<int>(type: "integer", nullable: false),
                    role_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_controled_states_car_state_id",
                table: "time_controled_states",
                column: "car_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_storages_area_id",
                table: "storages",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_area_id",
                table: "cars",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_car_state_id",
                table: "cars",
                column: "car_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_storage_id",
                table: "cars",
                column: "storage_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_target_area_id",
                table: "cars",
                column: "target_area_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_state_timers_car_id",
                table: "car_state_timers",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_state_timers_car_state_id",
                table: "car_state_timers",
                column: "car_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_state_timers_time_controled_state_id",
                table: "car_state_timers",
                column: "time_controled_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_cameras_area_id",
                table: "cameras",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_cameras_role_id",
                table: "cameras",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_barrier_infos_area_id",
                table: "barrier_infos",
                column: "area_id");

            migrationBuilder.AddForeignKey(
                name: "fk_barrier_infos_areas_area_id",
                table: "barrier_infos",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cameras_areas_area_id",
                table: "cameras",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cameras_camera_roles_role_id",
                table: "cameras",
                column: "role_id",
                principalTable: "camera_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_car_state_timers_car_states_car_state_id",
                table: "car_state_timers",
                column: "car_state_id",
                principalTable: "car_states",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_car_state_timers_cars_car_id",
                table: "car_state_timers",
                column: "car_id",
                principalTable: "cars",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_car_state_timers_time_controled_states_time_controled_state",
                table: "car_state_timers",
                column: "time_controled_state_id",
                principalTable: "time_controled_states",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cars_areas_area_id",
                table: "cars",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cars_areas_target_area_id",
                table: "cars",
                column: "target_area_id",
                principalTable: "areas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cars_car_states_car_state_id",
                table: "cars",
                column: "car_state_id",
                principalTable: "car_states",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cars_storages_storage_id",
                table: "cars",
                column: "storage_id",
                principalTable: "storages",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_storages_areas_area_id",
                table: "storages",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_time_controled_states_car_states_car_state_id",
                table: "time_controled_states",
                column: "car_state_id",
                principalTable: "car_states",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_role_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
