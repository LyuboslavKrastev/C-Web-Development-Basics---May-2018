namespace NotesApp
{
    using WebServer;

    using SimpleMvc.Framework.Routers;
    using SimpleMvc.Framework;
    using NotesApp.Data;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var server = new WebServer(1984, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(server, new NotesAppContext());
        }
    }
}
 