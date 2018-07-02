using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
