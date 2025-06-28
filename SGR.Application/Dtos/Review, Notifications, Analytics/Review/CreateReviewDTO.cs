using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Review__Notifications__Analytics.Review
{
    public record CreateReviewDTO
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
