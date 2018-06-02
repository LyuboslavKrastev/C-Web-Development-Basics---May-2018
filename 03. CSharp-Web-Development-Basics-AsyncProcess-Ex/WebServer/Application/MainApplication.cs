namespace WebServer.Application
{
    using Server.Contracts;
    using Application.Controllers;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", request => new HomeController().Index());

            appRouteConfig
               .Post(
                   "/register", req =>  new UserController()
                    .RegisterPost(req.FormData["name"]));

            appRouteConfig
              .Get(
                  "/register", req => new UserController()
                   .RegisterGet());

            appRouteConfig
                .Get(
                "/user/{(?<name>[A-Za-z]+)}",
                httpContext => new UserController().Details(httpContext.UrlParameters["name"]));
        }
    }
}
