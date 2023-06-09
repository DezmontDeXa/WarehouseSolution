using Microsoft.EntityFrameworkCore;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var settings = Settings.Load();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        optionsBuilder
        .UseNpgsql(settings.ConnectionString)
        .UseSnakeCaseNamingConvention();
        //.UseLazyLoadingProxies();
        
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<CarState> CarStates { get; set; }

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
