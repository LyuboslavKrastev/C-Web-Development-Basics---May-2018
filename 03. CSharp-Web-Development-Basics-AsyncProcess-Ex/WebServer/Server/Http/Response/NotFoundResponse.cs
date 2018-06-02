namespace WebServer.Server.Http.Response
{
    using Enums;
    using Application.Views;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
            : base(HttpStatusCode.NotFound, new NotFoundView())
        {
        }
    }
}
