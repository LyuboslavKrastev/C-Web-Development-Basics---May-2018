namespace NotesApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }
    }
}
