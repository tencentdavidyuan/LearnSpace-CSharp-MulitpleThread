using System;
using System.Threading;

namespace ThreadExample
{
    class StartThread
    {
        static void Main(string[] args)
        {
            var thread = new Thread(PrintNumber);
            thread.Start();
            PrintNumber2();

            Console.ReadKey();
        }

        static void PrintNumber()
        {
            Console.WriteLine("Thread Start...");
            for (int i = 0; i < 100; ++i)
                Console.WriteLine("Thread Print" + i);
        }

        static void PrintNumber2()
        {
            for (int i = 0; i < 100; ++i)
                Console.WriteLine("MainThread" + i);
        }
    }
}
