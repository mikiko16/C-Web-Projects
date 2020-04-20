using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyAspNetProject.Models;

namespace MyAspNetProject.Controllers
{
    [EnableCors("AllowAll")]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string text)
        {
            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                SendAt = DateTimeOffset.UtcNow,
            };
            // invoke this ReceiveMessage method in the client
            // Broadcast to all clients
            await Clients.All.SendAsync(
               "ReceiveMessage",
               message.SenderName,
               message.Text,
               message.SendAt
            );
        }
    }
}
