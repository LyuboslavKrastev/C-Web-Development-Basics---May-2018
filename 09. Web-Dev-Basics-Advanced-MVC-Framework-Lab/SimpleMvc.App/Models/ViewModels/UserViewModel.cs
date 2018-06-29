namespace NotesApp.Models.ViewModels
{
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Username { get; set; }

        public int UserId { get; set; }

        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}
