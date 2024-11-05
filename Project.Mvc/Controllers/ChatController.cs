using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Business.Services;

namespace Project.Mvc.Controllers
{
    public class ChatController : Controller
    {
        private readonly MessageQueueService _messageQueueService;
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(MessageQueueService messageQueueService, IHubContext<ChatHub> chatHub)
        {
            _messageQueueService = messageQueueService;
            _chatHub = chatHub;
            _messageQueueService.StartReceivingMessages(ReceiveMessage);
        }

        private async void ReceiveMessage(string message)
        {
            var parts = message.Split('|');
            var username = parts[0];
            var text = parts[1];
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Received message has an empty username!");
            }

            await _chatHub.Clients.All.SendAsync("ReceiveMessage", username, text);
        }

        public IActionResult SendMessage(string username, string message)
        {
            _messageQueueService.SendMessage($"{username}|{message}");
            return Ok();
        }
    }
}
