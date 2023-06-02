using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;

using (var db = new WarehouseContext())
{
	foreach (var car in db.Cars)
        car.IsInspectionRequired = false;

    foreach (var timer in db.CarStateTimers)
        timer.IsAlive = false;

    foreach (var car in db.Cars)
        if (car.CarStateId != new AwaitingState().Id)
            car.CarStateId = new ErrorState().Id;

    var toDayLists = db.WaitingLists.Include(x=>x.Cars).Where(x => x.Date.Value.DayOfYear == DateTime.Now.DayOfYear || x.Date.Value.DayOfYear-1 == DateTime.Now.DayOfYear).ToList();
    foreach (var list in toDayLists)
    {
        foreach(var car in list.Cars)
        {
            car.CarStateId = new AwaitingState().Id;
        }
    }

    db.SaveChanges();
}