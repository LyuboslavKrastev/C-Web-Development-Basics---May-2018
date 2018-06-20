namespace SimpleMvc.App.ViewModels
{
    using System.Collections.Generic;

    public class UsersProfileViewModel
    {
        public string Username { get; set; }

        public int UserId { get; set; }

        public IEnumerable<NoteViewModel> Notes { get; set; }

        public override string ToString()
        {
            return this.Username;
        }
    }
}
