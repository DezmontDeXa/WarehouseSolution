using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedLibrary.AppSettings;

namespace SharedLibrary.DataBaseModels;

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public DbSet<Area> Areas { get; set; }

    public DbSet<BarrierInfo> BarrierInfos { get; set; }

    public DbSet<Camera> Cameras { get; set; }

    public DbSet<CameraRole> CameraRoles { get; set; }

    public DbSet<Car> Cars { get; set; }

    public DbSet<CarState> CarStates { get; set; }

    public DbSet<Config> Configs { get; set; }

    public DbSet<Log> Logs { get; set; }

    public DbSet<WaitingList> WaitingLists { get; set; }

    public DbSet<TimeControledState> TimeControledStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json")
            .Build();

        // Get values from the config given their key and their target type.
        var settings = config.GetSection("Settings").Get<Settings>();
        //"Server=COMPUTER;Database=Warehouse;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=True;"
        optionsBuilder
        .UseSqlServer(settings.ConnectionString)
        .UseLazyLoadingProxies();
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Area>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Areas__3214EC079C9E7688");

    //        entity.Property(e => e.Name)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<BarrierInfo>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Barriers__3214EC07B1726FF3");

    //        entity.Property(e => e.Id).ValueGeneratedNever();
    //        entity.Property(e => e.Login)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Password)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Uri)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Camera>(entity =>
    //    {
    //        entity.Property(e => e.Endpoint)
    //            .HasMaxLength(150)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Ip)
    //            .HasMaxLength(255)
    //            .IsUnicode(false)
    //            .HasColumnName("IP");
    //        entity.Property(e => e.Login)
    //            .HasMaxLength(150)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Password)
    //            .HasMaxLength(150)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<CameraRole>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__CameraRo__3214EC07D5F38B71");

    //        entity.Property(e => e.Description)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.TypeName)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Car>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__Cars__3214EC07B15FA2B1");

    //        entity.Property(e => e.Id).ValueGeneratedNever();
    //        entity.Property(e => e.PlateNumberBackward).HasMaxLength(15);
    //        entity.Property(e => e.PlateNumberForward).HasMaxLength(15);
    //    });

    //    modelBuilder.Entity<CarState>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__CarState__3214EC074EA2928A");

    //        entity.Property(e => e.Id).ValueGeneratedNever();
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Config>(entity =>
    //    {
    //        entity.HasKey(e => new { e.Id, e.Key }).HasName("PK__Config__AE550C2FD27C8EA8");

    //        entity.ToTable("Config");

    //        entity.Property(e => e.Id).ValueGeneratedOnAdd();
    //        entity.Property(e => e.Key)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Value)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<WaitingList>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("PK__WaitingL__3214EC07C8407679");

    //        entity.Property(e => e.Id).ValueGeneratedNever();
    //    });

    //    modelBuilder.Entity<WaitingListToCar>(entity =>
    //    {
    //        entity.HasKey(e => new { e.WaitingListId, e.CarId }).HasName("PK__WaitingL__C689F795BE6769E9");

    //        entity.ToTable("WaitingListToCar");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
