﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Helpers;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebPush;

namespace push_notification_angular_dotnet_core.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {

        [HttpPost("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe([FromBody] PushSubscription sub)
        {
            Subscription.Subscriptions.Add(sub);
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Unsubscribe([FromBody] PushSubscription sub)
        {
            var item = Subscription.Subscriptions.FirstOrDefault(s => s.Endpoint == sub.Endpoint);
            if (item != null)
            {
                Subscription.Subscriptions.Remove(item);
            }
        }

        [HttpPost("broadcast")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Broadcast([FromBody] NotificationModel message, [FromServices] VapidDetails vapidDetails)
        {
            NotificationService.SendNotification(message, vapidDetails);
        }
    }
}