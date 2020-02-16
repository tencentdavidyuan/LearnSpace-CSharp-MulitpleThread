using System;
using System.Threading;

namespace ThreadExample
{
    class ThreadJoin
    {
        static void Main(string[] args)
        {
            var thread = new Thread(PrintNumberWithDelay);
            thread.Name = "Work Thread";
            thread.Start();

            // 主线程等待工作线程结束后才继续执行。
            thread.Join();

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
