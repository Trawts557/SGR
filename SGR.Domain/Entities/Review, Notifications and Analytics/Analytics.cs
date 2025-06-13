using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Review__Notifications_and_Analytics
{
    public class Analytics
    {
        public int IdAnalytics { get; set ; }
        public int IdRestaurant { get; set ; }
        public DateTime TimeStamp { get; set; }
        public double TotalSales { get; set; }
        public int TotalReservations { get; set; }
        
}
}
