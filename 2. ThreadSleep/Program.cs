using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{
    class ThreadSleep
    {
        static void Main(string[] args)
        {
            var thread = new Thread(PrintNumberWithDelay);
            thread.Name = "Work Thread";
            thread.Start();

            PrintNumber();

            Console.ReadKey();        
        }

        static void PrintNumberWithDelay()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Start... !");
            for (int i = 0; i < 10; ++i)
            {                
                Console.WriteLine(Thread.CurrentThread.Name + " " + i);
                Thread.Sleep(TimeSpan.FromMilliseconds(25));
            }
                
        }

        static void PrintNumber()
        {
            Thread.CurrentThread.Name = "Main Thread";
            for (int i = 0; i < 100; ++i)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));
                Console.WriteLine(Thread.CurrentThread.Name + " " + i);
            }
        }
    }
}
