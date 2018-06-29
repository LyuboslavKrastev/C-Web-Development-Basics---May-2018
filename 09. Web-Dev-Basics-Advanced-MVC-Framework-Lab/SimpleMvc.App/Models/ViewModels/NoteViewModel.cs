namespace NotesApp.Models.ViewModels
{
    using System;

    public class NoteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id} Title: {this.Title} {Environment.NewLine} {this.Content}";
        }
    }
}
