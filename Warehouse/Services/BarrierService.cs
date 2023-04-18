using System.Net.Http.Headers;
using System.Text;
using Warehouse.DataBaseModels;

namespace Warehouse.Services
{
    public class BarrierService : IDisposable
    {
        public BarrierInfo Barrier { get; }

        private HttpClient _http;

        public BarrierService(BarrierInfo barrier)
        {
            Barrier = barrier;
            _http = new HttpClient(); 
            _http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(
                    "Basic", 
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(barrier.Login + ":" + barrier.Password)));

        }

        public void Switch(BarrierCommand command)
        {
            var cmd = command == BarrierCommand.Open ? "high" : "low";
            string xmlReq = $"<IOPortData version ='1.0' xmlns='http://www.hikvision.com/ver10/XMLSchema'><outputState>{cmd}</outputState></IOPortData>";            
            var request = new HttpRequestMessage(HttpMethod.Put, Barrier.Uri);
            request.Content = new StringContent(xmlReq, Encoding.UTF8, "application/xml");
            _http.SendAsync(request);
        }

        public void Dispose()
        {
            _http.Dispose();
        }

        public enum BarrierCommand
        {
            Open,
            Close
        }
    }
}
