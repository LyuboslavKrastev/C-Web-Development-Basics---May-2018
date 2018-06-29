using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.BindingModels
{
    public class AddNoteBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }
    }
}
