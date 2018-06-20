namespace SimpleMvc.App.Controllers
{
    using Framework.Controllers;
    using Framework.Attributes.Methods;
    using Framework.Contracts;
    using BindingModels;
    using SimpleMvc.Domain;
    using SimpleMvc.Data;
    using ViewModels;
    using Framework.Contracts.Generic;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var db = new NotesDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            List<User> users;

            using (var db = new NotesDbContext())
            {
                users = db.Users.ToList();
            }

            AllUsernamesViewModel viewModel = new AllUsernamesViewModel();

            foreach (var user in users)
            {
                viewModel.Usernames.Add(new UsersProfileViewModel
                {
                    UserId = user.Id,
                    Username = user.Username
                });
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UsersProfileViewModel> Profile(int id)
        {
            using (var db = new NotesDbContext())
            {

                UsersProfileViewModel viewModel = db.Users
                    .Where(u => u.Id == id)
                    .Select(u => new UsersProfileViewModel
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Notes = u.Notes
                    .Select(
                        n => new NoteViewModel()
                        {
                            Title = n.Title,
                            Content = n.Content
                        })
                }).FirstOrDefault();

                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UsersProfileViewModel> Profile(AddNoteBindingModel model)
        {

            using (var db = new NotesDbContext())
            {
                var user = db.Users.Find(model.UserId);

                var note = new Note
                {
                    Title = model.Title,
                    Content = model.Content
                };
                user.Notes.Add(note);
                db.SaveChanges();
            }

            return Profile(model.UserId);
        }

    }
}

