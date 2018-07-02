using System.ComponentModel.DataAnnotations;


namespace ChushkaApp.App.Models.BindingModels
{
    public class ProductCreatingBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
