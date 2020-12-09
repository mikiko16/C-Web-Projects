using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace MyAspNetProject.Helpers
{
    public static class Subscription
    {
        public static List<PushSubscription> Subscriptions { get; set; } = new List<PushSubscription>();
    }
}
