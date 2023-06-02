using NLog;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using Warehouse.Models.CarStates.Implements;

namespace NaisServiceLibrary
{
    public class NaisService
    {
        private readonly Nais _nais;
        private readonly ILogger _logger;

        public NaisService(Nais nais, ILogger logger)
        {
            _nais = nais;
            _logger = logger;
            _nais.RecordAdded += OnRecordAdded;
            _nais.RecordModified += OnRecordModified;
        }

        public async void RunAsync()
        {
            await Task.Run(() =>
            {
                _nais.Run();
                //ApplyExistRecords();
            });
        }

        private void OnRecordAdded(object? sender, WeightsRecord e)
        {
            ApplyRecord(e);
        }

        private void OnRecordModified(object? sender, WeightsRecord e)
        {
            ApplyRecord(e);
        }

        private void ApplyExistRecords()
        {
            using (var db = new WarehouseContext())
            {
                foreach (var record in _nais.WeightsRecords)
                {
                    var platenumber = record.PlateNumber.Replace("|", "").ToUpper();
                    var existCar = db.Cars.FirstOrDefault(x => x.PlateNumberForward == platenumber);

                    if (existCar == null)
                    {
                        _logger.Error($"Машина ({platenumber}) не найдена в базе. Продолжаем.");
                        continue;
                    }

                    if (record.SecondWeighting != null)
                    {
                        ApplySecondWeightingOnInit(record, existCar);
                        continue;
                    }

                    if (record.FirstWeighting != null)
                    {
                        ApplyFirstWeighting(record, existCar, db);
                        continue;
                    }
                }
            }
        }

        private void ApplyRecord(WeightsRecord record)
        {
            using (var db = new WarehouseContext())
            {
                ApplyRecord(record, db);
            }
        }

        private void ApplyRecord(WeightsRecord record, WarehouseContext db)
        {
            var platenumber = record.PlateNumber.Replace("|", "").ToUpper();
            var existCar = db.Cars.FirstOrDefault(x => x.PlateNumberForward == platenumber);

            if (existCar == null)
            {
                _logger.Error($"Машина ({platenumber}) не найдена в базе. Продолжаем.");
                return;
            }

            if (!IsExpectedState(existCar, db))
            {
                var state = db.CarStates.First(x => x.Id == existCar.CarStateId);
                _logger.Error($"Машина ({platenumber}) имела не ожиданный статус. Статус: {state.Name}. Продолжаем.");
                return;
            }

            var carHasFirstWeighting = existCar.FirstWeighingCompleted;
            var carHasSecondWeighting = existCar.SecondWeighingCompleted;
            var recordHasFirstWeighting = record.FirstWeighting != null;
            var recordHasSecondWeighting = record.SecondWeighting != null;

            if (!existCar.FirstWeighingCompleted && recordHasFirstWeighting)
                ApplyFirstWeighting(record, existCar, db);
            else if (!existCar.SecondWeighingCompleted && recordHasSecondWeighting)
                ApplySecondWeighting(record, existCar);

            db.SaveChanges();
        }

        private void ApplyFirstWeighting(WeightsRecord record, Car existCar, WarehouseContext db)
        {
            var exitingForChangeAreaState = db.CarStates.First(x => x.TypeName == nameof(ExitingForChangeAreaState));
            var errorState = db.CarStates.First(x => x.TypeName == nameof(ErrorState));
            var loadingState = db.CarStates.First(x => x.TypeName == nameof(LoadingState));
            var storage = GetStorage(record, db);

            if (storage == null)
            {
                existCar.CarStateId = errorState.Id;
                _logger.Warn($"Склад c обозначением {record.StorageName} не реализован. Статус машины изменен на \"{errorState.Name}\".");
                return;
            }

            existCar.StorageId = storage.Id;

            var naisAreaId = int.Parse(db.Configs.First(x => x.Key == "NaisAreaId").Value);
            if (storage.AreaId == naisAreaId)
            {
                existCar.CarStateId = loadingState.Id;
                _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}. Статус машины изменен на \"{loadingState.Name}\".");
            }
            else
            {
                existCar.CarStateId = exitingForChangeAreaState.Id;
                existCar.TargetAreaId = storage.AreaId;
                _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}. Статус машины изменен на \"{exitingForChangeAreaState.Name}\".");
            }
        }

        private static Storage GetStorage(WeightsRecord record, WarehouseContext db)
        {
            var storages = db.Storages;

            foreach (var store in storages.OrderByDescending(x => x.NaisCode.Length))
            {
                if (record.StorageName.Contains(store.NaisCode))
                    return store;
            }

            return null;
        }

        private void ApplySecondWeighting(WeightsRecord record, Car existCar)
        {
            existCar.CarStateId = new ExitPassGrantedState().Id;
            _logger.Info($"Машина ({existCar.PlateNumberForward}) Прошла второе взвешивание. Статус машины изменен на \"{new ExitPassGrantedState().Name}\".");
        }

        private void ApplySecondWeightingOnInit(WeightsRecord record, Car existCar)
        {
            existCar.CarStateId = new FinishState().Id;
            _logger.Info($"Машина ({existCar.PlateNumberForward}) Прошла второе взвешивание. Статус машины изменен на \"{new FinishState().Name}\".");
        }

        private bool IsExpectedState(Car existCar, WarehouseContext db)
        {
            var state = GetCarState(existCar);

            if (state.TypeName == nameof(AwaitingWeighingState)) return true;
            if (state.TypeName == nameof(WeighingState)) return true;

            // Нужно ли уточнять что погрузка на территории весовой?
            if (state.TypeName == nameof(LoadingState)) return true;

            // Если AfterEnter камера не отработала по машине
            if (state.TypeName == nameof(OnEnterState)) return true;

            // Если на момент инициализации, машина уже вернулась с герцена
            if (state.TypeName == nameof(ExitingForChangeAreaState)) return true;


            return false;
        }

        private CarState GetCarState(Car car)
        {
            using(var db = new WarehouseContext())
            {
                return db.CarStates.FirstOrDefault(x=>x.Id == car.CarStateId);
            }
        }
    }
}
