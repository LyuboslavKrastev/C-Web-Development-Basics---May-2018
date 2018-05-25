using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P03_SimpleWebServer
{
    public class StartUp
    {
        static void Main()
        {
            var port = 1984;

            var ipAdress = IPAddress.Parse("127.0.0.1");

            var tcpListener = new TcpListener(ipAdress, port);

            tcpListener.Start();

            Console.WriteLine("Server Started");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            var task = Task.Run(() => Connect(tcpListener));
            task.Wait();
        }

        public static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Awaiting for client...");
                var client = await listener.AcceptTcpClientAsync();
                
                var buffer = new byte[1024];

                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                if (buffer.Any(b => b!='0'))
                {
                    Console.WriteLine("Client connected.");
                }            

                var clientMessage = Encoding.ASCII.GetString(buffer);

                Console.WriteLine(clientMessage.Trim('\0'));

                var responseMessage = "HTTP / 1.1 200 OK\nContent - Type: text / plain\n\nWe shall meet in the place where there is no darkness.";

                var data = Encoding.ASCII.GetBytes(responseMessage);

                client.GetStream().Write(data, 0, data.Length);

                client.GetStream().Dispose();
               
            }
        }
    }
}

