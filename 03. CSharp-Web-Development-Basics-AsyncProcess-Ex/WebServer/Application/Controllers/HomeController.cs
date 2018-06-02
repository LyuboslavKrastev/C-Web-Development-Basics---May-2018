namespace WebServer.Application.Controllers
{
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Enums;
    using Views.Home;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new IndexView());
        }
    }
}
