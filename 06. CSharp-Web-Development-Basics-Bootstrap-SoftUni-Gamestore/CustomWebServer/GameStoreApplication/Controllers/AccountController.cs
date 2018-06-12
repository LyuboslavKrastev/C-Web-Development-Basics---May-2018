namespace CustomWebServer.GameStoreApplication.Controllers
{
    using ViewModels.Account;
    using Server.Http.Contracts;
    using Server.Http;
    using Services;
    using Services.Contracts;
    using CustomWebServer.GameStoreApplication.ViewModels;

    public class AccountController : BaseController
    {
        private const string LoginView = @"account\login";
        private const string RegisterView = @"account\register";

        private readonly IUserService users;

        public AccountController(IHttpRequest request) 
            : base(request)
        {
            this.users = new UserService();
        }

        protected override string ApplicationDirectory => @"..\..\..\GameStoreApplication";

        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterView);
        }

        public IHttpResponse Register(RegisterViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Register();
            }

            bool success = this.users
                .Create(model.Email, model.FullName, model.Password);

            if (!success)
            {
                this.DisplayError("This E-mail address is already taken.");
                return this.Register();
            }
            else
            {
               return this.LoginUser(model.Email);
            }
        }

        public IHttpResponse Login()
        {         
            return this.FileViewResponse(LoginView);
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Login();
            }

            bool success = this.users.Find(model.Email, model.Password);

            if (!success)
            {
                this.DisplayError("Invalid E-mail address or password.");
                return this.Login();
            }
            else
            {
               return this.LoginUser(model.Email);
            }
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return this.RedirectResponse(HomePath);
        }

        private IHttpResponse LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
            this.Request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
            return this.RedirectResponse(HomePath);
        }
    }
}
