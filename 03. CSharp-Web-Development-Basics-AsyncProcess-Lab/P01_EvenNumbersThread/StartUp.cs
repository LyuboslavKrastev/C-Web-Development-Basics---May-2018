using System;
using System.Threading;

namespace P01_EvenNumbersThread
{
    public class StartUp
    {
        static void Main()
        {
            var input = Console.ReadLine().Split();
            var start = int.Parse(input[0]);
            var end = int.Parse(input[1]);

            var thread = new Thread(() => PrintEvenNumbers(start, end));
            thread.Start();
            thread.Join(); /* Waiting for the thread to complete its task */

            Console.WriteLine("The thread has finished its work");
        }

        public static void PrintEvenNumbers(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}

