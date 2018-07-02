using ChushkaApp.App.Models.BindingModels;
using ChushkaApp.Models;
using Microsoft.EntityFrameworkCore;
using SoftUni.WebServer.Common;
using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
using SoftUni.WebServer.Mvc.Attributes.Security;
using SoftUni.WebServer.Mvc.Interfaces;
using System.Linq;

namespace ChushkaApp.App.Controllers
{
    public class UsersController : BaseController
    {
        public const int UserId = 1;
        public const int AdminId = 2;

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoggingInBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.BuildErrorView();
                return this.View();
            }

            User user;
            using (this.Context)
            {
                user = this.Context.Users
                    .Include(u => u.Role)
                    .Where(u => u.Username == model.Username)
                    .FirstOrDefault();
            }

            if (user == null)
            {
                this.ViewData.Data["error"] = "Invalid username or password.";
                return this.View();
            }

            var passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            if (user.PasswordHash != passwordHash)
            {
                this.ViewData.Data["error"] = "Invalid username or password.";
                return this.View();
            }

            this.SignIn(user.Username, user.Id, new[] { user.Role.Name.ToString() });

            return this.RedirectToHome();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Register()
        {
            this.ViewData["error"] = string.Empty;

            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }


        [HttpPost]
        public IActionResult Register(UserRegisteringBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                this.RedirectToHome();
            }
            if (!this.IsValidModel(model))
            {
                this.BuildErrorView();
                return this.View();
            }

            string passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            var user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PasswordHash = passwordHash
            };

            if (!this.Context.Users.Any())
            {
                user.RoleId = AdminId;
            }
            else
            {
                user.RoleId = UserId;
            }

            string userRole = string.Empty;
            using (this.Context)
            {
                if (this.Context.Users.Any(u => u.Username == user.Username))
                {
                    this.ViewData["error"] = string.Format("Username already taken");
                    return this.View();
                }
                if (this.Context.Users.Any(u => u.Email == user.Email))
                {
                    this.ViewData["error"] = string.Format("Email already taken");
                    return this.View();
                }
                this.Context.Users.Add(user);
                this.Context.SaveChanges();

                userRole = this.Context.Roles.FirstOrDefault(r => r.Id == user.RoleId).ToString();
            }

            this.SignIn(user.Username, user.Id, new[] { userRole });

            return this.RedirectToHome();
        }
    }
}
