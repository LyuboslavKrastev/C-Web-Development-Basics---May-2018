using KittenApp.Data;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;

namespace KittenApp.App
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var server = new WebServer.WebServer(1984, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(server, new KittenAppContext());
        }
    }
}
