namespace NotesApp.Controllers
{
    using Models.BindingModels;
    using NotesApp.Models;
    using NotesApp.Models.ViewModels;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisteringUserBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.Model.Data["error"] = ParameterValidator.ModelErrors.Values.Select(e => e.First()).First();
                return this.View();
            }

            var passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            var user = new User()
            {
                Username = model.Username,
                PasswordHash = passwordHash
            };

            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }

            this.SignIn(user.Username, user.Id);
            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult All()
        {
            List<UserViewModel> users = this.Context
             .Users
             .Select(user => new UserViewModel
             {
                 UserId = user.Id,
                 Username = user.Username,
                 Notes = user.Notes.Select(n => new NoteViewModel
                 {
                     Id = n.Id,
                     Title = n.Title,
                     Content = n.Content
                 })
             })
             .ToList();

            var result = new StringBuilder();

            foreach (var user in users)
            {
                result
                    .Append($@"
                <div class=""card bg-light mb-3"" style=""max-width: 50%;"">
                 <div class=""card-body"">
               <h5 class=""card-title"">{user.Username}</h5>
                 </div>
                </div>")
                    .AppendLine();
            }

            this.Model.Data["users"] = result.ToString();

            return this.View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model.Data["error"] = string.Empty;
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
                this.Model.Data["error"] = "Invalid username or password.";
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
                this.Model.Data["error"] = "Invalid username or password.";
                return this.View();
            }

            var passwordHash = PasswordUtilities.GetPasswordHash(model.Password);

            if (user.PasswordHash != passwordHash)
            {
                this.Model.Data["error"] = "Invalid username or password.";
                return this.View();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult AddNote()
        {
            return this.View();
        }

        [HttpPost]
        [PreAuthorize]
        public IActionResult AddNote(AddNoteBindingModel model)
        {

            if (!this.IsValidModel(model))
            {
                this.Model.Data["error"] = ParameterValidator.ModelErrors.Values.Select(e => e.First()).First();
                return this.View();
            }

            Note note = new Note()
            {
                Title = model.Title,
                AuthorId = this.DbUser.Id,
                Content = model.Content
            };

            this.Context.Notes.Add(note);
            this.Context.SaveChanges();
            return this.Profile();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Profile()
        {
            this.Model.Data["username"] = this.DbUser.Username;

            List<NoteViewModel> notes = this.Context
                    .Notes
                    .Where(u => u.AuthorId == this.DbUser.Id)
                    .Select(note => new NoteViewModel
                    {
                        Id = note.Id,
                        Title = note.Title,
                        Content = note.Content
                    })
                    .ToList();

            var result = new StringBuilder();

            foreach (var note in notes)
            {
                result
                    .Append($@"
                <div class=""card bg-light mb-3"" style=""max-width: 50%;"">
                 <div class=""card-body"">
               <h5 class=""card-title"">{note.Title}</h5>
                <p class=""card-text"">{note.Content}</p>
                 </div>
                </div>")
                    .AppendLine();
            }

            this.Model.Data["notes"] = result.ToString();

            return this.View();
        }
    }
}

