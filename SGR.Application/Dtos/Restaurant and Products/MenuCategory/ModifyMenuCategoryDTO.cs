using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant
{
    public record ModifyMenuCategoryDTO
    {
        public int IdMenuCategory { get; set; }
        public string? Name { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Description { get; set; }
    }
}
