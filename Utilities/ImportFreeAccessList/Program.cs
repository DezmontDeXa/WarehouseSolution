using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedLibrary.DataBaseModels;

const string ListName = "FreeAccess";


ImportFromJson();

void ImportFromJson()
{
    var file = "FreeAccessGercena.json";
    var fileCars = JsonConvert.DeserializeObject<List<JsonCar>>(File.ReadAllText(file));

    using (var db = new WarehouseContext())
    {
        var list = db.WaitingLists.Include(x => x.Cars).FirstOrDefault(x => x.Name == ListName);
        if (list != null)
        {
            AddCars(fileCars, db, list);
        }
        else
        {

            list = new WaitingList();
            list.Name = ListName;
            list.AccessGrantType = AccessGrantType.Free;
            list.Cars = new List<Car>();
            AddCars(fileCars, db, list);
            db.WaitingLists.Add(list);
        }

        db.SaveChanges();

    }

}

void AddCars(List<JsonCar>? fileCars, WarehouseContext db, WaitingList list)
{
    foreach (var fileCar in fileCars)
    {
        var car = db.Cars.FirstOrDefault(x => x.PlateNumberForward == fileCar.nomCar.ToUpper());
        if (car == null)
        {
            list.Cars.Add(
                new Car()
                {
                    CarStateId = 0,
                    PlateNumberForward = fileCar.nomCar.ToUpper(),
                    PlateNumberBackward = fileCar.nomCar.ToUpper(),
                    Driver = fileCar.nameVod
                });
        }
        else
        {
            if (list.Cars.Any(x => x.PlateNumberForward == car.PlateNumberForward))
                continue;

            list.Cars.Add(car);
        }
    }
}


//ImportFromTxt();
void ImportFromTxt()
{
    var lines = File.ReadAllLines("FreeAccess.txt");
    using (var db = new WarehouseContext())
    {
        var list = db.WaitingLists.Include(x => x.Cars).FirstOrDefault(x => x.Name == ListName);
        if (list != null)
        {
            AddCars(lines, db, list);
        }
        else
        {

            list = new WaitingList();
            list.Name = ListName;
            list.AccessGrantType = AccessGrantType.Free;
            list.Cars = new List<Car>();
            AddCars(lines, db, list);
            db.WaitingLists.Add(list);
        }

        db.SaveChanges();

    }
    static void AddCars(string[] lines, WarehouseContext db, WaitingList? list)
    {
        foreach (var line in lines)
        {
            var car = db.Cars.FirstOrDefault(x => x.PlateNumberForward == line.ToUpper());
            if (car == null)
            {
                list.Cars.Add(
                    new Car()
                    {
                        CarStateId = 0,
                        PlateNumberForward = line.ToUpper(),
                        PlateNumberBackward = line.ToUpper(),
                    });
            }
            else
            {
                if (list.Cars.Any(x => x.PlateNumberForward == car.PlateNumberForward))
                    continue;

                list.Cars.Add(car);
            }
        }
    }
}

public class JsonCar
{
    public string nomCar { get; set; }
    public string nameVod { get; set; }
}