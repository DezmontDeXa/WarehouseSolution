using System.Net.Http.Headers;
using System.Text;

namespace CameraListenerService
{
    public class CameraListener : IDisposable
    {
        public event EventHandler<CameraNotifyBlock>? OnNotification;
        public event EventHandler<Exception>? OnError;

        private readonly HttpClient _http;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cts;

        public CameraListener(Uri uri)
        {
            _http = new HttpClient();
            _cts = new CancellationTokenSource();
            _uri = uri;

            if (!string.IsNullOrEmpty(_uri.UserInfo))
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(_uri.UserInfo)));

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
                    Task.Delay(1000).Wait();
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
            Dictionary<string, string> headers = ReadHeaders(reader);
            if (headers["Content-Type"] != "application/xml" && headers["Content-Type"] != "text/xml")
                return null;
            int result;
            if (!int.TryParse(headers["Content-Length"], out result))
                throw new Exception("Пустой блок даннных.");
            var chArray = new char[result];

            reader.ReadBlock(chArray, 0, result);
            var content = string.Join("", chArray).TrimStart();

            try
            {
                return new CameraNotifyBlock(headers, content);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
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