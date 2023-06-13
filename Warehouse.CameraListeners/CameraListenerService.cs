using System.Net.Http.Headers;
using System.Text;
using Warehouse.Interfaces.CamerasListener;

namespace Warehouse.CameraListeners
{
    public class CameraListener : IDisposable, ICameraListener
    {
        public event EventHandler<ICameraNotifyBlock>? OnNotification;
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
                    using (var reader = new BinaryReader(stream))
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

        private void Listening(BinaryReader reader)
        {
            while (!_cts.IsCancellationRequested)
            {
                StringBuilder contentBuilder = new StringBuilder();
                var line = reader.ReadLine();
                if (line.StartsWith("Content-Type:"))
                {
                    var e = new CameraNotifyBlock(reader, line);
                    if (e == null) continue;
                    OnNotification?.Invoke(this, e);
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}