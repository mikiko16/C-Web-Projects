using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class NotificationRegisterModel : INotificationModel
    {
        public NotificationRegisterModel(){
            this.Title = "New User Registered";
            this.Message = "Please confirm my request !!!";
            this.Url = "Without URL";
        }
        public string Title { get; set; }

        public string Message { get; set; }

        //[JsonProperty]
        public string Url { get; set; }
    }
}
