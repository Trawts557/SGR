using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Review__Notifications_and_Analytics
{
    public class Review
    {
        public int IdReview { get; set ; }
        public int IdCustomer { get; set ; }
        public int IdRestaurant { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Timestamp { get; set; }
}
}
