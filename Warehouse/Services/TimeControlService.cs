using NLog;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class TimeControlService
    {
        // Выезд разрешен
        private int _exitPassGrantedStateId = 12;
        // Ожидается на герцена
        private int _awaitingOnGercenaStateId = 2;
        // Досмотр
        private int _inspectionStateId = 11;

        private Dictionary<Car, DateTime> _inControlCars = new Dictionary<Car, DateTime>();
        private TimeSpan _armavirToHercenaTranslateTimeout;
        private readonly ILogger logger;

        public TimeControlService(ILogger logger)
        {
            using (var db = new WarehouseContext())
            {
                _armavirToHercenaTranslateTimeout = new TimeSpan(0,0, int.Parse(db.Configs.First(x => x.Key == "TranslateTimeout").Value));
            }

            Task.Run(Working);
            this.logger = logger;
        }
              
        private void Working()
        {
            using (WarehouseContext db = new WarehouseContext())
            {
                while (true)
                {
                    AddCarsToControl(db);
                    RemoveCarsFromControl(db);
                    CheckTimes(db);
                    Task.Delay(1000).Wait();
                }
            }
        }

        private void CheckTimes(WarehouseContext db)
        {
            foreach (var carToTimePair in _inControlCars.ToArray())
            {
                if(carToTimePair.Value + _armavirToHercenaTranslateTimeout < DateTime.Now)
                {
                    var carInDb = db.Cars.First(x => x.Id == carToTimePair.Key.Id);

                    if (carInDb.CarStateId == _exitPassGrantedStateId)
                    {
                        _inControlCars.Remove(carToTimePair.Key);
                        carInDb.CarStateId = _inspectionStateId;
                        db.SaveChanges();
                        logger.Info($"{GetType().Name}: машина ({carInDb.PlateNumberForward}) задержалась при выезде с Армавирской. Назначен досмотр. Таймер отключен.");
                    }
                }
            }
        }

        private void AddCarsToControl(WarehouseContext db)
        {
            foreach (var car in db.Cars.Where(x => x.CarStateId == _exitPassGrantedStateId && x.CarStateContext != null))
            {
                if (_inControlCars.ContainsKey(car)) continue;
                _inControlCars.Add(car, DateTime.Now);
                logger.Info($"{GetType().Name}: запущен таймер выезда для машины ({car.PlateNumberForward}). Контекст: {car.CarStateContext}");
            }
        }

        private void RemoveCarsFromControl(WarehouseContext db)
        {
            foreach (var carToTimePair in _inControlCars)
            {
                var carInDb = db.Cars.First(x => x.Id == carToTimePair.Key.Id);
                // Если машина выехала с с армавирской на Герцена
                if (carInDb.CarStateId == _awaitingOnGercenaStateId)
                {
                    _inControlCars.Remove(carToTimePair.Key);
                    logger.Info($"{GetType().Name}: машина ({carInDb.PlateNumberForward}) выехала с Армавирской до Герцена. Таймер отключен.");
                }
            }
        }
    }
}
