﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Warehouse.DataBase.Context;

#nullable disable

namespace SharedLibrary.Migrations
{
    [DbContext(typeof(WarehouseContext))]
    [Migration("20230614174944_rename_CarStates_table")]
    partial class rename_CarStates_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarWaitingList", b =>
                {
                    b.Property<int>("CarsId")
                        .HasColumnType("integer")
                        .HasColumnName("cars_id");

                    b.Property<int>("WaitingListsId")
                        .HasColumnType("integer")
                        .HasColumnName("waiting_lists_id");

                    b.HasKey("CarsId", "WaitingListsId")
                        .HasName("pk_car_waiting_list");

                    b.HasIndex("WaitingListsId")
                        .HasDatabaseName("ix_car_waiting_list_waiting_lists_id");

                    b.ToTable("car_waiting_list", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.CameraRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("TypeName")
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id")
                        .HasName("pk_camera_roles");

                    b.ToTable("camera_roles", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("integer")
                        .HasColumnName("area_id");

                    b.Property<int?>("CarStateId")
                        .HasColumnType("integer")
                        .HasColumnName("car_state_id");

                    b.Property<string>("Driver")
                        .HasColumnType("text")
                        .HasColumnName("driver");

                    b.Property<bool>("FirstWeighingCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("first_weighing_completed");

                    b.Property<bool>("IsInspectionRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_inspection_required");

                    b.Property<string>("PlateNumberBackward")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("plate_number_backward");

                    b.Property<string>("PlateNumberForward")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("plate_number_forward");

                    b.Property<string>("PlateNumberSimilars")
                        .HasColumnType("text")
                        .HasColumnName("plate_number_similars");

                    b.Property<bool>("SecondWeighingCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("second_weighing_completed");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer")
                        .HasColumnName("storage_id");

                    b.Property<int?>("TargetAreaId")
                        .HasColumnType("integer")
                        .HasColumnName("target_area_id");

                    b.HasKey("Id")
                        .HasName("pk_cars");

                    b.HasIndex("PlateNumberForward")
                        .IsUnique()
                        .HasDatabaseName("ix_cars_plate_number_forward");

                    b.ToTable("cars", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.CarStateTimer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<int?>("CarStateId")
                        .HasColumnType("integer")
                        .HasColumnName("car_state_id");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_alive");

                    b.Property<long>("StartTimeTicks")
                        .HasColumnType("bigint")
                        .HasColumnName("start_time_ticks");

                    b.Property<int?>("TimeControledStateId")
                        .HasColumnType("integer")
                        .HasColumnName("time_controled_state_id");

                    b.HasKey("Id")
                        .HasName("pk_car_state_timers");

                    b.ToTable("car_state_timers", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.CarStateType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id")
                        .HasName("pk_car_state_types");

                    b.ToTable("car_state_types", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("Exception")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("exception");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<string>("Logger")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("logger");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("stack_trace");

                    b.HasKey("Id")
                        .HasName("pk_logs");

                    b.ToTable("logs", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.CarDetectedNotify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CameraId")
                        .HasColumnType("integer")
                        .HasColumnName("camera_id");

                    b.Property<int?>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processed");

                    b.HasKey("Id")
                        .HasName("pk_car_detected_notifies");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_car_detected_notifies_car_id");

                    b.ToTable("car_detected_notifies", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.ExpiredListCarNotify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CameraId")
                        .HasColumnType("integer")
                        .HasColumnName("camera_id");

                    b.Property<int?>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("DetectedPlateNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("detected_plate_number");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("direction");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processed");

                    b.Property<byte[]>("PlateNumberPicture")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("plate_number_picture");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int?>("WaitingListId")
                        .HasColumnType("integer")
                        .HasColumnName("waiting_list_id");

                    b.HasKey("Id")
                        .HasName("pk_expired_list_car_notifies");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_expired_list_car_notifies_car_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_expired_list_car_notifies_role_id");

                    b.HasIndex("WaitingListId")
                        .HasDatabaseName("ix_expired_list_car_notifies_waiting_list_id");

                    b.ToTable("expired_list_car_notifies", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.InspectionRequiredCarNotify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processed");

                    b.HasKey("Id")
                        .HasName("pk_inspection_required_car_notifies");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_inspection_required_car_notifies_car_id");

                    b.ToTable("inspection_required_car_notifies", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.NotInListCarNotify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CameraId")
                        .HasColumnType("integer")
                        .HasColumnName("camera_id");

                    b.Property<int?>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("DetectedPlateNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("detected_plate_number");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("direction");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processed");

                    b.Property<byte[]>("PlateNumberPicture")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("plate_number_picture");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_not_in_list_car_notifies");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_not_in_list_car_notifies_car_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_not_in_list_car_notifies_role_id");

                    b.ToTable("not_in_list_car_notifies", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.UnknownCarNotify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CameraId")
                        .HasColumnType("integer")
                        .HasColumnName("camera_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("DetectedPlateNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("detected_plate_number");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("direction");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processed");

                    b.Property<byte[]>("PlateNumberPicture")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("plate_number_picture");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_unknown_car_notifies");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_unknown_car_notifies_role_id");

                    b.ToTable("unknown_car_notifies", (string)null);
                });

            modelBuilder.Entity("Warehouse.DataBase.WaitingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessGrantType")
                        .HasColumnType("integer")
                        .HasColumnName("access_grant_type");

                    b.Property<string>("Camera")
                        .HasColumnType("text")
                        .HasColumnName("camera");

                    b.Property<string>("Customer")
                        .HasColumnType("text")
                        .HasColumnName("customer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("PurposeOfArrival")
                        .HasColumnType("text")
                        .HasColumnName("purpose_of_arrival");

                    b.Property<string>("Route")
                        .HasColumnType("text")
                        .HasColumnName("route");

                    b.Property<string>("Ship")
                        .HasColumnType("text")
                        .HasColumnName("ship");

                    b.HasKey("Id")
                        .HasName("pk_waiting_lists");

                    b.ToTable("waiting_lists", (string)null);
                });

            modelBuilder.Entity("CarWaitingList", b =>
                {
                    b.HasOne("Warehouse.DataBase.Car", null)
                        .WithMany()
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_car_waiting_list_cars_cars_id");

                    b.HasOne("Warehouse.DataBase.WaitingList", null)
                        .WithMany()
                        .HasForeignKey("WaitingListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_car_waiting_list_waiting_lists_waiting_lists_id");
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.CarDetectedNotify", b =>
                {
                    b.HasOne("Warehouse.DataBase.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .HasConstraintName("fk_car_detected_notifies_cars_car_id");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.ExpiredListCarNotify", b =>
                {
                    b.HasOne("Warehouse.DataBase.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .HasConstraintName("fk_expired_list_car_notifies_cars_car_id");

                    b.HasOne("Warehouse.DataBase.CameraRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_expired_list_car_notifies_camera_roles_role_id");

                    b.HasOne("Warehouse.DataBase.WaitingList", "WaitingList")
                        .WithMany()
                        .HasForeignKey("WaitingListId")
                        .HasConstraintName("fk_expired_list_car_notifies_waiting_lists_waiting_list_id");

                    b.Navigation("Car");

                    b.Navigation("Role");

                    b.Navigation("WaitingList");
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.InspectionRequiredCarNotify", b =>
                {
                    b.HasOne("Warehouse.DataBase.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .HasConstraintName("fk_inspection_required_car_notifies_cars_car_id");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.NotInListCarNotify", b =>
                {
                    b.HasOne("Warehouse.DataBase.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .HasConstraintName("fk_not_in_list_car_notifies_cars_car_id");

                    b.HasOne("Warehouse.DataBase.CameraRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_not_in_list_car_notifies_camera_roles_role_id");

                    b.Navigation("Car");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Warehouse.DataBase.Notifies.UnknownCarNotify", b =>
                {
                    b.HasOne("Warehouse.DataBase.CameraRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_unknown_car_notifies_camera_roles_role_id");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
