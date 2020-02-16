using System;
using System.Threading;

namespace ThreadExample
{
    class AbortThread
    {
        static void Main(string[] args)
        {
            var workThread = new Thread(PrintNumberWithDelay);
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.Sleep(TimeSpan.FromSeconds(6));
            workThread.Abort();
            Console.WriteLine(workThread.Name + " Abort");

            workThread = new Thread(PrintNumberWithDelay);
            workThread.Name = "Work Thread B";
            workThread.Start();

            PrintNumber();

            Console.ReadKey();
        }

        static void PrintNumberWithDelay()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Start...");
            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " " + i);
            }

        }

        static void PrintNumber()
        {
            Thread.CurrentThread.Name = "Main Thread";
            for (int i = 0; i < 100; ++i)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " " + i);
            }
        }
    }
}
