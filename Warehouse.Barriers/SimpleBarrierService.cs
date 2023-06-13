using NLog;
using System.Net.Http.Headers;
using System.Text;
using Warehouse.Interfaces.Barriers;

namespace Warehouse.Barriers
{
    public partial class SimpleBarrierService : IBarriersService, IDisposable
    {
        private HttpClient _http;
        private readonly ILogger _logger;

        public SimpleBarrierService(ILogger logger)
        {
            _logger = logger;
            _http = new HttpClient();
        }

        public void Open(IBarrierInfo barrier)
        {
            Switch(barrier, BarrierCommand.Open);
            Switch(barrier, BarrierCommand.Close);
        }

        private void Switch(IBarrierInfo barrier, BarrierCommand command)
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

            }
            catch (Exception ex)
            {
                _logger.Error($"Error on switch barrier. Barrier: {barrier.Name}, Command {command}, Ex: {ex}");
                return;
            }
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
