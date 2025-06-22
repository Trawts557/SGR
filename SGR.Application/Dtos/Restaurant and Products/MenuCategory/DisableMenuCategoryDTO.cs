using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant
{
    public record DisableMenuCategoryDTO
    {
        public string? DeletedBy { get; set; } 
        public int IdMenuCategory { get; set; }


    }
}
