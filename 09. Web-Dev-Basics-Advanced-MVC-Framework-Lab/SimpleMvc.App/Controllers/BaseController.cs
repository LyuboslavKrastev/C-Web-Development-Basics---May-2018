namespace NotesApp.Controllers
{
    using NotesApp.Data;
    using NotesApp.Models;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public abstract class BaseController : Controller
    {
        private const string AuthenticatedMenu =
                 @"<li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/"">Home</a>
                  </li>
                  <li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/users/profile"">Profile</a>
                  </li>
                  <li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/users/all"">All Users</a>
                  </li>
                  <li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/notes/all"">All Notes</a>
                  </li>
                  <li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/users/addnote"">Add Note</a>
                  </li>
                  <li class=""nav-item active col-md-2"">
                      <a class=""nav-link h5"" href=""/users/logout"">Logout</a>
                  </li>";
        private const string UnauthenticatedMenu =
                 @"<li class=""nav-item active col-md-4"">
                      <a class=""nav-link h5"" href=""/"">Home</a>
                  </li>
                  <li class=""nav-item active col-md-4"">
                      <a class=""nav-link h5"" href=""/users/login"">Login</a>
                  </li>
                  <li class=""nav-item active col-md-4"">
                      <a class=""nav-link h5"" href=""/users/register"">Register</a>
                  </li>";

        protected BaseController()
        {
            this.Context = new NotesAppContext();
            this.Model.Data["error"] = string.Empty;
        }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        public NotesAppContext Context { get; set; }

        protected User DbUser { get; private set; }

        protected virtual IActionResult BuildErrorView()
        {
            this.Model.Data["error"] = "You have errors in your form";
            return this.View();
        }

        public override void OnAuthentication()
        {
            this.Model.Data["topMenu"] = this.User.IsAuthenticated ?
                 AuthenticatedMenu : UnauthenticatedMenu;

            if (this.User.IsAuthenticated)
            {

                this.DbUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);
            }

            base.OnAuthentication();
        }
    }
}
