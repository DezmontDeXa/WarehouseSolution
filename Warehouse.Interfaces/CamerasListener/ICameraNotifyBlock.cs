using System.Xml;

namespace Warehouse.Interfaces.CamerasListener
{
    public interface ICameraNotifyBlock
    {
        string Content { get; }
        byte[] ContentBytes { get; }
        int ContentLength { get; }
        string ContentType { get; }
        string? EventType { get; }
        IReadOnlyDictionary<string, string> Headers { get; }
        XmlDocument XmlDocument { get; }
        XmlElement XmlDocumentRoot { get; }

        string ToString();
    }
}