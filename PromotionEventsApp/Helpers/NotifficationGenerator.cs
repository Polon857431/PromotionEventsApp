using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.Helpers
{
    public class NotificationGenerator
    {
        public static Notification CreateNotification(string title, string content, string color, string icon)
        {
            return new Notification()
            {
                Title = title,
                Content = content,
                Color = color,
                Icon = icon
            };
        }
    }
}
