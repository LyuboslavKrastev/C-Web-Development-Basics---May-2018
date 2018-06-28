using KittenApp.App.Models.BindingModels;
using KittenApp.Models;
using SimpleMvc.Common;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;
using System.Linq;

namespace KittenApp.App.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            this.Model.Data["errors"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisteringModel model)
        {
            if (!IsValidModel(model))
            {
                string errorMessage = $"{this.ParameterValidator.ModelErrors.Values.Select(v => v.First()).First()}";
                this.Model.Data["errors"] = string.Format(ErrorBox, errorMessage);
                return this.View();
            }

            if (this.Context.Users.Any(u => u.Username == model.Username))
            {
                this.Model.Data["errors"] = string.Format(ErrorBox, "Username already taken!");
                return this.View();
            }


            if (this.Context.Users.Any(u => u.Email == model.Email))
            {
                this.Model.Data["errors"] = string.Format(ErrorBox, "Email already taken!");
                return this.View();
            }

            var user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = PasswordUtilities.GetPasswordHash(model.Password)
            };

            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
                this.SignIn(user.Username, user.Id);
            }

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model.Data["errors"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoggingInModel model)
        {
            var user = this.Context.Users
                .FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                this.Model.Data["errors"] = string.Format(ErrorBox, "Invalid username or password");
                return this.View();
            }

            string passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            if (user.PasswordHash != passwordHash)
            {
                this.Model.Data["error"] = "Invalid username or password";
                return this.View();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
        }
    }
}
