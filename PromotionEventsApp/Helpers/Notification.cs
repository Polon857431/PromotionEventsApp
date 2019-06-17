using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.Helpers
{
    public enum NotificationType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }
    public class Notification
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
        public string Content { get; set; }
        public int DelayInMs { get; set; }

    }
}
