using AdminClient.Services.Interfaces;

namespace AdminClient.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
