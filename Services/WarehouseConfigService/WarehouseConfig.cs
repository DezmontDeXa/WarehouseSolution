using Microsoft.EntityFrameworkCore;
using SharedLibrary.AppSettings;
using WarehouseConfgisService.Models;

namespace WarehouseConfigService
{
    public class WarehouseConfig : DbContext
    {
        public WarehouseConfig()
        {
        }

        public WarehouseConfig(DbContextOptions<WarehouseConfig> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settings = Settings.Load();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            optionsBuilder
            .UseNpgsql(settings.ConfigConnectionString)
            .UseSnakeCaseNamingConvention();
            //.UseLazyLoadingProxies();

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<BarrierInfo> BarrierInfos { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Config> Configs { get; set; }

        public DbSet<TimeControledState> TimeControledStates { get; set; }

        public DbSet<Storage> Storages { get; set; }
    }
}