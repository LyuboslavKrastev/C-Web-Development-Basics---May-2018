using System;
using System.Net;

namespace P02_ValidateURL
{
    class StartUp
    {
        private const int ValidHttpPort = 80;
        private const int ValidHttpsPort = 443; /* NOTE: There is a mistake in the second example, where 447 is accepted as a valid HTTPS port. */

        static void Main()
        {                   
            try
            {
                string decodedUrl = WebUtility.UrlDecode(Console.ReadLine());
                Uri parsedUrl = new Uri(decodedUrl); /* Using a built in System.Net Class instead of Regex */

                bool invalidProtocol = string.IsNullOrWhiteSpace(parsedUrl.Scheme);  /* Scheme == Protocol */
                bool invalidHost = string.IsNullOrWhiteSpace(parsedUrl.Host);
                bool invalidHttpsPort = parsedUrl.Scheme == "https" && parsedUrl.Port != ValidHttpsPort;
                bool invalidHttpPort = parsedUrl.Scheme == "http" && parsedUrl.Port != ValidHttpPort;

                if (invalidProtocol || invalidHost || invalidHttpsPort || invalidHttpPort)
                {
                    throw new ArgumentException();
                }

                PrintParts(parsedUrl);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
            }                    
        }

        private static void PrintParts(Uri parsedUrl)
        {
            Console.WriteLine($"Protocol: {parsedUrl.Scheme}");
            Console.WriteLine($"Host: {parsedUrl.Host}");
            Console.WriteLine($"Port: {parsedUrl.Port}");
            Console.WriteLine($"Path: {parsedUrl.AbsolutePath}");

            if (!string.IsNullOrWhiteSpace(parsedUrl.Query))
            {
                Console.WriteLine($"Query: {parsedUrl.Query.Substring(1)}");
            }
            if (!string.IsNullOrWhiteSpace(parsedUrl.Fragment))
            {
                Console.WriteLine($"Fragment: {parsedUrl.Fragment.Substring(1)}");
            }
        }
    }
}
