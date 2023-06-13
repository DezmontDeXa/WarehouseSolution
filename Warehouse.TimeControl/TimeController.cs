using NLog;
using Warehouse.ConfigDataBase.Context;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.TimeControl;

namespace Warehouse.TimeControl
{
    public class TimeController : ITimeControler
    {
        private readonly ILogger logger;
        private readonly IAppSettings settings;
        private readonly IWarehouseDataBaseMethods dbMethods;

        public TimeController(ILogger logger, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
        {
            this.logger = logger;
            this.settings = settings;
            this.dbMethods = dbMethods;
        }

        public async void RunAsync()
        {
            await Task.Run(Working);
        }

        private void Working()
        {
            while (true)
            {
                using (var configsDb = new WarehouseConfig(settings))
                    foreach (var controlledState in configsDb.TimeControledStates.ToList())
                        foreach (var car in dbMethods.GetCars().Where(x => x.CarStateId == controlledState.CarStateId))
                            ProcessCar(car, controlledState);

                Task.Delay(2000).Wait();
            }
        }

        private void ProcessCar(ICar car, ITimeControledState controlledState)
        {
            var existTimer = dbMethods.GetTimers().Where(t => t.IsAlive).FirstOrDefault(x => x.CarId == car.Id);
            if (existTimer != null)
            {
                ProcessExistTimer(car, controlledState, existTimer);
            }
            else
            {
                if (car.IsInspectionRequired)
                    return;

                CreateNewTimer(car, controlledState);
            }
        }

        private void ProcessExistTimer(ICar car, ITimeControledState timeControlledState, ICarStateTimer existTimer)
        {
            var carState = dbMethods.GetCarState(car);
            var controlledState = dbMethods.GetTimerTargetState(timeControlledState);

            if (car.CarStateId != timeControlledState.CarStateId)
            {
                dbMethods.SetTimerIsAlive(existTimer, false);
                logger.Info($"Машине ({car.PlateNumberForward}) отключен таймер по статусу {controlledState.Name}. Причина: Смена статуса машины на {carState.Name}.");
                return;
            }

            var startTime = new DateTime(existTimer.StartTimeTicks);
            var duration = new TimeSpan(0, 0, timeControlledState.Timeout);
            if (startTime + duration < DateTime.Now)
            {
                dbMethods.SetTimerIsAlive(existTimer, false);
                dbMethods.SetCarInspectionRequired(car, true);
                logger.Debug($"StartTime ({startTime}) + Duration ({duration}) <  DateTime({DateTime.UtcNow.SetKindUtc()})");
                logger.Warn($"Машине ({car.PlateNumberForward}) требуется провести досмотр на следующем КПП. Причина: Истек таймер по статусу {controlledState.Name}.");
            }
        }

        private void CreateNewTimer(ICar car, ITimeControledState controlledState)
        {            
            dbMethods.CreateTimer(car, controlledState);
            var controlledCarState = dbMethods.GetTimerTargetState(controlledState);
            logger.Info($"Машине ({car.PlateNumberForward}) запущен таймер по статусу {controlledCarState.Name}.");
        }
    }
}