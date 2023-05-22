using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddCarNotifies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpiredListCarNotifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetectedPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    PlateNumberPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    WaitingListId = table.Column<int>(type: "int", nullable: true),
                    CameraId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpiredListCarNotifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpiredListCarNotifies_CameraRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CameraRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpiredListCarNotifies_Cameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Cameras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpiredListCarNotifies_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpiredListCarNotifies_WaitingLists_WaitingListId",
                        column: x => x.WaitingListId,
                        principalTable: "WaitingLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectionRequiredCarNotifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionRequiredCarNotifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionRequiredCarNotifies_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotInListCarNotifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetectedPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    PlateNumberPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    CameraId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotInListCarNotifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotInListCarNotifies_CameraRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CameraRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotInListCarNotifies_Cameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Cameras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotInListCarNotifies_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnknownCarNotifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetectedPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    PlateNumberPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownCarNotifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnknownCarNotifies_CameraRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CameraRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnknownCarNotifies_Cameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Cameras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredListCarNotifies_CameraId",
                table: "ExpiredListCarNotifies",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredListCarNotifies_CarId",
                table: "ExpiredListCarNotifies",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredListCarNotifies_RoleId",
                table: "ExpiredListCarNotifies",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredListCarNotifies_WaitingListId",
                table: "ExpiredListCarNotifies",
                column: "WaitingListId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionRequiredCarNotifies_CarId",
                table: "InspectionRequiredCarNotifies",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_NotInListCarNotifies_CameraId",
                table: "NotInListCarNotifies",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_NotInListCarNotifies_CarId",
                table: "NotInListCarNotifies",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_NotInListCarNotifies_RoleId",
                table: "NotInListCarNotifies",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownCarNotifies_CameraId",
                table: "UnknownCarNotifies",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownCarNotifies_RoleId",
                table: "UnknownCarNotifies",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpiredListCarNotifies");

            migrationBuilder.DropTable(
                name: "InspectionRequiredCarNotifies");

            migrationBuilder.DropTable(
                name: "NotInListCarNotifies");

            migrationBuilder.DropTable(
                name: "UnknownCarNotifies");
        }
    }
}
