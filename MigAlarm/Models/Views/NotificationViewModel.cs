using System.Collections.Generic;

namespace MigAlarm.Models.Views
{
    public class NotificationViewModel
    {
        public IEnumerable<Notification> Notifications { get; set; }
        public Notification Notification { get; set; }
    }
}