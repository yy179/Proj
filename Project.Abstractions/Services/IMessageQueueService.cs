using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IMessageQueueService
    {
        void SendMessage(string message);
        void StartReceivingMessages(Action<string> onMessageReceived);
        void Dispose();
    }
}
