namespace NotesApp.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoggingInBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
