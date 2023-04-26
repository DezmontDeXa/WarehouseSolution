using SharedLibrary.DataBaseModels;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;

namespace Warehouse.Services.CameraListenerService
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
                    if (e == null) continue;
                    OnNotification?.Invoke(this, e);

                }
            }
        }

        private CameraNotifyBlock ReadBlock(StreamReader reader)
        {
            try
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
            catch (XmlException e)
            {
                return null;
            }
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
}