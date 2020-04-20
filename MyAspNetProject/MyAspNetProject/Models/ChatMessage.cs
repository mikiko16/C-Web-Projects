using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class ChatMessage
    {
        public ChatMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string SenderName { get; set; }

        public string Text { get; set; }

        public DateTimeOffset SendAt { get; set; }
    }
}
