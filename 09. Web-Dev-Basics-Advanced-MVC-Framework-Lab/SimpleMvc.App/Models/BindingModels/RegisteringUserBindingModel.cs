using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.BindingModels
{
    public class RegisteringUserBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
