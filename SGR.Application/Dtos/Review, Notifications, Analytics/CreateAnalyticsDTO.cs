using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Review__Notifications__Analytics
{
    public record CreateAnalyticsDTO
    {
        public int RestaurantId { get; set; }
        public string MetricName { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime RecordedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
