using ImportWaitingLists;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataBaseModels;

public class SystemResetAndInitializeService
{
    public void ResetAndInitialize()
    {
        using (var db = new WarehouseContext())
        {
            ClearDataBase(db);

            Console.WriteLine("Cleared. Any key for import start data");
            Console.ReadKey();

            ImportLists(db);
            db.SaveChanges();
        }
    }

    private static void ClearDataBase(WarehouseContext db)
    {
        var tables = new List<string>()
        {
            "logs",
            "car_detected_notifies",
            "expired_list_car_notifies",
            "inspection_required_car_notifies",
            "not_in_list_car_notifies",
            "unknown_car_notifies",
            "car_state_timers",
            "waiting_lists",
            "car_waiting_list",
            "cars",
        };

        foreach (var table in tables)
        {
            try
            {
                if (table == null) continue;
                Console.WriteLine($"Trancate {table}...");
                db.Database.ExecuteSqlRaw($"TRUNCATE TABLE {table}");
                db.SaveChanges();
            }catch(Exception e)
            {
                Console.WriteLine( e.Message);
                continue;
            }
        }
    }

    private static void ImportLists(WarehouseContext db)
    {
        Console.WriteLine("Import free access lists...");
        ImportFreeLists();

        Console.WriteLine("Import temp access lists...");
        ImportTempLists();
    }

    private static void ImportFreeLists()
    {
        var allLists = new List<List<CarData>>();
        allLists.Add(new JsonFileParser().ParseFile("AppData/FreeAccess.json"));
        allLists.Add(new JsonFileParser().ParseFile("AppData/FreeAccessGercena.json"));
        allLists.Add(new TxtFileParser().ParseFile("AppData/FreeAccess.txt"));

        var importer = new ListImporter();

        importer.ImportList(allLists[0], "FreeAccess", 0, AccessGrantType.Free);
        importer.ImportList(allLists[1], "FreeAccessGercena", 1, AccessGrantType.Free);
        importer.ImportList(allLists[2], "FreeAccess_1", 2, AccessGrantType.Free);

    }

    private static void ImportTempLists()
    {
        var folder = System.Configuration.ConfigurationManager.AppSettings["TempListsSourceFolder"];
        Importer.Import(folder);
    }
}

