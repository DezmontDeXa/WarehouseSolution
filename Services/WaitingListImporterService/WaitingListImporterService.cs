using SharedLibrary.DataBaseModels;
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
            result.WaitingList.AccessGrantType = accessGrantType;
            result.WaitingList.Name = $"{accessGrantType}({result.WaitingList.Number})";
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

        private ParseResult ParseFile(string xmlFileContent)
        {
            var result = new WaitingList();
            var existCars = new List<Car>();

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

            using (var db = new WarehouseContext())
            {
                foreach (XmlNode node in xmlDocumentRoot.SelectNodes("//ТаблицаСписокТС"))
                {
                    var car = new Car();
                    car.PlateNumberForward = node.Attributes["НомерТС"].Value;
                    car.PlateNumberBackward = node.Attributes["Прицеп"].Value;
                    car.Driver = node.Attributes["Водитель"].Value;
                    car.CarStateId = 0;
                    car.TargetAreaId = _cameraToAreaId[result.Camera];

                    var carInDb = db.Cars.FirstOrDefault(x => x.PlateNumberForward == car.PlateNumberForward &&
                    x.Driver == car.Driver);
                    if (carInDb != null)
                        existCars.Add(carInDb);
                    else
                        result.Cars.Add(car);

                }
            }

            return new ParseResult() { WaitingList = result, ExistCars = existCars };
        }

        private struct ParseResult
        {
            public WaitingList WaitingList;
            public List<Car> ExistCars;
        }

        private void ImportToDataBase(ParseResult result)
        {
            using (var db = new WarehouseContext())
            {
                if (db.WaitingLists.Any(x => x.Number == result.WaitingList.Number))
                    return;

                foreach (var car in result.ExistCars)
                {
                    var carInDb = db.Cars.First(x => x.Id == car.Id);
                    result.WaitingList.Cars.Add(carInDb);
                }

                db.WaitingLists.Add(result.WaitingList);
                db.SaveChanges();
            }
        }
    }
}
