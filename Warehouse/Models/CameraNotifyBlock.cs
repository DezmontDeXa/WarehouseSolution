using System.Text;

namespace Warehouse.Models
{
    public class CameraNotifyBlock
    {
        public IReadOnlyDictionary<string, string> Headers { get; }

        public string Content { get; }

        public byte[] ContentBytes { get; }

        public string ContentType { get; }

        public CameraNotifyBlock(IReadOnlyDictionary<string, string> headers, byte[] contentBytes)
        {
            this.Headers = headers;
            this.ContentBytes = contentBytes;
            this.Content = Encoding.UTF8.GetString(contentBytes);
            this.ContentType = headers["Content-Type"];
        }

        public CameraNotifyBlock(IReadOnlyDictionary<string, string> headers, string content)
        {
            this.Headers = headers;
            this.ContentBytes = Encoding.UTF8.GetBytes(content);
            this.Content = content;
            this.ContentType = headers["Content-Type"];
        }

        public override string ToString() => "ContentType: " + this.ContentType + "\r\nContent: " + this.Content + "\r\n";
    }
}
