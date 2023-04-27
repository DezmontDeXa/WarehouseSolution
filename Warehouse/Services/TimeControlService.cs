using Microsoft.EntityFrameworkCore;
using NLog;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class TimeControlService
    {
        private List<TimeControledState> _observableStates = new List<TimeControledState>();
        private List<TimeControlCarStateItem> _observableCars = new List<TimeControlCarStateItem>();
        private readonly ILogger logger;

        public TimeControlService(ILogger logger)
        {
            using (var db = new WarehouseContext())
            {
                _observableStates = db.TimeControledStates.Include(x => x.CarState).ToList();
            }

            Task.Run(Working);
            this.logger = logger;
        }

        private void Working()
        {
            while (true)
            {
                using (var db = new WarehouseContext())
                {
                    ProcessObservedCars(db);
                    AddCarsToObserve(db);
                }
                Task.Delay(1000).Wait();
            }
        }

        private void ProcessObservedCars(WarehouseContext db)
        {
            foreach (var observedCar in _observableCars.ToArray())
            {
                var car = db.Cars.Include(x => x.CarState).First(x => x.Id == observedCar.Car.Id);

                if (CarStateChanged(db, car, observedCar))
                {
                    _observableCars.Remove(observedCar);
                    logger.Info($"{GetType().Name}: статус машины ({car.PlateNumberForward}) изменился с \"{observedCar.State.Name}\" на \"{car.CarState.Name}\". Таймер отключен.");
                    return;
                }

                if (observedCar.IsTimedOut)
                {
                    SendCarToInspect(db, observedCar.Car);
                    _observableCars.Remove(observedCar);
                    logger.Info($"{GetType().Name}: Таймер истек. Машина ({car.PlateNumberForward}) отправлена на досмотр.");
                    return;
                }
            }
        }

        private bool CarStateChanged(WarehouseContext db, Car car, TimeControlCarStateItem observedCar)
        {
            var carStateId = car.CarStateId;
            if (carStateId != observedCar.State.Id)
            {
                return true;
            }
            return false;
        }

        private void SendCarToInspect(WarehouseContext db, Car car)
        {
            db.Cars.First(x => x.Id == car.Id).IsInspectionRequired = true;
            db.SaveChanges();
        }

        private void AddCarsToObserve(WarehouseContext db)
        {
            var cars = db.Cars.Include(x => x.CarState).ToList();

            foreach (var car in cars)
            {
                if (car.IsInspectionRequired) return;
                var observableStateConfig = _observableStates.FirstOrDefault(x => x.CarStateId == car.CarStateId);
                if (observableStateConfig == null) return;
                if (_observableCars.Any(x => x.Car.Id == car.Id && x.State.Id == observableStateConfig.CarStateId)) return;
                _observableCars.Add(new TimeControlCarStateItem(car, car.CarState, observableStateConfig.Timeout));
                logger.Info($"{GetType().Name}: Запущен таймер для машины ({car.PlateNumberForward}). Статус: {observableStateConfig.CarState.Name}. Таймаут: {observableStateConfig.Timeout}");
            }
        }
    }

    public class TimeControlCarStateItem
    {
        public Car Car { get; }
        public CarState State { get; }
        public DateTime OnBegin { get; }
        public TimeSpan Timeout { get; }
        public bool IsTimedOut => DateTime.Now > OnBegin + Timeout;

        public TimeControlCarStateItem(Car car, CarState? carState, int timeout)
        {
            Car = car;
            State = carState;
            Timeout = new TimeSpan(0, 0, timeout);
            OnBegin = DateTime.Now;
        }

    }
}
