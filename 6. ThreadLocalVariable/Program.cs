using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadExample
{
    class ThreadLocalVariable
    {
        static void Main(string[] args)
        {
            //ExampleMethod_1();
            //ExampleMethod_2();
            ExampleMethod_3();

            Console.ReadKey();
        }

        static void ExampleMethod_1()
        {
            ThreadLocal<string> local = new ThreadLocal<string>();
            local.Value = "Go Wuha!";

            var workThread = new Thread(() =>
            {
                Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
        }

        static void ExampleMethod_2()
        {
            ThreadLocal<string> local = new ThreadLocal<string>();            

            var workThread = new Thread(() =>
            {
                local.Value = "Go Wuha!";
                Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
        }

        static void ExampleMethod_3()
        {
            ThreadLocal<string> local = new ThreadLocal<string>();
            local.Value = "Go China!";

            var workThread = new Thread(() =>
            {
                local.Value = "Go Wuha!";
                Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine("[ThreadLocal]" + Thread.CurrentThread.Name + " " + local.Value);
        }
    }
}
