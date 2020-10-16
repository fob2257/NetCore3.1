using NetCore3_1.Services.Interfaces;

namespace NetCore3_1.Services.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Message from MessageService";
        }
    }
}