using System.ComponentModel.DataAnnotations;

namespace ChushkaApp.App.Models.BindingModels
{
    public class UserLoggingInBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
