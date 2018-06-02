namespace WebServer.Server
{
    using Contracts;
    using Routing;
    using Routing.Contracts;
    using System.Net;
    using System.Net.Sockets;
    using System;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private const string defaultIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener listener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            this.port = port;

            this.listener = new TcpListener(IPAddress.Parse(defaultIpAddress), port);

            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);
        }

        public void Run()
        {
            this.listener.Start();

            this.isRunning = true;

            Console.WriteLine($"Server running on {defaultIpAddress}:{this.port}");

            Task.Run(this.ListenLoop).Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                var client = await this.listener.AcceptSocketAsync();

                var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);

                var connection = connectionHandler.ProcessRequestAsync();

                connection.Wait();
            }
        }
    }
}
