using Warehouse.LogsReader.Services.Interfaces;

namespace Warehouse.LogsReader.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
