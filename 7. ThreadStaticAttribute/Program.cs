using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadExample
{
    class ThreadStaticAttib
    {
        [ThreadStatic]
        static string username = string.Empty;

        static void Main(string[] args)
        {
            //ExampleMethod_1();
            //ExampleMethod_2();
            ExampleMethod_3();

            Console.ReadKey();
        }

        public static void ExampleMethod_1()
        {
            username = "hello world!";

            var workThread = new Thread(() =>
            {
                Console.WriteLine(Thread.CurrentThread.Name + " " + username);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine(Thread.CurrentThread.Name + " " + username);
        }

        public static void ExampleMethod_2()
        {
            var workThread = new Thread(() =>
            {
                username = "hello world!";
                Console.WriteLine(Thread.CurrentThread.Name + " " + username);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine(Thread.CurrentThread.Name + " " + username);
        }

        public static void ExampleMethod_3()
        {
            var workThread = new Thread(() =>
            {
                username = "Go Wuhan!";
                Console.WriteLine(Thread.CurrentThread.Name + " " + username);
            });
            workThread.Name = "Work Thread A";
            workThread.Start();

            username = "Go  China!";
            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine(Thread.CurrentThread.Name + " " + username);
        }
    }
}
