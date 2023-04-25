﻿using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Tests
{
    public class WarehouseSystemTests
    {
        const string testCarPlateNumber = "P302AC193";
        const int testCarId = 1;
        const int testCarDefaultStateId = 1;
        const int delay = 3000;

        private readonly WarehouseContext _db; 
        private readonly IEnumerable<CameraListenerService> _cameraListeners;

        public WarehouseSystemTests(WarehouseContext db, IEnumerable<CameraListenerService> cameraListeners)
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