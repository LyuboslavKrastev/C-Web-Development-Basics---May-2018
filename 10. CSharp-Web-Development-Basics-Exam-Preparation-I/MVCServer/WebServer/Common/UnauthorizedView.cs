namespace WebServer.Common
{
    using Contracts;

    public class UnauthorizedView : IView
    {
        public string View()
        {
            return "<h1>Error 401 Unauthorized</h1>";
        }
    }
}
