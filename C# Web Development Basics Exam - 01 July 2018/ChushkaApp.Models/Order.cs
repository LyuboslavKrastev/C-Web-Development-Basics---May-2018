using System;
using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; } //GUID

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int ClientId { get; set; }
        public User Client { get; set; }

        [Required]
        public DateTime OrderedOn { get; set; }
    }
}
