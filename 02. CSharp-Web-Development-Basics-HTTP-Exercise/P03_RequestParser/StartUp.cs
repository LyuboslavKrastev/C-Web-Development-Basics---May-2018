using System;
using System.Collections.Generic;
using System.Linq;

namespace P03_RequestParser
{
    class Program
    {     
        private static string httpResponseTemplate = "{0} {1} {2}" +
                  Environment.NewLine + "Content-Length: {3}" +
                  Environment.NewLine + "Content-Type: text/plain" +
                  Environment.NewLine + "{4}";

        static void Main()
        {
            var validPaths = new Dictionary<string, HashSet<string>>(); /* Valid Path - Valid Methods */

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] urlParts = input.ToLower().Split("/", StringSplitOptions.RemoveEmptyEntries);
              
                string path = $"/{urlParts[0]}";
                string method = urlParts[1];

                if (!validPaths.ContainsKey(path))
                {
                    validPaths[path] = new HashSet<string>();
                }

                validPaths[path].Add(method);
            }

            string[] requestArgs = Console.ReadLine().Split();
            string requestMethod = requestArgs[0].ToLower();
            string requestUrl = requestArgs[1].ToLower();
            string requestProtocol = requestArgs[2];

            bool successfulRequest = validPaths.ContainsKey(requestUrl)
                && validPaths[requestUrl].Any(m => m == requestMethod);

            int responseStatus = successfulRequest ? 200 : 404;
            string responseStatusText = successfulRequest ? "OK" : "Not Found";

            string response = string.Format(httpResponseTemplate,
                requestProtocol, responseStatus, responseStatusText, 
                responseStatusText.Length, 
                responseStatusText);

            Console.WriteLine("RESPONSE:");
            Console.WriteLine(response);
        }
    }
}
