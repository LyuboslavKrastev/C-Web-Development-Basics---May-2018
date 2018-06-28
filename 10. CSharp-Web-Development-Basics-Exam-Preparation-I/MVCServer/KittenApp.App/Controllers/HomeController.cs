using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;

namespace KittenApp.App.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["message"] = $"Welcome, {this.User.Name}";
                this.Model.Data["loginMessage"] = string.Empty;
            }
            else
            {
                this.Model.Data["message"] = "Welcome to Fluffy Duffy Munchkin Cats";
                this.Model.Data["loginMessage"] = @"<a href=""/users/login"">Login</a> to trade or <a href=""/users/register"">Register</a> if you don't have an account.";
            }

            return this.View();
        }
    }
}
