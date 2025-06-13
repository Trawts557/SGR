using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Restaurants_and_Products
{
    public class Restaurant
    {
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Specialty { get; set; }
        public int IdOwner { get; set; }

    }
}
