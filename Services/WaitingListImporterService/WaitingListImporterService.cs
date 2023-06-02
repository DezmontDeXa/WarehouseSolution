using SharedLibrary.DataBaseModels;
using SharedLibrary.Extensions;
using SharedLibrary.Interfaces.Services;
using System.Xml;

namespace AdminClient.Services
{
    public class WaitingListImporterService : IWaitingListImporterService
    {
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
                car.PlateNumberForward = StringExtensions.TransliterateToRu(node.Attributes["НомерТС"].Value);
                car.PlateNumberBackward = StringExtensions.TransliterateToRu(node.Attributes["Прицеп"].Value);
                car.Driver = node.Attributes["Водитель"].Value;
                car.CarStateId = 0;
                car.TargetAreaId = _cameraToAreaId[result.Camera];
                result.Cars.Add(car);
            }

            return result;
        }

        private void ImportToDataBase(WaitingList list)
        {
            using (var db = new WarehouseContext())
            {
                if (db.WaitingLists.Any(x => x.Number == list.Number))
                    return;

                var cars = list.Cars;
                list.Cars.Clear();
                var waitingListEntity = db.WaitingLists.Add(list);

                foreach (var car in cars)
                {
                    var existCar = db.Cars.FirstOrDefault(x => x.PlateNumberForward == car.PlateNumberForward);
                    if(existCar==null)
                    {
                        waitingListEntity.Entity.Cars.Add(car);
                    }
                    else
                    {
                        var carEntity = db.Cars.Add(car);
                        waitingListEntity.Entity.Cars.Add(carEntity.Entity);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
