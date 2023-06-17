using System.Text;
using System.Xml;
using Warehouse.Interfaces.CamerasListener;

namespace Warehouse.CameraListeners
{
    public class CameraNotifyBlock : ICameraNotifyBlock
    {
        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        public string? Content { get; private set; }

        public byte[] ContentBytes { get; private set; }

        public string ContentType { get; private set; }

        public int ContentLength { get; private set; }

        public XmlDocument? XmlDocument { get; private set; }

        public XmlElement? XmlDocumentRoot { get; private set; }

        public string? EventType { get; private set; }

        public CameraNotifyBlock(BinaryReader reader, string contentType)
        {
            ContentType = contentType.Split(":;".ToCharArray())[1].Trim();
            ContentLength = int.Parse(reader.ReadLine()?.Split(":")[1].Trim() ?? "0");
            Headers = ReadHeaders();

            ContentBytes = reader.ReadBytes(ContentLength);

            switch (ContentType)
            {
                case "application/xml":
                    Content = Encoding.UTF8.GetString(ContentBytes).Trim();
                    CreateXMLDocument();
                    return;
                case "text/xml":
                    Content = Encoding.UTF8.GetString(ContentBytes).Trim();
                    CreateXMLDocument();
                    return;
                case "image/jpeg":
                    ContentBytes = ContentBytes.Skip(2).ToArray();
                    return;
                case "application/json":
                    return;
            }

            return;

        }

        private Dictionary<string, string> ReadHeaders()
        {
            return new Dictionary<string, string>()
            {
                { "Content-Type", ContentType },
                { "Content-Length", ContentLength.ToString() }
            };
        }

        private void CreateXMLDocument()
        {
            if (Content is null) throw new ArgumentNullException(nameof(Content));

            XmlDocument = new XmlDocument();
            XmlDocument.LoadXml(Content);
            XmlDocumentRoot = XmlDocument.DocumentElement;
            EventType = XmlDocumentRoot?["eventType"]?.InnerText ?? "Null event type";
        }

        public override string ToString() => "ContentType: " + ContentType + "\r\nContent: " + Content + "\r\n";
    }
}