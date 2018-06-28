using MeTube.Data;
using MeTube.Models;
using SimpleMvc.Framework.Controllers;
using SimpleMvc.Framework.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace MeTube.App.Controllers
{
    public class BaseController : Controller
    {
        protected const string ErrorBox = @"<div class=""alert alert-danger"">
                 <strong>{0}</strong>
                    </div>";

        private const string AuthenticatedMenu =
                  @"<li class=""nav-item active col-md-3"">
                      <a class=""nav-link h5"" href=""/"">Home</a>
                  </li>
                  <li class=""nav-item active col-md-3"">
                      <a class=""nav-link h5"" href=""/users/profile"">Profile</a>
                  </li>
                  <li class=""nav-item active col-md-3"">
                      <a class=""nav-link h5"" href=""/tubes/upload"">Upload</a>
                  </li>
                  <li class=""nav-item active col-md-3"">
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
            this.Context = new MeTubeAppcontext();
            this.Model.Data["error"] = string.Empty;
        }

        protected MeTubeAppcontext Context { get; private set; }

        protected User DbUser { get; private set; }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        protected virtual void BuildErrorView()
        {
            string error = this.ParameterValidator.ModelErrors.Values.Select(v => v.First()).First().ToString();
            this.Model.Data["error"] = string.Format(ErrorBox, error);

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
