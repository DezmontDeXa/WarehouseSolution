using NLog;
using System.Net.Http.Headers;
using System.Text;
using Warehouse.SharedLibrary;

namespace Warehouse.Services
{
    public class BarrierService : IDisposable
    {
        private HttpClient _http;
        private readonly ILogger _logger;

        public BarrierService(ILogger logger)
        {
            _logger = logger;
            _http = new HttpClient(); 
        }

        public void Switch(BarrierInfo barrier, BarrierCommand command)
        {
            try
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(barrier.Login + ":" + barrier.Password)));


                var cmd = command == BarrierCommand.Open ? "high" : "low";
                string xmlReq = $"<IOPortData version ='1.0' xmlns='http://www.hikvision.com/ver10/XMLSchema'><outputState>{cmd}</outputState></IOPortData>";
                var request = new HttpRequestMessage(HttpMethod.Put, barrier.Uri);
                request.Content = new StringContent(xmlReq, Encoding.UTF8, "application/xml");
                _http.Send(request);

            }catch(Exception ex)
            {
                _logger.Error($"Error on switch barrier. Barrier: {barrier.Name}, Command {command}, Ex: {ex}");
                return;
            }
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
