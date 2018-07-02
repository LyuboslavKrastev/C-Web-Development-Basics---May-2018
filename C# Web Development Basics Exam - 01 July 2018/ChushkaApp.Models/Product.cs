using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType Type { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
