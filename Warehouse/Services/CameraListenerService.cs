using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class CameraListenerService : IDisposable
    {
        public Camera Camera { get; }

        public event EventHandler<CameraNotifyBlock>? OnNotification;
        public event EventHandler<Exception>? OnError;

        private readonly HttpClient _http;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cts;

        public CameraListenerService(Camera camera)
        {
            Camera = camera;
            _http = new HttpClient();
            var uriString = $"{camera.Ip}/{camera.Endpoint}";

            if (!string.IsNullOrEmpty(camera.Login) || !string.IsNullOrEmpty(camera.Password))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(camera.Login + ":" + camera.Password)));

                uriString = $"{camera.Login}:{camera.Password}@{uriString}";
            }

            uriString = $"{(camera.UseSsl ? "https" : "http")}://{uriString}";

            _uri = new Uri(uriString);
            _cts = new CancellationTokenSource();

            Task.Run(Working);
        }

        private void Working()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    using (var stream = _http.GetStreamAsync(_uri).Result)
                    using (var reader = new StreamReader(stream))
                        Listening(reader);
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, ex);
                    continue;
                }
            }
        }

        private void Listening(StreamReader reader)
        {
            while (!_cts.IsCancellationRequested)
            {
                if (reader.ReadLine() == "--boundary")
                {
                    CameraNotifyBlock e = ReadBlock(reader);
                    OnNotification?.Invoke(this, e);
                }
            }
        }

        private CameraNotifyBlock ReadBlock(StreamReader reader)
        {
            Dictionary<string, string> headers = ReadHeaders(reader);
            int result;
            if (!int.TryParse(headers["Content-Length"], out result))
                throw new Exception("Пустой блок даннных.");
            var chArray = new char[result];
            reader.ReadBlock(chArray, 0, result);
            var content = string.Join("", chArray);
            return new CameraNotifyBlock(headers, content);
        }

        private Dictionary<string, string> ReadHeaders(StreamReader reader)
        {
            var dictionary = new Dictionary<string, string>();
            var line = "";
            while (!line.StartsWith("Content-Length"))
            {
                line = reader.ReadLine();
                var strArray = line?.Split(":;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (strArray != null && strArray.Length >= 2)
                    dictionary.Add(strArray[0].Trim(), strArray[1].Trim());
            }
            return dictionary;
        }

#if DEBUG

        public void SendTestData()
        {
            OnNotification?.Invoke(this,
                new CameraNotifyBlock(
                    new Dictionary<string, string>()
                    {
                        { "Content-Type", "TestContent" }
                    },
                    File.ReadAllText("TestData/TestCameraNotifyBlock.xml")));
        }
#endif

        public void Dispose()
        {
            _cts.Cancel();
        }

    }

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
