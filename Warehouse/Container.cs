using Google.Protobuf.Collections;
using Ninject.Activation;
using Ninject.Modules;
using NLog;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.Commands;

namespace Warehouse
{
    public class Container : NinjectModule
    {
        private WarehouseDataBase _db;

        public override void Load()
        {
            InitLog();
            InitDatabase();
            InitCommands();
            InitServices();

            Bind<WarehouseDataBase>().ToConstant(_db);
            Bind<ILogger>().ToConstant(LogManager.GetCurrentClassLogger());
            Bind<WarehouseSystem>().ToSelf();
        }



        private void InitLog()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"logs/{DateTime.Now.ToString("\\yyyy-\\MM-\\dd")}.log" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }

        private void InitDatabase()
        {
            _db = new WarehouseDataBase();

            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            var plateNumber = new PlateNumber()
            {
                Value = "о123оо123"
            };
            _db.PlateNumbers.Add(plateNumber);

            var carStatus = new CarStatus()
            {
                Description = "Ожидается"
            };
            _db.Statuses.Add(carStatus);

            var car = new Car()
            {
                CarStatus = carStatus,
                PlateNumbers = new List<PlateNumber>() { plateNumber }
            };
            _db.Cars.Add(car);

            // Saves changes
            _db.SaveChanges();
        }

        private void InitCommands()
        {
            Bind<ChangeStatusCommand>().ToSelf();
        }

        private void InitServices()
        {
            throw new NotImplementedException();
        }
    }
}
