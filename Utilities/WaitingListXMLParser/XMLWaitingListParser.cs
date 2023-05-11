using SharedLibrary.DataBaseModels;
using System.Xml;

namespace WaitingListXMLParser
{
    public class XMLWaitingListParser : IWaitingListParser
    {
        public WaitingList Parse(string xmlContent)
        {
            var result = new WaitingList();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent);
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

                car.PlateNumberForward = node.Attributes["НомерТС"].Value;
                car.PlateNumberBackward = node.Attributes["Прицеп"].Value;
                car.Driver = node.Attributes["Водитель"].Value;

                result.Cars.Add(car);
            }

            return result;
        }

        public WaitingList Parse(Stream xmlFile)
        {
            using (var reader = new StreamReader(xmlFile))
            {
                return Parse(reader.ReadToEnd());
            }
        }
    }
}