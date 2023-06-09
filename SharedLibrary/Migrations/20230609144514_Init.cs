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
                name: "camera",
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
                    table.PrimaryKey("pk_camera", x => x.id);
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
                name: "car_state_timers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_alive = table.Column<bool>(type: "boolean", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    car_state_id = table.Column<int>(type: "integer", nullable: true),
                    time_controled_state_id = table.Column<int>(type: "integer", nullable: true),
                    start_time_ticks = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_state_timers", x => x.id);
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
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                name: "waiting_lists",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    access_grant_type = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    customer = table.Column<string>(type: "text", nullable: true),
                    purpose_of_arrival = table.Column<string>(type: "text", nullable: true),
                    ship = table.Column<string>(type: "text", nullable: true),
                    route = table.Column<string>(type: "text", nullable: true),
                    camera = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_waiting_lists", x => x.id);
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
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unknown_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_unknown_car_notifies_camera_camera_id",
                        column: x => x.camera_id,
                        principalTable: "camera",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_unknown_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
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
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_detected_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_detected_notifies_camera_camera_id",
                        column: x => x.camera_id,
                        principalTable: "camera",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_detected_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inspection_required_car_notifies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not_in_list_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_camera_camera_id",
                        column: x => x.camera_id,
                        principalTable: "camera",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_not_in_list_car_notifies_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
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
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expired_list_car_notifies", x => x.id);
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_camera_camera_id",
                        column: x => x.camera_id,
                        principalTable: "camera",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_expired_list_car_notifies_camera_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "camera_roles",
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

            migrationBuilder.CreateIndex(
                name: "ix_car_detected_notifies_camera_id",
                table: "car_detected_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_detected_notifies_car_id",
                table: "car_detected_notifies",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_car_waiting_list_waiting_lists_id",
                table: "car_waiting_list",
                column: "waiting_lists_id");

            migrationBuilder.CreateIndex(
                name: "ix_cars_plate_number_forward",
                table: "cars",
                column: "plate_number_forward",
                unique: true);

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
                name: "ix_unknown_car_notifies_camera_id",
                table: "unknown_car_notifies",
                column: "camera_id");

            migrationBuilder.CreateIndex(
                name: "ix_unknown_car_notifies_role_id",
                table: "unknown_car_notifies",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_detected_notifies");

            migrationBuilder.DropTable(
                name: "car_state_timers");

            migrationBuilder.DropTable(
                name: "car_states");

            migrationBuilder.DropTable(
                name: "car_waiting_list");

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
                name: "waiting_lists");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "camera");

            migrationBuilder.DropTable(
                name: "camera_roles");
        }
    }
}
