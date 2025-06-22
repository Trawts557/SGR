using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Review__Notifications_and_Analytics
{
    public class Notification
    {
        public int IdNotification{ get; set ; }
        public int IdUser{ get; set ; }
        public string? Message { get; set; }
        public bool ReadFlag { get; set; }
        public DateTime Timestamp { get; set; }
}
}
