using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeTube.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Tube> Tubes { get; set; }
    }
}
