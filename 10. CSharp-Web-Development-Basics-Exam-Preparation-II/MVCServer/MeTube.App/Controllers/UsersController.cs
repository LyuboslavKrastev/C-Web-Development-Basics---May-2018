using MeTube.App.Attributes;
using MeTube.App.Models.BindingModels;
using MeTube.App.Models.ViewModels;
using MeTube.Models;
using SimpleMvc.Common;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeTube.App.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
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
                PasswordHash = passwordHash
            };

            using (this.Context)
            {
                if (this.Context.Users.Any(u => u.Username == user.Username))
                {
                    this.Model.Data["error"] = string.Format(ErrorBox, "Username already taken");
                    return this.View();
                }
                if (this.Context.Users.Any(u => u.Email == user.Email))
                {
                    this.Model.Data["error"] = string.Format(ErrorBox, "Email already taken");
                    return this.View();
                }
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

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
                    .FirstOrDefault(u => u.Username == model.Username);
            }

            if (user == null)
            {
                this.Model.Data["error"] = string.Format(ErrorBox, "Invalid username or password.");
                return this.View();
            }

            var passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            if (user.PasswordHash != passwordHash)
            {
                this.Model.Data["error"] = string.Format(ErrorBox, "Invalid username or password.");
                return this.View();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (this.User.IsAuthenticated)
            {
                this.SignOut();
            }

            return this.RedirectToHome();
        }

        [HttpGet]
        [AuthotirzeLogin]
        public IActionResult Profile()
        {
            IEnumerable<TubeProfileViewModel> tubes;

            using (this.Context)
            {
                tubes = this.Context.Tubes
                  .Where(t => t.UploaderId == this.DbUser.Id)
                  .Select(TubeProfileViewModel.FromTube)
                  .ToList();
            }

            var tubesResult = new StringBuilder();

            this.Model.Data["username"] = this.DbUser.Username;
            this.Model.Data["email"] = this.DbUser.Email;

            foreach (var tube in tubes)
            {
                tubesResult.AppendLine(
                    $@"<tr>
                        <td>{tube.Id}</td>
                        <td>{tube.Title}</td>
                        <td>{tube.Author}</td>
                        <td><a href=""/tubes/details?id={tube.Id}"">Details</a></td>                   
                     </tr>");
            }

            this.Model.Data["tubes"] = tubesResult.ToString();

            return this.View();
        }
    }
}
