using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedLibrary.DataBaseModels;
using SharedLibrary.Extensions;
using System.Collections.Generic;

public class ListImporter
{
    public void ImportList(List<CarData> data, string name, int number, AccessGrantType accessType)
    {
        using (var db = new WarehouseContext())
        {
            var list = new WaitingList();
            list.Name = name;
            list.Number = number;
            list.AccessGrantType = accessType;
            list.Cars = new List<Car>();
            AddCars(data, db, list);
            db.WaitingLists.Add(list);

            db.SaveChanges();
        }
    }
    private static void AddCars(List<CarData> data, WarehouseContext db, WaitingList? list)
    {
        foreach (var carData in data)
        {
            var car = db.Cars.FirstOrDefault(x => x.PlateNumberForward == carData.PlateNumber);
            if (car == null)
            {
                list.Cars.Add(
                    new Car()
                    {
                        CarStateId = 0,
                        PlateNumberForward = carData.PlateNumber,
                        PlateNumberBackward = carData.PlateNumberBackward,
                        Driver = carData.DriverName
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

public class TxtFileParser : FileParser
{
    public override List<CarData> ParseFile(string file)
    {
        var lines = File.ReadAllLines(file);
        return lines.Select(x => new CarData(x, "", "")).ToList();
    }
}

public class JsonFileParser : FileParser
{
    public override List<CarData> ParseFile(string file)
    {
        var fileCars = JsonConvert.DeserializeObject<List<JsonCar>>(File.ReadAllText(file));
        return fileCars.Select(x => new CarData(x.nomCar, x.nameVod, x.nomPric)).ToList();        
    }

    private class JsonCar
    {
        public string nomCar { get; set; }
        public string? nameVod { get; set; }
        public string? nomPric { get; set; }
    }
}

public abstract class FileParser
{
    public abstract List<CarData> ParseFile(string file);
}

public class CarData
{
    public CarData(string plateNumber, string driverName, string plateNumberBackward)
    {
        PlateNumber = StringExtensions.TransliterateToRu(plateNumber.ToUpper());
        PlateNumberBackward = StringExtensions.TransliterateToRu(plateNumberBackward?.ToUpper()) ?? "";
        DriverName = driverName;
    }

    public string PlateNumber { get;  }
    public string PlateNumberBackward { get; }
    public string DriverName { get; }
}