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
                    foreach (var controlledState in db.TimeControledStates.Include(x=>x.CarState))
                    {
                        foreach (var car in db.Cars.Where(x=>x.CarStateId == controlledState.CarStateId))
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
            var existTimer = db.CarStateTimers.Where(x=>x.IsAlive).FirstOrDefault(x => x.CarId == car.Id);
            if(existTimer != null)
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

        private void ProcessExistTimer(WarehouseContext db, Car car, TimeControledState controlledState, CarStateTimer existTimer)
        {
            if(car.CarStateId != controlledState.CarStateId)
            {
                existTimer.IsAlive = false;
                logger.Info($"Машине ({car.PlateNumberForward}) отключен таймер по статусу {controlledState.CarState.Name}. Причина: Смена статуса машины на {car.CarState.Name}.");
                return;
            }

            var startTime = existTimer.StartTime;
            var duration = new TimeSpan(0,0,controlledState.Timeout);
            if(startTime + duration < DateTime.Now)
            {
                existTimer.IsAlive = false;
                car.IsInspectionRequired = true;
                logger.Warn($"Машине ({car.PlateNumberForward}) требуется провести досмотр на следующем КПП. Причина: Истек таймер по статусу {controlledState.CarState.Name}.");
            }
        }

        private void CreateNewTimer(WarehouseContext db, Car car, TimeControledState controlledState)
        {
            var timer = new CarStateTimer()
            {
                IsAlive = true,
                Car = car,
                TimeControledState = controlledState,
                CarState = controlledState.CarState,
                StartTime = DateTime.Now,
            };
            db.CarStateTimers.Add(timer);
            logger.Info($"Машине ({car.PlateNumberForward}) запущен таймер по статусу {controlledState.CarState.Name}.");
        }
    }
}