using System.ComponentModel.DataAnnotations;

namespace KittenApp.App.Models.BindingModels
{
    public class UserLoggingInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
