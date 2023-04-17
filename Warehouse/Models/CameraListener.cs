using System.Net.Http.Headers;
using System.Text;
using Warehouse.DBModels;

namespace Warehouse.Models
{
    public class CameraListener : IDisposable
    {
        public Camera Camera { get; }

        public event EventHandler<CameraNotifyBlock>? OnNotification;
        public event EventHandler<Exception>? OnError;

        private readonly HttpClient _http;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cts;

        public CameraListener(Camera camera)
        {
            Camera = camera;
            _http = new HttpClient();
            _uri = new Uri($"{(camera.UseSsl ? "https":"http")}://{camera.Ip}/{camera.Endpoint}");
            if(!string.IsNullOrEmpty(camera.Login) || !string.IsNullOrEmpty(camera.Password))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", 
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(camera.Login + ":" + camera.Password)));
            }

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
                catch(Exception ex)
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
            Dictionary<string, string> headers = this.ReadHeaders(reader);
            int result;
            if (!int.TryParse(headers["Content-Length"], out result))
                throw new Exception("Пустой блок даннных.");
            char[] chArray = new char[result];
            reader.ReadBlock(chArray, 0, result);
            string content = string.Join<char>("", (IEnumerable<char>)chArray);
            return new CameraNotifyBlock((IReadOnlyDictionary<string, string>)headers, content);
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

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}
