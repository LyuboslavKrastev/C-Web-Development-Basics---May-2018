namespace SimpleMvc.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username should be at least 3 symbols long")]
        [MaxLength(30, ErrorMessage = "Username should not be more than 30 symbols long")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Note> Notes { get; set; } = new HashSet<Note>();
    }
}
