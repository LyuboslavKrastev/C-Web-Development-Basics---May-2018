namespace CustomWebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using Services.Contracts;
    using ViewModels.Account;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                bool userAlreadyExists = db.Users.Any(u => u.Username == username);

                if (userAlreadyExists)
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                bool userExists = db.Users.Any(u => u.Username == username && u.Password == password);
                return userExists;
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                return new ProfileViewModel
                {
                    Username = user.Username,
                    RegistrationDate = user.RegistrationDate,
                    TotalOrders = user.Orders.Count()
                };
            }
        }

        public int? GetUserId(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var id = db.Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null;
            }
        }
    }
}
