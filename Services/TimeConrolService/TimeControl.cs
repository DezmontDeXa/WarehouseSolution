using Microsoft.EntityFrameworkCore;
using NLog;
using SharedLibrary.DataBaseModels;

namespace TimeControlService
{
    public class TimeControl
    {
        private readonly ILogger logger;

        public TimeControl(ILogger logger)
        {
            this.logger = logger;
        }

        public async void RunAsync()
        {
            await Task.Run(Working);
        }

        private void Working()
        {
            while (true)
            {
                using (var db = new WarehouseContext())
                {
                    foreach (var controlledState in db.TimeControledStates.ToList())
                    {
                        foreach (var car in db.Cars.Where(x => x.CarStateId == controlledState.CarStateId).ToList())
                        {
                            ProcessCar(db, car, controlledState);
                        }
                    }
                    db.SaveChanges();
                }

                Task.Delay(2000).Wait();
            }
        }

        private void ProcessCar(WarehouseContext db, Car car, TimeControledState controlledState)
        {
            var existTimer = db.CarStateTimers.Where(x => x.IsAlive).FirstOrDefault(x => x.CarId == car.Id);
            if (existTimer != null)
            {
                ProcessExistTimer(db, car, controlledState, existTimer);
            }
            else
            {
                if (car.IsInspectionRequired)
                    return;

                CreateNewTimer(db, car, controlledState);
            }
        }

        private void ProcessExistTimer(WarehouseContext db, Car car, TimeControledState timeControlledState, CarStateTimer existTimer)
        {
            var carState = GetCarState(car);
            var controlledState = GetControlledCarState(timeControlledState);

            if (car.CarStateId != timeControlledState.CarStateId)
            {
                existTimer.IsAlive = false;
                logger.Info($"Машине ({car.PlateNumberForward}) отключен таймер по статусу {controlledState.Name}. Причина: Смена статуса машины на {carState.Name}.");
                return;
            }

            var startTime = existTimer.StartTime;
            var duration = new TimeSpan(0, 0, timeControlledState.Timeout);
            if (startTime + duration < DateTime.Now)
            {
                existTimer.IsAlive = false;
                car.IsInspectionRequired = true;
                logger.Warn($"Машине ({car.PlateNumberForward}) требуется провести досмотр на следующем КПП. Причина: Истек таймер по статусу {controlledState.Name}.");
            }
        }

        private CarState GetCarState(Car car)
        {
            using (var db = new WarehouseContext())
                return db.CarStates.First(x => x.Id == car.CarStateId);
        }

        private CarState GetControlledCarState(TimeControledState controlledState)
        {
            using (var db = new WarehouseContext())
                return db.CarStates.First(x => x.Id == controlledState.Id);
        }

        private void CreateNewTimer(WarehouseContext db, Car car, TimeControledState controlledState)
        {
            var timer = new CarStateTimer()
            {
                IsAlive = true,
                CarId = car.Id,
                TimeControledStateId = controlledState.Id,
                CarStateId = controlledState.CarStateId,
                StartTime = DateTime.Now,
            };
            var controlledCarState = GetControlledCarState(controlledState);
            db.CarStateTimers.Add(timer);
            logger.Info($"Машине ({car.PlateNumberForward}) запущен таймер по статусу {controlledCarState.Name}.");
        }
    }
}