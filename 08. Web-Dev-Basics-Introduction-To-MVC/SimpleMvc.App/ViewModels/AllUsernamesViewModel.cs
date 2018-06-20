namespace SimpleMvc.App.ViewModels
{
    using System.Collections.Generic;

    public class AllUsernamesViewModel
    {
        public AllUsernamesViewModel()
        {
            this.Usernames = new List<UsersProfileViewModel>();
        }

        public ICollection<UsersProfileViewModel> Usernames { get; set; }
    }
}
