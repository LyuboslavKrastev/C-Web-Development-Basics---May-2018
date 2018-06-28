using SimpleMvc.Framework.Attributes.Security;
using WebServer.Http.Contracts;
using WebServer.Http.Response;

namespace MeTube.App.Attributes
{
    public class AuthotirzeLoginAttribute : PreAuthorizeAttribute
    {
        public override IHttpResponse GetResponse(string message)
        {
            return new RedirectResponse("/users/login");
        }
    }
}
