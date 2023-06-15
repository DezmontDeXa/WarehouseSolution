using System.Xml;
using Warehouse.AppSettings;
using Warehouse.DataBase;
using Warehouse.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.AppSettings;
using Warehouse.RusificationServices;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.ListsImporterServices;

namespace Warehouse.Utilities.WaitingListsImporterProject
{
    public class WaitingListImporterService : IWaitingListImporterService
    {
        private IRussificationService _ruService = new RussificationService();
        private IAppSettings settings;

        public WaitingListImporterService()
        {
            settings = new DefaultAppSettings();
            settings.Load();
        }

        private readonly Dictionary<string, int> _cameraToAreaId = new Dictionary<string, int>()
        {
            {"Пропуск Б.Хмельницкого", 1 },
            {"Въезд герцена", 2 },
        };

        public void ImportList(AccessGrantType accessGrantType, string xmlFileContent)
        {
            xmlFileContent = FixContent(xmlFileContent);
            var result = ParseFile(xmlFileContent);
            result.AccessGrantType = accessGrantType;
            result.Name = $"{accessGrantType}({result.Number})";
            ImportToDataBase(result);
        }

        public void ImportList(AccessGrantType accessGrantType, FileInfo xmlFileInfo)
        {
            ImportList(accessGrantType, File.ReadAllText(xmlFileInfo.FullName));
        }

        private string FixContent(string xmlFileContent)
        {
            xmlFileContent = xmlFileContent.Replace("<?", "<").Replace("?>", ">");
            xmlFileContent = xmlFileContent.Trim();
            if (!xmlFileContent.EndsWith("</xml>"))
                xmlFileContent = xmlFileContent + "</xml>";
            return xmlFileContent;
        }

        private WaitingList ParseFile(string xmlFileContent)
        {
            var result = new WaitingList();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlFileContent);
            var xmlDocumentRoot = xmlDocument.DocumentElement["СписокПогрузкиРазгрузки"];

            result.Number = int.Parse(xmlDocumentRoot.GetAttribute("Номер").Trim());
            result.Date = DateTime.Parse(xmlDocumentRoot.GetAttribute("Дата").Trim());
            result.Customer = xmlDocumentRoot.GetAttribute("Заказчик").Trim();
            result.Camera = xmlDocumentRoot.GetAttribute("Камера").Trim();
            result.PurposeOfArrival = xmlDocumentRoot.GetAttribute("ЦельЗаезда").Trim();
            result.Ship = xmlDocumentRoot.GetAttribute("Судно").Trim();
            result.Route = xmlDocumentRoot.GetAttribute("Маршрут").Trim();
            result.Cars = new List<Car>();

            foreach (XmlNode node in xmlDocumentRoot.SelectNodes("//ТаблицаСписокТС"))
            {
                var car = new Car();
                car.PlateNumberForward = _ruService.ToRu(node.Attributes["НомерТС"].Value.ToUpper());
                car.PlateNumberBackward = _ruService.ToRu(node.Attributes["Прицеп"].Value.ToUpper());
                car.Driver = node.Attributes["Водитель"].Value;
                car.CarStateId = 0;
                if(result.Camera != null && result.Camera != "")
                car.TargetAreaId = _cameraToAreaId[result.Camera];
                result.Cars.Add(car);
            }

            return result;
        }

        private void ImportToDataBase(WaitingList list)
        {
            using (var db = new WarehouseContext(settings))
            {
                if (db.WaitingLists.Any(x => x.Number == list.Number))
                    return;

                foreach (var car in list.Cars.DistinctBy(x => x.PlateNumberForward))
                {
                    var existCar = db.Cars.AsNoTracking().FirstOrDefault(x => x.PlateNumberForward == car.PlateNumberForward);
                    if (existCar != null)
                        car.Id = existCar.Id;
                }

                if (list.PurposeOfArrival == "Постоянный")
                    list.AccessGrantType = AccessGrantType.Free;


                db.WaitingLists.Attach(list);
                db.SaveChanges();
            }
        }
    }
}
