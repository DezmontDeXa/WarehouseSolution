using SharedLibrary.DataBaseModels;

namespace WarehouseContextMethodsService
{
    public static class WarehouseContextMethods
    {

        protected void ChangeCarStatus(Camera camera, int carId, int stateId)
        {
            Car? car = null;
            CarState? state = null;
            try
            {
                using (var db = new WarehouseContext())
                {
                    car = db.Cars.Find(carId);
                    state = db.CarStates.Find(stateId);
                    car.CarStateId = stateId;
                    db.SaveChanges();

                    Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить статус на \"{state.Name}\"");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{camera.Name}:\t Не удалось сменить статус машины ({car?.PlateNumberForward}, {state?.Name}). {ex.Message}");
                return;
            }
        }

        protected void SetCarErrorStatus(Camera camera, int carId)
        {
            ChangeCarStatus(camera, carId, new ErrorState().Id);
        }

        protected void SetCarArea(Camera camera, int carId, int? areaId)
        {
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.Find(carId);
                car.AreaId = areaId;

                Area? area = null;
                if (areaId != null)
                    area = db.Areas.Find(areaId);

                db.SaveChanges();

                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected void SetCarTargetArea(Camera camera, int carId, int? areaId)
        {
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.Find(carId);
                car.TargetAreaId = areaId;

                Area? area = null;
                if (areaId != null)
                    db.Areas.Find(areaId);

                db.SaveChanges();
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить целевую территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected static Area? GetCameraArea(Camera camera)
        {
            using (var db = new WarehouseContext())
                return db.Areas.Find(camera.AreaId);
        }

        protected static Area? GetCarTargetArea(Car car)
        {
            using (var db = new WarehouseContext())
                return db.Areas.Find(car.TargetAreaId);
        }
    }
}