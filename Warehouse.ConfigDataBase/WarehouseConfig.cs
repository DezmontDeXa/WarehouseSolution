using Microsoft.EntityFrameworkCore;
using Warehouse.DataBase.Models.Config;
using Warehouse.Interfaces.AppSettings;

namespace Warehouse.ConfigDataBase
{
    public class WarehouseConfig : DbContext
    {
        private string connectionString;

        public WarehouseConfig()
        {
            connectionString = File.ReadAllText("C:\\Users\\DezmontDeXa\\source\\repos\\WarehouseSolution\\Warehouse.ConfigDataBase\\ConnectionString.json");
        }

        public WarehouseConfig(IAppSettings settings)
        {
            connectionString = settings.ConfigConnectionString;
        }

        public WarehouseConfig(DbContextOptions<WarehouseConfig> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            optionsBuilder
            .UseNpgsql(connectionString)
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