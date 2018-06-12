namespace CustomWebServer.GameStoreApplication.Data.Models
{
    using Common.ValidationConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {       
        public int Id { get; set; }

        [MinLength(AccountConstants.FullNameMinLength), MaxLength(AccountConstants.FullNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(AccountConstants.EmailMinLength), MaxLength(AccountConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(AccountConstants.PasswordMinLength), MaxLength(AccountConstants.PasswordMaxLength)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
