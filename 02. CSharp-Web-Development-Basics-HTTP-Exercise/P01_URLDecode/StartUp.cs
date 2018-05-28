using System;
using System.Net;

namespace P01_URLDecode
{
    class StartUp
    {
        static void Main()
        {
            string result = WebUtility.UrlDecode(Console.ReadLine());
            Console.WriteLine(result);
        }
    }
}
