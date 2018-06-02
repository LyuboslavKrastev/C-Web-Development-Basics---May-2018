namespace WebServer
{
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using Application;

    class Launcher : IRunnable
    {
        private const int port = 1984;

        private WebServer webServer;

        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var routeConfig = new AppRouteConfig();

            var mainApplication = new MainApplication();
            mainApplication.Configure(routeConfig);

            this.webServer = new WebServer(port, routeConfig);
            this.webServer.Run();
        }
    }
}
