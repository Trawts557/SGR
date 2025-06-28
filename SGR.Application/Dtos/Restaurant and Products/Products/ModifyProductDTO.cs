using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant_and_Products.Products
{
    public record ModifyProductDTO
    {
        public int IdProduct { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
