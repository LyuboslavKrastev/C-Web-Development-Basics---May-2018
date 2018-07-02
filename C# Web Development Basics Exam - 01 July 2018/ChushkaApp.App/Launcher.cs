using ChushkaApp.Data;
using Microsoft.EntityFrameworkCore;
using SoftUni.WebServer.Mvc;
using SoftUni.WebServer.Mvc.Routers;
using SoftUni.WebServer.Server;

namespace ChushkaApp.App
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var server = new WebServer(1984, new ControllerRouter(), new ResourceRouter());
            ConfigureDatabaseContext(new CshushkaAppContext());
            MvcEngine.Run(server);
        }

        public static void ConfigureDatabaseContext(DbContext context)
        {
            using (context)
            {
                context.Database.Migrate();
            }
        }
    }
}
