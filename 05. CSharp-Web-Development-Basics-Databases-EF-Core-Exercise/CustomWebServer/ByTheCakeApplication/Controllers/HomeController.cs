namespace CustomWebServer.ByTheCakeApplication.Controllers
{
    using Server.Http.Contracts;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse(@"home\about");
        }
    }
}
