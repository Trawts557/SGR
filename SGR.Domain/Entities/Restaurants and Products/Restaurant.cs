using System;

namespace SGR.Domain.Entities.Restaurants_and_Products
{
    public class Restaurant
    {
        public int IdRestaurant { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Specialty { get; set; }
        public int IdOwner { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
