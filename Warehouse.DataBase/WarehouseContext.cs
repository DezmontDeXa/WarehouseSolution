using Microsoft.EntityFrameworkCore;
using Warehouse.DataBase.Models.Main;
using Warehouse.DataBase.Models.Main.Notifies;
using Warehouse.Interfaces.AppSettings;

namespace Warehouse.DataBase;

public partial class WarehouseContext : DbContext
{
    private readonly string connectionString;

    //public WarehouseContext()
    //{
    //    connectionString = File.ReadAllText("C:\\Users\\DezmontDeXa\\source\\repos\\WarehouseSolution\\Warehouse.DataBase\\ConnectionString.json");
    //}

    public WarehouseContext(IAppSettings appSettings)
    {
        connectionString = appSettings.ConnectionString;
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        optionsBuilder
        .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention();
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<CarStateType> CarStateTypes { get; set; }

    public DbSet<Log> Logs { get; set; }

    public DbSet<CameraRole> CameraRoles { get; set; }

    public DbSet<WaitingList> WaitingLists { get; set; }

    public DbSet<CarStateTimer> CarStateTimers { get; set; }




    public DbSet<ExpiredListCarNotify> ExpiredListCarNotifies { get; set; }

    public DbSet<InspectionRequiredCarNotify> InspectionRequiredCarNotifies { get; set; }

    public DbSet<NotInListCarNotify> NotInListCarNotifies { get; set; }

    public DbSet<UnknownCarNotify> UnknownCarNotifies { get; set; }

    public DbSet<CarDetectedNotify> CarDetectedNotifies { get; set; }

}
