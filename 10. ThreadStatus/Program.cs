using System;
using System.Threading;

namespace ThreadExample
{
    class ThreadStateExample
    {
        static void Main(string[] args)
        {
            var workThreadA = new Thread(PrintNumbersWithStatus);
            workThreadA.Name = "Work Thread A";

            var workThreadB = new Thread(DoNothing);
            workThreadB.Name = "Work Thread B";

            Thread.CurrentThread.Name = "Main Thread";

            Console.WriteLine(workThreadA.Name + " " + workThreadA.ThreadState.ToString());
            workThreadB.Start();
            workThreadA.Start();

            
            for (int i = 0; i < 30; ++i)
            {
                Console.WriteLine("[MainThread Print]  workThreadA " + workThreadA.ThreadState.ToString() + " " + i);
            }
            Thread.Sleep(TimeSpan.FromSeconds(10));
            workThreadA.Abort();

            Console.WriteLine("WorkThreadA has been aborted!");
            Console.WriteLine(workThreadA.Name + " " + workThreadA.ThreadState.ToString());
            Console.WriteLine(workThreadB.Name + " " + workThreadB.ThreadState.ToString());

            Console.ReadKey();
        }

        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " " + "Starting...");
            Console.WriteLine(Thread.CurrentThread.Name + " " + Thread.CurrentThread.ThreadState.ToString());
            for(int i = 0; i < 100; ++i)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " " + i);
            }
        }
    }
}
