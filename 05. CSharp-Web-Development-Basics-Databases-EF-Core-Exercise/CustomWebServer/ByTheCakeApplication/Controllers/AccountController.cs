namespace CustomWebServer.ByTheCakeApplication.Controllers
{
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using System;
    using ViewModels;
    using ViewModels.Account;

    public class AccountController : BaseController
    {
        private const int MinUsernameLength = 3;
        private const int MinPasswordLength = 3;

        private const string RegisterViewPath = @"account\register";
        private const string LoginViewPath = @"account\login";
        private const string InvalidCredentials = "Invalid username or password";

        private readonly IUserService users;

        public AccountController()
        {
            this.users = new UserService();
        }

        public IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(RegisterViewPath);
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            this.SetDefaultViewData();

            bool invalidRegistrationData =
                   model.Username.Length < MinUsernameLength ||
                   model.Password.Length < MinPasswordLength ||
                   model.ConfirmPassword != model.Password;

            if (invalidRegistrationData)
            {
                this.DisplayError(InvalidCredentials);

                return this.FileViewResponse(RegisterViewPath);
            }

            bool successfulCreation = this.users.Create(model.Username, model.Password);

            if (successfulCreation)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }
            else
            {
                this.DisplayError("This username is already taken.");

                return this.FileViewResponse(RegisterViewPath);
            }
        }

        public IHttpResponse Login()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(LoginViewPath);
        }

        public IHttpResponse Login(IHttpRequest req, LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                this.DisplayError("Please fill in all the fields.");

                return this.FileViewResponse(LoginViewPath);
            }

            var success = this.users.Find(model.Username, model.Password);

            if (success)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }
            else
            {
                this.DisplayError(InvalidCredentials);

                return this.FileViewResponse(LoginViewPath);
            }
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("The user is not logged in.");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.users.Profile(username);

            if (profile == null)
            {
                throw new InvalidOperationException($"The user {username} does not exist in the database.");
            }

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
            this.ViewData["totalOrders"] = profile.TotalOrders.ToString();

            return this.FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        private void SetDefaultViewData()
        {
            this.ViewData["authenticatedDisplay"] = "none";
        }

        private void LoginUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
