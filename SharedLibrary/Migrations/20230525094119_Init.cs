using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "camera_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    type_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_camera_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "configs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<string>(type: "text", nullable: false),
                    exception = table.Column<string>(type: "text", nullable: false),
                    stack_trace = table.Column<string>(type: "text", nullable: false),
                    logger = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    permissions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "waiting_lists",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    customer = table.Column<string>(type: "text", nullable: true),
                    purpose_of_arrival = table.Column<string>(type: "text", nullable: true),
                    ship = table.Column<string>(type: "text", nullable: true),
                    route = table.Column<string>(type: "text", nullable: true),
                    camera = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    access_grant_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_waiting_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "barrier_infos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    uri = table.Column<string>(type: "text", nullable: false),
                    area_id = table.Column<int>(type: "integer", nullable: false),
                    login = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_barrier_infos", x => x.id);
                    table.ForeignKey(
                        name: "fk_barrier_infos_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    nais_code = table.Column<string>(type: "text", nullable: false),
                    area_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storages", x => x.id);
                    table.ForeignKey(
                        name: "fk_storages_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cameras",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    link = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    area_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cameras", x => x.id);
                    table.ForeignKey(
                        name: "fk_cameras_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cameras_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "time_controled_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_state_id = table.Column<int>(type: "integer", nullable: true),
                    timeout = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_controled_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_time_controled_states_car_states_car_state_id",
                        column: x => x.car_state_id,
                        principalTable: "car_states",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "user_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plate_number_forward = table.Column<string>(type: "text", nullable: false),
                    plate_number_backward = table.Column<string>(type: "text", nullable: false),
                    plate_number_similars = table.Column<string>(type: "text", nullable: true),
                    driver = table.Column<string>(type: "text", nullable: true),
                    target_area_id = table.Column<int>(type: "integer", nullable: true),
                    is_inspection_required = table.Column<bool>(type: "boolean", nullable: false),
                    first_weighing_completed = table.Column<bool>(type: "boolean", nullable: false),
                    second_weighing_completed = table.Column<bool>(type: "boolean", nullable: false),
                    storage_id = table.Column<int>(type: "integer", nullable: true),
                    car_state_id = table.Column<int>(type: "integer", nullable: true),
                    area_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cars", x => x.id);
                    table.ForeignKey(
                        name: "fk_cars_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cars_areas_target_area_id",
                        column: x => x.target_area_id,
                        principalTable: "areas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cars_car_states_car_state_id",
                        column: x => x.car_state_id,
                        principalTable: "car_states",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cars_storages_storage_id",
                        column: x => x.storage_id,
                        principalTable: "storages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "unknown_car_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    detected_plate_number = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    plate_number_picture = table.Column<byte[]>(type: "bytea", nullable: false),
                    camera_id = table.Column<int>(type: "integer", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unknown_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_unknown_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_unknown_car_notifies_cameras_camera_id",
                        column: x => x.camera_id,
                        principalTable: "cameras",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "car_detected_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    camera_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_detected_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_detected_notifies_cameras_camera_id",
                        column: x => x.camera_id,
                        principalTable: "cameras",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_detected_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "car_state_timers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_alive = table.Column<bool>(type: "boolean", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    car_state_id = table.Column<int>(type: "integer", nullable: true),
                    time_controled_state_id = table.Column<int>(type: "integer", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_state_timers", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_state_timers_car_states_car_state_id",
                        column: x => x.car_state_id,
                        principalTable: "car_states",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_state_timers_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_state_timers_time_controled_states_time_controled_state",
                        column: x => x.time_controled_state_id,
                        principalTable: "time_controled_states",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "car_waiting_list",
                columns: table => new
                {
                    cars_id = table.Column<int>(type: "integer", nullable: false),
                    waiting_lists_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_waiting_list", x => new { x.cars_id, x.waiting_lists_id });
                    table.ForeignKey(
                        name: "fk_car_waiting_list_cars_cars_id",
                        column: x => x.cars_id,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_waiting_list_waiting_lists_waiting_lists_id",
                        column: x => x.waiting_lists_id,
                        principalTable: "waiting_lists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expired_list_car_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    detected_plate_number = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    plate_number_picture = table.Column<byte[]>(type: "bytea", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    waiting_list_id = table.Column<int>(type: "integer", nullable: true),
                    camera_id = table.Column<int>(type: "integer", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expired_list_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_cameras_camera_id",
                        column: x => x.camera_id,
                        principalTable: "cameras",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_waiting_lists_waiting_list_id",
                        column: x => x.waiting_list_id,
                        principalTable: "waiting_lists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inspection_required_car_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_required_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspection_required_car_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "not_in_list_car_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    detected_plate_number = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    plate_number_picture = table.Column<byte[]>(type: "bytea", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    camera_id = table.Column<int>(type: "integer", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not_in_list_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_cameras_camera_id",
                        column: x => x.camera_id,
                        principalTable: "cameras",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_barrier_infos_area_id",
                table: "barrier_infos",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_cameras_area_id",
                table: "cameras",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_cameras_role_id",
                table: "cameras",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_detected_notifies_camera_id",
                table: "car_detected_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_detected_notifies_car_id",
                table: "car_detected_notifies",
                column: "car_id");

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
                name: "ix_car_waiting_list_waiting_lists_id",
                table: "car_waiting_list",
                column: "waiting_lists_id");

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
                name: "ix_expired_list_car_notifies_camera_id",
                table: "expired_list_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_expired_list_car_notifies_car_id",
                table: "expired_list_car_notifies",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_expired_list_car_notifies_role_id",
                table: "expired_list_car_notifies",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_expired_list_car_notifies_waiting_list_id",
                table: "expired_list_car_notifies",
                column: "waiting_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_required_car_notifies_car_id",
                table: "inspection_required_car_notifies",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_not_in_list_car_notifies_camera_id",
                table: "not_in_list_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_not_in_list_car_notifies_car_id",
                table: "not_in_list_car_notifies",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_not_in_list_car_notifies_role_id",
                table: "not_in_list_car_notifies",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_storages_area_id",
                table: "storages",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_controled_states_car_state_id",
                table: "time_controled_states",
                column: "car_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_unknown_car_notifies_camera_id",
                table: "unknown_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_unknown_car_notifies_role_id",
                table: "unknown_car_notifies",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "barrier_infos");

            migrationBuilder.DropTable(
                name: "car_detected_notifies");

            migrationBuilder.DropTable(
                name: "car_state_timers");

            migrationBuilder.DropTable(
                name: "car_waiting_list");

            migrationBuilder.DropTable(
                name: "configs");

            migrationBuilder.DropTable(
                name: "expired_list_car_notifies");

            migrationBuilder.DropTable(
                name: "inspection_required_car_notifies");

            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "not_in_list_car_notifies");

            migrationBuilder.DropTable(
                name: "unknown_car_notifies");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "time_controled_states");

            migrationBuilder.DropTable(
                name: "waiting_lists");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "cameras");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "car_states");

            migrationBuilder.DropTable(
                name: "storages");

            migrationBuilder.DropTable(
                name: "camera_roles");

            migrationBuilder.DropTable(
                name: "areas");
        }
    }
}
