using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public interface INotificationModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
