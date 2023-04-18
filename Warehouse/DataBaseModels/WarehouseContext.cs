using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.DataBaseModels;

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<BarrierInfo> BarrierInfos { get; set; }

    public virtual DbSet<Camera> Cameras { get; set; }

    public virtual DbSet<CameraRole> CameraRoles { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=COMPUTER;Database=Warehouse;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Areas__3214EC079C9E7688");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BarrierInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Barriers__3214EC07B1726FF3");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Camera>(entity =>
        {
            entity.Property(e => e.Endpoint)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.Login)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CameraRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CameraRo__3214EC07D5F38B71");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Key }).HasName("PK__Config__AE550C2FD27C8EA8");

            entity.ToTable("Config");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Key)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
