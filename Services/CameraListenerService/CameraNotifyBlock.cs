using System.Text;
using System.Xml;

namespace CameraListenerService
{

    public class CameraNotifyBlock
    {
        public IReadOnlyDictionary<string, string> Headers { get; }

        public string Content { get; }

        public byte[] ContentBytes { get; }

        public string ContentType { get; }

        public XmlDocument XmlDocument { get; }

        public XmlElement XmlDocumentRoot { get; }

        public string? EventType { get; }

        public CameraNotifyBlock(IReadOnlyDictionary<string, string> headers, string content)
        {
            Headers = headers;
            ContentBytes = Encoding.UTF8.GetBytes(content);
            Content = content;
            ContentType = headers["Content-Type"];

            XmlDocument = new XmlDocument();
            XmlDocument.LoadXml(Content);
            XmlDocumentRoot = XmlDocument.DocumentElement;

            //var nsmgr = new XmlNamespaceManager(XmlDocument.NameTable);
            //nsmgr.AddNamespace("hik", "http://www.hikvision.com/ver20/XMLSchema");
            //XmlDocument.Prefix = "hik";

            EventType = XmlDocumentRoot["eventType"]?.InnerText;
        }

        public override string ToString() => "ContentType: " + ContentType + "\r\nContent: " + Content + "\r\n";
    }
}