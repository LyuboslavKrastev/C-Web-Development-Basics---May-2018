namespace CustomWebServer.GameStoreApplication.Controllers
{
    using Services.Contracts;
    using Infrastructure;
    using Server.Http.Contracts;
    using Services;
    using Common;
    using Server.Http;

    public abstract class BaseController : Controller
    {
        protected const string HomePath = "/";

        private readonly IUserService users;

        protected BaseController(IHttpRequest request)
        {
            this.Request = request;
            this.Authentication = new Authentication(false, false);

            this.users = new UserService();

            this.Authenticate();
        }

        protected IHttpRequest Request { get; private set; }

        protected Authentication Authentication { get; private set; }

        protected override string ApplicationDirectory => @"..\..\..\GameStoreApplication";

        private void Authenticate()
        {
            var anonymousDisplay = "flex";
            var authDisplay = "none";
            var adminDisplay = "none";

            var authenticatedUserEmail = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (authenticatedUserEmail != null)
            {
                anonymousDisplay = "none";
                authDisplay = "flex";

                var isAdmin = this.users.IsAdmin(authenticatedUserEmail);

                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonymousDisplay;
            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
        }
    }
}
