using AdminClient.Services.Interfaces;
using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace AdminClient.Services
{
    public class WaitingListImporterService : IWaitingListImporterService
    {
        public void ImportList(AccessGrantType accessGrantType, string xmlFileContent)
        {
            xmlFileContent = FixContent(xmlFileContent);
            var waitingList = ParseFile(xmlFileContent);
            waitingList.AccessGrantType = accessGrantType;
            waitingList.Name = $"{accessGrantType}({waitingList.Number})";
            ImportToDataBase(waitingList);
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

                car.PlateNumberForward = node.Attributes["НомерТС"].Value;
                car.PlateNumberBackward = node.Attributes["Прицеп"].Value;
                car.Driver = node.Attributes["Водитель"].Value;
                car.CarStateId = 0;
                result.Cars.Add(car);
            }

            return result;
        }

        private void ImportToDataBase(WaitingList waitingList)
        {
            using (var db = new WarehouseContext())
            {
                db.WaitingLists.Add(waitingList);
                db.SaveChanges();
            }
        }
    }
}
