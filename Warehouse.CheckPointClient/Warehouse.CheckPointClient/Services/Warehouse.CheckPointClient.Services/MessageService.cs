using Warehouse.CheckPointClient.Services.Interfaces;

namespace Warehouse.CheckPointClient.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
