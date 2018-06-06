namespace CustomWebServer.ByTheCakeApplication.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ImageUrl { get; set; }

        public ICollection<OrderProduct> Orders { get; set; } = new List<OrderProduct>();
    }
}
