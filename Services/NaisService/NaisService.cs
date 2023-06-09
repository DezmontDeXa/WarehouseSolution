using NLog;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using WaitingListsService;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;
using WarehouseConfgisService.Models;
using WarehouseConfigService;

namespace NaisServiceLibrary
{
    public class NaisService
    {
        private readonly Nais _nais;
        private readonly WaitingLists waitingListService;
        private readonly FuzzyFindCarService findCarService;
        private readonly ILogger _logger;
        private List<CarStateBase> _expectedStates;

        public NaisService(Nais nais, WaitingLists waitingListService, FuzzyFindCarService findCarService, ILogger logger)
        {
            _nais = nais;
            this.waitingListService = waitingListService;
            this.findCarService = findCarService;
            _logger = logger;

            _expectedStates = new List<CarStateBase>()
            {
                new AwaitingWeighingState(),
                new WeighingState(),
                new LoadingState(),
                new LoadingState(),
                new ExitingForChangeAreaState()
            };

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
                    var platenumber = record.PlateNumber.Replace("|", "").ToUpper().Trim().Replace(" ", "");
                    var existCar = db.Cars.FirstOrDefault(x => x.PlateNumberForward == platenumber);

                    if (existCar == null)
                    {
                        _logger.Error($"Машина ({platenumber}) не найдена в базе. Продолжаем.");
                        continue;
                    }

                    if (record.FirstWeighting != null)
                    {
                        ApplyFirstWeighting(record, existCar);
                        continue;
                    }

                    if (record.SecondWeighting != null)
                    {
                        ApplySecondWeightingOnInit(record, existCar);
                        continue;
                    }
                }
            }
        }

        private void ApplyRecord(WeightsRecord record)
        {
            var platenumber = record.PlateNumber.Replace("|", "").ToUpper();
            var existCar = findCarService.FindCar(platenumber);

            if (existCar == null)
            {
                _logger.Error($"Машина ({platenumber}) не найдена в базе.");
                return;
            }

            if (!IsExpectedState(existCar))
            {
                var state = GetCarState(existCar);
                _logger.Error($"Машина ({platenumber}) имела не ожиданный статус. Статус: {state.Name}. Продолжаем.");
                return;
            }

            var carHasFirstWeighting = existCar.FirstWeighingCompleted;
            var carHasSecondWeighting = existCar.SecondWeighingCompleted;
            var recordHasFirstWeighting = record.FirstWeighting != null;
            var recordHasSecondWeighting = record.SecondWeighting != null;

            if (!existCar.FirstWeighingCompleted && recordHasFirstWeighting)
                ApplyFirstWeighting(record, existCar);
            else if (!existCar.SecondWeighingCompleted && recordHasSecondWeighting)
                ApplySecondWeighting(record, existCar);
        }

        private void ApplyFirstWeighting(WeightsRecord record, Car car)
        {
            var exitingForChangeAreaState = new ExitingForChangeAreaState();
            var errorState = new ErrorState();
            var loadingState = new LoadingState();
            var unloadingState = new UnloadingState();

            var storage = GetStorage(record);

            if (storage == null)
            {
                car.CarStateId = errorState.Id;
                _logger.Warn($"Склад c обозначением {record.StorageName} не реализован. Статус машины изменен на \"{errorState.Name}\".");
                return;
            }

            using (var db = new WarehouseContext())
            {
                var existCar = db.Cars.Find(car.Id);

                existCar.StorageId = storage.Id;
                var naisAreaId = GetNaisAreaId();
                var accessType = waitingListService.GetAccessTypeInfo(existCar.PlateNumberForward);
                if (storage.AreaId == naisAreaId)
                {
                    SendToNaisAreaStorage(storage, existCar, accessType);
                }
                else
                {
                    SendToAnotherAreaStorage(storage, existCar, accessType);
                }

                db.SaveChanges();
            }


        }

        private void SendToAnotherAreaStorage(Storage storage, Car? existCar, CarAccessInfo accessType)
        {
            if (accessType.TopPurposeOfArrival == PurposeOfArrival.Loading || accessType.TopPurposeOfArrival == PurposeOfArrival.Unloading)
            {
                existCar.CarStateId = new ExitingForChangeAreaState().Id;
                existCar.TargetAreaId = storage.AreaId;
                _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new ExitingForChangeAreaState().Name}\".");
            }
            else
            {
                existCar.CarStateId = new ErrorState().Id;
                _logger.Error($"Машина ({existCar.PlateNumberForward}) из списка ({accessType.TopAccessTypeList?.Number}) с целью заезда ({accessType.TopPurposeOfArrival}) была отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new ErrorState().Name}\".");                
            }
        }

        private void SendToNaisAreaStorage(Storage storage, Car? existCar, CarAccessInfo accessType)
        {
            switch (accessType.TopPurposeOfArrival)
            {
                case PurposeOfArrival.Loading:
                    existCar.CarStateId = new LoadingState().Id;
                    _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new LoadingState().Name}\".");

                    break;
                case PurposeOfArrival.Unloading:
                    existCar.CarStateId = new UnloadingState().Id;
                    _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new UnloadingState().Name}\".");
                    break;

                default:
                    existCar.CarStateId = new ErrorState().Id;
                    _logger.Error($"Машина ({existCar.PlateNumberForward}) из списка ({accessType.TopAccessTypeList?.Number}) с целью заезда ({accessType.TopPurposeOfArrival}) была отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new ErrorState().Name}\".");
                    break;
            }
            existCar.TargetAreaId = null;
        }

        private static int GetNaisAreaId()
        {
            using (var configsDb = new WarehouseConfig())
            using (var db = new WarehouseContext())
                return int.Parse(configsDb.Configs.First(x => x.Key == "NaisAreaId").Value);
        }

        private static Storage GetStorage(WeightsRecord record)
        {
            using (var configsDb = new WarehouseConfig())
            using (var db = new WarehouseContext())
            {
                var storages = configsDb.Storages;

                foreach (var store in storages.OrderByDescending(x => x.NaisCode.Length))
                {
                    if (record.StorageName.Contains(store.NaisCode))
                        return store;
                }

                return null;
            }
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

        private bool IsExpectedState(Car existCar)
        {
            var state = GetCarState(existCar);

            foreach (var expectedState in _expectedStates)
            {
                if (expectedState.Id == state.Id)
                    return true;
            }

            return false;
        }

        private CarState GetCarState(Car car)
        {
            using (var db = new WarehouseContext())
            {
                return db.CarStates.FirstOrDefault(x => x.Id == car.CarStateId);
            }
        }
    }
}
