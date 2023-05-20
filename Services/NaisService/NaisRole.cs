using NLog;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using Warehouse.Models.CarStates.Implements;

namespace NaisService
{
    public class NaisRole
    {
        private readonly Nais _nais;
        private readonly ILogger _logger;

        public NaisRole(Nais nais, ILogger logger)
        {
            _nais = nais;
            _logger = logger;
            _nais.RecordAdded += OnRecordAdded;
            _nais.RecordModified += OnRecordModified;
        }

        public void Run()
        {
            _nais.Run();
            ApplyExistRecords();
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

                    if(record.FirstWeighting != null)
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
                _logger.Error($"Машина ({platenumber}) имела не ожиданный статус. Статус: {existCar.CarState.Name}. Продолжаем.");
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
                existCar.CarState = errorState;
                _logger.Warn($"Склад c обозначением {record.StorageName} не реализован. Статус машины изменен на \"{errorState.Name}\".");
                return;
            }

            existCar.Storage = storage;

            var naisAreaId = int.Parse(db.Configs.First(x => x.Key == "NaisAreaId").Value);
            if (storage.Area.Id == naisAreaId)
            {
                existCar.CarState = loadingState;
                _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}. Статус машины изменен на \"{loadingState.Name}\".");
            }
            else
            {
                existCar.CarState = exitingForChangeAreaState;
                existCar.TargetArea = storage.Area;
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
            if (existCar.CarState.TypeName == nameof(AwaitingWeighingState)) return true;
            if (existCar.CarState.TypeName == nameof(WeighingState)) return true;
            return false;
        }

    }
}
