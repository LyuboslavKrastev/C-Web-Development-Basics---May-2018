namespace CustomWebServer
{
    using ByTheCakeApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        private const int port = 1984;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var application = new ByTheCakeApp();
            application.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            application.Configure(appRouteConfig);

            var CustomWebServer = new CustomWebServer(port, appRouteConfig);

            CustomWebServer.Run();
        }
    }
}
