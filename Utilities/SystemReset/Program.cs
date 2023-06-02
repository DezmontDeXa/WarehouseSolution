using SharedLibrary.DataBaseModels;

using (var db = new WarehouseContext())
{
	foreach (var car in db.Cars)
        car.IsInspectionRequired = false;

    foreach (var timer in db.CarStateTimers)
        timer.IsAlive = false;

    db.SaveChanges();
}