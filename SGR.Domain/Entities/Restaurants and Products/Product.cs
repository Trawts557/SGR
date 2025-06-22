using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Restaurants_and_Products
{
    public class Product
    {
        public int IdProduct { get; set ; }
        public string? Name { get; set ; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public int IdRestaurant { get; set; }
        public int IdCategory { get; set; }

    }
}
