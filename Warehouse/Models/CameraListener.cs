using System.Net.Http.Headers;
using System.Text;
using Warehouse.CameraRoles;
using Warehouse.DBModels;

namespace Warehouse.Models
{
    public class CameraListener
    {
        public string Url { get; }
        private HttpClient client;
        private StreamReader _reader;

        public event EventHandler<NotifyBlock> OnNotification;
        public event EventHandler<Exception> OnError;

        // Адаптировать AlertStreamListenenr 

        public CameraListener(Camera camera, CameraRoleBase cameraRole)
        {

        }

        public NotificationListener(string url)
        {
            Url = url;
            this.client = new HttpClient();
            Uri uri = new Uri(url);
            if (uri.UserInfo == null)
                return;
            string str1 = ((IEnumerable<string>)uri.UserInfo.Split(':')).First<string>();
            string str2 = uri.UserInfo.Replace(str1 + ":", "");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(str1 + ":" + str2)));
        }

        public async Task StartAsync()
        {
            using (var stream = await client.GetStreamAsync(Url))
            {
                _reader = new StreamReader(stream);
                await Task.Run(Listening);
            }
        }

        private void Listening()
        {
            while (_reader != null)
            {
                try
                {
                    if (_reader.ReadLine() == "--boundary")
                    {
                        NotifyBlock e = ReadBlock(_reader);
                        EventHandler<NotifyBlock> onNotification = OnNotification;
                        if (onNotification != null)
                            onNotification(this, e);
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, ex);
                    return;
                }
            }
        }

        public void Stop()
        {
            this._reader.Dispose();
            this._reader = (StreamReader)null;
        }

        private NotifyBlock ReadBlock(StreamReader reader)
        {
            Dictionary<string, string> headers = this.ReadHeaders(reader);
            int result;
            if (!int.TryParse(headers["Content-Length"], out result))
                throw new Exception("Пустой блок даннных.");
            char[] chArray = new char[result];
            reader.ReadBlock(chArray, 0, result);
            string content = string.Join<char>("", (IEnumerable<char>)chArray);
            return new NotifyBlock((IReadOnlyDictionary<string, string>)headers, content);
        }

        private Dictionary<string, string> ReadHeaders(StreamReader reader)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string str = "";
            while (!str.StartsWith("Content-Length"))
            {
                str = this._reader.ReadLine();
                string[] strArray = str.Split(":;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length >= 2)
                    dictionary.Add(strArray[0].Trim(), strArray[1].Trim());
            }
            return dictionary;
        }
    }
    public class NotifyBlock
    {
        public IReadOnlyDictionary<string, string> Headers { get; }

        public string Content { get; }

        public byte[] ContentBytes { get; }

        public string ContentType { get; }

        public NotifyBlock(IReadOnlyDictionary<string, string> headers, byte[] contentBytes)
        {
            this.Headers = headers;
            this.ContentBytes = contentBytes;
            this.Content = Encoding.UTF8.GetString(contentBytes);
            this.ContentType = headers["Content-Type"];
        }

        public NotifyBlock(IReadOnlyDictionary<string, string> headers, string content)
        {
            this.Headers = headers;
            this.ContentBytes = Encoding.UTF8.GetBytes(content);
            this.Content = content;
            this.ContentType = headers["Content-Type"];
        }

        public override string ToString() => "ContentType: " + this.ContentType + "\r\nContent: " + this.Content + "\r\n";
    }
}
