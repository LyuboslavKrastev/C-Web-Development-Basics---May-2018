using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
