﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedLibrary.DataBaseModels;

#nullable disable

namespace SharedLibrary.Migrations
{
    [DbContext(typeof(WarehouseContext))]
    [Migration("20230520144455_AddCarStateTimesTable")]
    partial class AddCarStateTimesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarWaitingList", b =>
                {
                    b.Property<int>("CarsId")
                        .HasColumnType("int");

                    b.Property<int>("WaitingListsId")
                        .HasColumnType("int");

                    b.HasKey("CarsId", "WaitingListsId");

                    b.HasIndex("WaitingListsId");

                    b.ToTable("CarWaitingList");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.BarrierInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("BarrierInfos");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Camera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("RoleId");

                    b.ToTable("Cameras");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.CameraRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CameraRoles");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("CarStateId")
                        .HasColumnType("int");

                    b.Property<string>("Driver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FirstWeighingCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInspectionRequired")
                        .HasColumnType("bit");

                    b.Property<string>("PlateNumberBackward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlateNumberForward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlateNumberSimilars")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SecondWeighingCompleted")
                        .HasColumnType("bit");

                    b.Property<int?>("StorageId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetAreaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CarStateId");

                    b.HasIndex("StorageId");

                    b.HasIndex("TargetAreaId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.CarState", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarStates");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.CarStateTimer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int?>("CarStateId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TimeControledStateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CarStateId");

                    b.HasIndex("TimeControledStateId");

                    b.ToTable("CarStateTimers");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exception")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logger")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("NaisCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.TimeControledState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarStateId")
                        .HasColumnType("int");

                    b.Property<int>("Timeout")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarStateId");

                    b.ToTable("TimeControledStates");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Login");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Permissions")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.WaitingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessGrantType")
                        .HasColumnType("int");

                    b.Property<string>("Camera")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("PurposeOfArrival")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WaitingLists");
                });

            modelBuilder.Entity("CarWaitingList", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Car", null)
                        .WithMany()
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedLibrary.DataBaseModels.WaitingList", null)
                        .WithMany()
                        .HasForeignKey("WaitingListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.BarrierInfo", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Camera", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedLibrary.DataBaseModels.CameraRole", "CameraRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("CameraRole");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Car", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("SharedLibrary.DataBaseModels.CarState", "CarState")
                        .WithMany()
                        .HasForeignKey("CarStateId");

                    b.HasOne("SharedLibrary.DataBaseModels.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId");

                    b.HasOne("SharedLibrary.DataBaseModels.Area", "TargetArea")
                        .WithMany()
                        .HasForeignKey("TargetAreaId");

                    b.Navigation("Area");

                    b.Navigation("CarState");

                    b.Navigation("Storage");

                    b.Navigation("TargetArea");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.CarStateTimer", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("SharedLibrary.DataBaseModels.CarState", "CarState")
                        .WithMany()
                        .HasForeignKey("CarStateId");

                    b.HasOne("SharedLibrary.DataBaseModels.TimeControledState", "TimeControledState")
                        .WithMany()
                        .HasForeignKey("TimeControledStateId");

                    b.Navigation("Car");

                    b.Navigation("CarState");

                    b.Navigation("TimeControledState");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.Storage", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.TimeControledState", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.CarState", "CarState")
                        .WithMany()
                        .HasForeignKey("CarStateId");

                    b.Navigation("CarState");
                });

            modelBuilder.Entity("SharedLibrary.DataBaseModels.User", b =>
                {
                    b.HasOne("SharedLibrary.DataBaseModels.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
