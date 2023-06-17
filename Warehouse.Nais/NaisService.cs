using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.ConfigDataBase;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.Nais;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.Nais
{
    public class NaisService
    {
        private NaisRecordsObserver _nais;
        private IWaitingListsService _waitingListService;
        private IAppSettings _settings;
        private IWarehouseDataBaseMethods _dbMethods;
        private IFindCarService _findCarService;
        private ILogger _logger;
        private readonly List<ICarStateBase> _expectedStates;

        public NaisService(NaisRecordsObserver nais, IWaitingListsService waitingListService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods, IFindCarService findCarService, ILogger logger)
        {
            _nais = nais;
            _waitingListService = waitingListService;
            _settings = settings;
            _dbMethods = dbMethods;
            _findCarService = findCarService;
            _logger = logger;

            _expectedStates = new List<ICarStateBase>()
            {
                new AwaitingFirstWeighingState(),
                new AwaitingSecondWeighingState(),
                new WeighingState(),
                new LoadingState(),
                new UnloadingState(),

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

        private void OnRecordAdded(object? sender, IWeightsRecord e)
        {
            ApplyRecord(e);
        }

        private void OnRecordModified(object? sender, IWeightsRecord e)
        {
            ApplyRecord(e);
        }

        private void ApplyExistRecords()
        {
            foreach (var record in _nais.WeightsRecords)
            {
                var platenumber = record.PlateNumber.Replace("|", "").ToUpper().Trim().Replace(" ", "");
                var existCar = _dbMethods.GetCarByPlateNumber(platenumber);

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

        private void ApplyRecord(IWeightsRecord record)
        {
            var platenumber = record.PlateNumber.Replace("|", "").ToUpper();
            var existCar = _findCarService.FindCar(platenumber);

            if (existCar == null)
            {
                _logger.Error($"Машина ({platenumber}) не найдена в базе.");
                return;
            }

            if (!IsExpectedState(existCar))
            {
                var state = _dbMethods.GetCarState(existCar);
                _logger.Error($"Машина ({platenumber}) имела не ожиданный статус. Статус: {state.Name}. Продолжаем.");
                //return;
            }

            var carHasFirstWeighting = existCar.FirstWeighingCompleted;
            var carHasSecondWeighting = existCar.SecondWeighingCompleted;
            var recordHasFirstWeighting = record.FirstWeighting != null;
            var recordHasSecondWeighting = record.SecondWeighting != null;

            if (!carHasFirstWeighting && recordHasFirstWeighting)
                ApplyFirstWeighting(record, existCar);
            else if (!carHasSecondWeighting && recordHasSecondWeighting)
                ApplySecondWeighting(record, existCar);
        }

        private void ApplyFirstWeighting(IWeightsRecord record, ICar car)
        {
            var storage = GetStorage(record);

            if (storage == null)
            {
                _dbMethods.SetCarState(car.Id, new ErrorState().Id);
                _logger.Warn($"Склад c обозначением {record.StorageName} не реализован. Статус машины изменен на \"{new ErrorState().Name}\".");
                return;
            }

            var existCar = _dbMethods.GetCarById(car.Id);
            if (existCar is null)
                throw new ArgumentNullException(nameof(existCar));

            _dbMethods.SetCarStorage(existCar, storage);
            _dbMethods.SetCarFirstWeightning(car, true);
            _dbMethods.SetCarSecondWeightning(car, false);
            var naisAreaId = GetNaisAreaId();
            var accessType = _waitingListService.GetAccessTypeInfo(existCar.PlateNumberForward);
            if (storage.AreaId == naisAreaId)
            {
                SendToNaisAreaStorage(storage, existCar, accessType);
            }
            else
            {
                SendToAnotherAreaStorage(storage, existCar, accessType);
            }
        }

        private void SendToAnotherAreaStorage(IStorage storage, ICar existCar, ICarAccessInfo accessType)
        {
            if (accessType.TopPurposeOfArrival == PurposeOfArrival.Loading || accessType.TopPurposeOfArrival == PurposeOfArrival.Unloading)
            {
                _dbMethods.SetCarState(existCar.Id, new ExitingForChangeAreaState().Id);
                _dbMethods.SetCarTargetArea(existCar, storage.AreaId);
                _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new ExitingForChangeAreaState().Name}\".");
            }
            else
            {
                _dbMethods.SetCarState(existCar.Id, new ErrorState().Id);
                _logger.Error($"Машина ({existCar.PlateNumberForward}) из списка ({accessType.TopAccessTypeList?.Number}) с целью заезда ({accessType.TopPurposeOfArrival}) была отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new ErrorState().Name}\".");
            }
        }

        private void SendToNaisAreaStorage(IStorage storage, ICar existCar, ICarAccessInfo accessType)
        {
            switch (accessType.TopPurposeOfArrival)
            {
                case PurposeOfArrival.Loading:
                    _dbMethods.SetCarState(existCar.Id, new LoadingState().Id);
                    _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new LoadingState().Name}\".");

                    break;
                case PurposeOfArrival.Unloading:
                    _dbMethods.SetCarState(existCar.Id, new UnloadingState().Id);
                    _logger.Info($"Машина ({existCar.PlateNumberForward}) отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new UnloadingState().Name}\".");
                    break;

                default:
                    _dbMethods.SetCarState(existCar.Id, new UnloadingState().Id);
                    _logger.Error($"Машина ({existCar.PlateNumberForward}) из списка ({accessType.TopAccessTypeList?.Number}) без указанной цели заезда. Была отправлена на склад {storage.Name}({storage.NaisCode}). Статус машины изменен на \"{new UnloadingState().Name}\".");
                    break;
            }
            _dbMethods.SetCarTargetArea(existCar, null);
        }

        private int GetNaisAreaId()
        {
            using (var configsDb = new WarehouseConfig(_settings))
            {
                var value = configsDb.Configs.First(x => x.Key == "NaisAreaId").Value;
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                return int.Parse(value);
            }
        }

        private IStorage GetStorage(IWeightsRecord record)
        {
            using (var configsDb = new WarehouseConfig(_settings))
                foreach (var store in configsDb.Storages.OrderByDescending(x => x.NaisCode.Length))
                    if (record.StorageName.Contains(store.NaisCode))
                        return store;

            throw new NullReferenceException($"Failed GetStorage from WeightsRecord. NaisCode: {record.StorageName}");
        }

        private void ApplySecondWeighting(IWeightsRecord record, ICar car)
        {
            _dbMethods.SetCarState(car.Id, new ExitingState().Id);
            _dbMethods.SetCarFirstWeightning(car, true);
            _dbMethods.SetCarSecondWeightning(car, true);
            _logger.Info($"Машина ({car.PlateNumberForward}) Прошла второе взвешивание. Статус машины изменен на \"{new ExitingState().Name}\".");
        }

        private void ApplySecondWeightingOnInit(IWeightsRecord record, ICar car)
        {
            _dbMethods.SetCarState(car.Id, new ExitingState().Id);
            _dbMethods.SetCarFirstWeightning(car, true);
            _dbMethods.SetCarSecondWeightning(car, true);
            _logger.Info($"Машина ({car.PlateNumberForward}) Прошла второе взвешивание. Статус машины изменен на \"{new ExitingState().Name}\".");
        }

        private bool IsExpectedState(ICar existCar)
        {
            var state = _dbMethods.GetCarState(existCar);

            foreach (var expectedState in _expectedStates)
            {
                if (expectedState.Id == state.Id)
                    return true;
            }

            return false;
        }
    }
}
