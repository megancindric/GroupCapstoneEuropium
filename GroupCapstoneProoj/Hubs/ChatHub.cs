//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNetCore.SignalR;
//using Owin;
//using System.Threading.Tasks;

//namespace SignalRChat.Hubs
//{
//    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
//    {
//        public async Task SendMessage(string user, string message)
//        {
//            await Clients.All.SendAsync("ReceiveMessage", user, message);
//        }

//        public void Send(string userId, string message)
//        {
//            Clients.User(userId).SendAsync(message);
//        }
//    }
//}