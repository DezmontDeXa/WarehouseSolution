using CameraListenerService;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Tests
{
    public class WarehouseSystemTests
    {
        const string testCarPlateNumber = "P302AC193";
        const int testCarId = 1;
        const int testCarDefaultStateId = 0;
        const int delay = 1000;

        private readonly WarehouseContext _db; 
        private readonly IEnumerable<CameraListener> _cameraListeners;

        public WarehouseSystemTests(WarehouseContext db, IEnumerable<CameraListener> cameraListeners)
        {            
            _db = db;
            _cameraListeners = cameraListeners;
        }

        public void RunNormalPipelineTest()
        {
            Task.Run(() =>
            {
                var testCar = _db.Cars.First(x => x.PlateNumberForward == testCarPlateNumber && x.Id == testCarId);
                testCar.CarStateId = testCarDefaultStateId;
                _db.SaveChanges();

                Task.Delay(delay).Wait();

                foreach (var listener in _cameraListeners)
                {
                    listener.SendTestData();
                    Task.Delay(delay).Wait();
                }
            });
        }
    }
}
