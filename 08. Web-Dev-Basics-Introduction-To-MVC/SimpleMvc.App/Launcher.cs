namespace SimpleMvc.App
{
    using WebServer;
    using Framework;
    using Framework.Routers;
    using SimpleMvc.Data;
    using Microsoft.EntityFrameworkCore;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            using (var db = new NotesDbContext())
            {
                db.Database.Migrate();
            }

            var server = new WebServer(1984, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}
 