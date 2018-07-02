using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.App.Models.BindingModels
{
    public class UserRegisteringBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
