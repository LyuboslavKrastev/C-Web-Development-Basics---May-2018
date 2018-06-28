using KittenApp.Data;
using KittenApp.Models;
using SimpleMvc.Framework.Controllers;
using SimpleMvc.Framework.Interfaces;
using System.Linq;

namespace KittenApp.App.Controllers
{
    public class BaseController : Controller
    {
        protected const string ErrorBox = @"<div class=""alert alert-danger"">
                 <strong>{0}</strong>
                    </div>";

        private const string AuthenticatedMenu =

             @"<li class=""nav-item active col-md-3"">
                        <a class=""nav-link"" href=""/"">Home</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link"" href=""/kittens/all"">All Kittens</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link"" href=""/kittens/add"">Add Kitten</a>
                    </li>
                      <li class=""nav-item active col-md-3"">
                        <a class=""nav-link"" href=""/users/logout"">Logout</a>
                    </li>";

        private const string UnauthenticatedMenu =
                 @"<li class=""nav-item active col-md-4"">
                        <a class=""nav-link"" href=""/"">Home</a>
                    </li>
                    <li class=""nav-item active col-md-4"">
                        <a class=""nav-link"" href=""/users/login"">Login</a>
                    </li>
                    <li class=""nav-item active col-md-4"">
                        <a class=""nav-link"" href=""/users/register"">Register</a>
                    </li>";

        protected BaseController()
        {
            this.Context = new KittenAppContext();
            this.Model.Data["error"] = string.Empty;
        }

        protected KittenAppContext Context { get; private set; }

        protected User DbUser { get; private set; }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        protected virtual void BuildErrorView()
        {
            this.Model.Data["error"] = this.ParameterValidator.ModelErrors.Values.Select(v => v.First()).First().ToString();
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
