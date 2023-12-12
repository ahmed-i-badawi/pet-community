using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string fromUser, string message)
        {
            await Clients.All.SendAsync("ResieveMessage", fromUser, message);
        }
    }

}




//[Authorize]
//public class ChatHub : Hub
//{
//    public override Task OnConnectedAsync()
//    {
//        Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
//        return base.OnConnectedAsync();
//    }
//    public async Task SendMessage(string user, string message)
//    {
//        await Clients.All.SendAsync("ReceiveMessage", user, message);
//    }

//    public Task SendMessageToGroup(string sender, string receiver, string message)
//    {
//        return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
//    }
//}