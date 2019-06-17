using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.Helpers
{
    public class NotificationGenerator
    {
        public static Notification CreateNotification(string title, string content, NotificationType type, string icon, int delayInMs)
        {
            return new Notification()
            {
                Title = title,
                Content = content,
                Type = type,
                Icon = icon,
                DelayInMs = delayInMs
            };
        }
    }
}
