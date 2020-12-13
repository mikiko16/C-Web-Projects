using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyAspNetProject.Helpers;
using MyAspNetProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace MyAspNetProject.Services
{
    public static class NotificationService
    {
        public static void SendNotification(INotificationModel message, VapidDetails vapidDetails)
        {
            var client = new WebPushClient();
            var serializedMessage = JsonConvert.SerializeObject(message);

            foreach (var pushSubscription in Subscription.Subscriptions)
            {
                client.SendNotification(pushSubscription, serializedMessage, vapidDetails);
            }
        }
    }
}
