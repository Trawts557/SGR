using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Review__Notifications__Analytics.Review
{
    public record ModifyReviewDTO
    {
        public int IdReview { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
