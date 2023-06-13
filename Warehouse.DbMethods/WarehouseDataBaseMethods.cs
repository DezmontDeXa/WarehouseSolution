using Microsoft.EntityFrameworkCore;
using Warehouse.DataBase;
using Warehouse.DataBase.Context;
using Warehouse.DataBase.Notifies;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.DbMethods
{
    public class WarehouseDataBaseMethods : IWarehouseDataBaseMethods
    {
        private readonly IAppSettings settings;

        public WarehouseDataBaseMethods(IAppSettings settings)
        {
            this.settings = settings;
        }

        // Init

        public void RegisterCarStates(IEnumerable<ICarStateBase> carStates)
        {
            using (var db = new WarehouseContext(settings))
            {
                foreach (var state in carStates)
                {
                    var stateInDb = db.CarStates.Find(state.Id);
                    if (stateInDb == null)
                        db.CarStates.Add(new CarState() { Id = state.Id, Name = state.Name, TypeName = state.TypeName });
                }
                db.SaveChanges();
            }
        }

        public void RegisterCameraRoles(IEnumerable<ICameraRoleBase> cameraRoles)
        {
            using (var db = new WarehouseContext(settings))
            {
                foreach (var role in cameraRoles)
                {
                    var existRole = db.CameraRoles.Find(role.Id);
                    if (existRole == null)
                        db.CameraRoles.Add(new CameraRole() { Name = role.Name, Description = role.Description, TypeName = role.GetType().Name });
                    else
                    {
                        existRole.Description = role.Description;
                        existRole.Name = role.Name;
                        existRole.TypeName = role.GetType().Name;
                    }
                }

                db.SaveChanges();
            }
        }

        // Cars
        public IEnumerable<ICar> GetCars()
        {
            using (var db = new WarehouseContext(settings))
                return db.Cars.ToList();
        }

        public ICar? GetCarById(int id)
        {
            using (var db = new WarehouseContext(settings))
                return FindEntity<ICar>(db, id);
        }

        public ICar? GetCarByPlateNumber(string plateNumber)
        {
            using (var db = new WarehouseContext(settings))
            {
                foreach (var car in db.Cars.Include(x => x.WaitingLists))
                {
                    if (car == null) continue;
                    if (car.PlateNumberForward.ToLower() == plateNumber.ToLower())
                        return car;
                    if (car.PlateNumberBackward.ToLower() == plateNumber.ToLower())
                        return car;

                    if (car.PlateNumberSimilars != null)
                        foreach (var similar in car.PlateNumberSimilars.Split(new char[] { ',' }))
                            if (similar.ToLower() == plateNumber.ToLower())
                                return car;
                }

                return null;
            }
        }

        public IEnumerable<ICar> GetCarsWithWaitingLists()
        {
            using (var db = new WarehouseContext(settings))
                return db.Cars.Include(x => x.WaitingLists).ToList();
        }

        public ICarState GetCarState(ICar car)
        {
            using (var db = new WarehouseContext(settings))
                return db.CarStates.First(x => x.Id == car.CarStateId);
        }

        public void SetCarInspectionRequired(ICar car, bool value)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car.Id, out var entity))
                    ((Car)entity).IsInspectionRequired = value;
                db.SaveChanges();
            }
        }

        public void SetCarStorage(ICar car, IStorage storage)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car.Id, out var dbCar))
                    ((Car)dbCar).StorageId = storage.Id;
                db.SaveChanges();
            }
        }


        public void SetCarState(ICar car, ICarStateBase state)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car.Id, out var dbCar))
                    ((Car)dbCar).CarStateId = state.Id;
                db.SaveChanges();
            }
        }

        public void SetCarState(ICar car, ICarState state)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car.Id, out var dbCar))
                    ((Car)dbCar).CarStateId = state.Id;
                db.SaveChanges();
            }
        }

        public void SetCarState(int car, int state)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car, out var dbCar))
                    ((Car)dbCar).CarStateId = state;
                db.SaveChanges();
            }
        }

        public void SetCarTargetArea(ICar car, int? areaId)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICar>(db, car.Id, out var dbCar))
                    ((Car)dbCar).TargetAreaId = areaId;
                db.SaveChanges();
            }
        }


        // Camera Roles

        public ICameraRole GetCameraRoleById(int roleId)
        {
            using (var db = new WarehouseContext(settings))
            {
                return FindEntity<CameraRole>(db, roleId);
            }
        }


        // Timers
        public void CreateTimer(ICarStateTimer timer)
        {
            CreateEntity(timer);
        }

        public IEnumerable<ICarStateTimer> GetTimers()
        {
            using (var db = new WarehouseContext(settings))
                return db.CarStateTimers.ToList();
        }

        public void SetTimerIsAlive(ICarStateTimer timer, bool value)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (TryFindEntity<ICarStateTimer>(db, timer.Id, out var entity))
                    entity.IsAlive = value;
                db.SaveChanges();
            }
        }

        public ICarState GetTimerTargetState(ITimeControledState controlledState)
        {
            using (var db = new WarehouseContext(settings))
                return db.CarStates.First(x => x.Id == controlledState.CarStateId);
        }


        private bool TryFindEntity<T>(WarehouseContext db, int id, out T entity) where T : class
        {
            entity = FindEntity<T>(db, id);
            return entity != null;
        }

        private T? FindEntity<T>(WarehouseContext db, int id) where T : class
        {
            return db.Set<T>().Find(id);
        }

        private void CreateEntity<T>(T entity) where T : class
        {
            using (var db = new WarehouseContext(settings))
                db.Set<T>().Add(entity);
        }

        public ICarState? GetStateById(int stateId)
        {
            using (var db = new WarehouseContext(settings))
                return FindEntity<CarState>(db, stateId);
        }

        public void SendNotify<T>(T notify) where T : class
        {
            using (var db = new WarehouseContext(settings))
            {
                db.Set<T>().Add(notify);
                db.SaveChanges();
            }
        }

        public void SetCarArea(ICar? car, int? areaId)
        {
            using (var db = new WarehouseContext(settings))
            {
                var dbCar = (Car)GetCarById(car.Id);
                dbCar.AreaId = areaId;
                db.SaveChanges();
            }
        }

        #region Send Notifies

        public void SendCarDetectedNotify(ICamera camera, ICarAccessInfo carAccessInfo)
        {
            SendNotify(
                    new CarDetectedNotify()
                    {
                        CameraId = camera.Id,
                        CreatedOn = DateTime.Now,
                        Car = (Car)GetCarById(carAccessInfo.Car.Id),
                    });
        }

        public void SendUnknownCarNotify(ICamera camera, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new UnknownCarNotify()
            {
                CreatedOn = DateTime.Now,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
                CameraId = camera.Id,
                Role = (CameraRole)GetCameraRoleById(camera.RoleId)
            };
            SendNotify(notify);
        }

        public void SendNotInListCarNotify(ICamera camera, ICar car, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new NotInListCarNotify()
            {
                CameraId = camera.Id,
                CreatedOn = DateTime.Now,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
                Car = (Car)GetCarById(car.Id),
                Role = (CameraRole)GetCameraRoleById(camera.RoleId)
            };
            SendNotify(notify);
        }

        public void SendExpriredListCarNotify(ICamera camera, ICar car, IWaitingList list, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new ExpiredListCarNotify()
            {
                CameraId = camera.Id,
                CreatedOn = DateTime.Now,
                WaitingList = (WaitingList)list,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
                Car = (Car)GetCarById(car.Id),
                Role = (CameraRole)GetCameraRoleById(camera.RoleId)
            };
            SendNotify(notify);
        }

        public void SendInspectionRequiredCarNotify(ICar car)
        {
            var notify = new InspectionRequiredCarNotify()
            {
                CreatedOn = DateTime.Now,
                Car = (Car)GetCarById(car.Id),
            };
        }

        public void CreateTimer(ICar car, ITimeControledState controlledState)
        {
            var timer = new CarStateTimer()
            {
                IsAlive = true,
                CarId = car.Id,
                TimeControledStateId = controlledState.Id,
                CarStateId = controlledState.CarStateId,
                StartTimeTicks = DateTime.Now.Ticks,
            };
        }

        #endregion
    }
}
