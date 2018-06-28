using MeTube.Data;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;
using System;
using WebServer;

namespace MeTube.App
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var server = new WebServer.WebServer(1984, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(server, new MeTubeAppcontext());
        }
    }
}
