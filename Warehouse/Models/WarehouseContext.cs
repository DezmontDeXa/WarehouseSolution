using Microsoft.EntityFrameworkCore;

namespace Warehouse.Models;

/// <summary>
/// For Update use
/// Scaffold-DbContext "Server=COMPUTER;Database=Warehouse;Trusted_Connection=True;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer  -OutputDir Model1s
/// In PM
/// </summary>

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

    public virtual DbSet<Camera> Cameras { get; set; }

    public virtual DbSet<CameraRole> CameraRoles { get; set; }

    public virtual DbSet<Config> Configs { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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

        modelBuilder.Entity<Camera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cameras__3214EC07D58F93B2");

            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CameraRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CameraRo__3214EC07D5F38B71");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Key }).HasName("PK__Config__AE550C2FD27C8EA8");

            entity.ToTable("Config");

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
