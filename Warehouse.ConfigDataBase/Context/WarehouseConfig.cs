using Microsoft.EntityFrameworkCore;
using Warehouse.ConfigDataBase.Models;
using Warehouse.Interfaces.AppSettings;

namespace Warehouse.ConfigDataBase.Context
{
    public class WarehouseConfig : DbContext
    {
        private IAppSettings settings;

        public WarehouseConfig(IAppSettings settings)
        {
            this.settings = settings;
        }

        public WarehouseConfig(DbContextOptions<WarehouseConfig> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            settings.Load();

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