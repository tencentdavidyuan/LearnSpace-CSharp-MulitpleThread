using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheadExample
{
    class TaskBase
    {
        // Task的底层都是由不同的TaskScheduler支持的
        // TaskScheduler是Task的CPU（处理器）
        // 默认的TaskScheduler是ThreadPoolTaskScheduler

        static void Main(string[] args)
        {
            // 异步方式，启动task
            //StartTaskMethod_1();
            //StartTaskMethod_2();
            //StartTaskMethod_3();

            // 同步方式，启动task
            //StartTaskMethod_4();

            TaskWithResult();

            Console.ReadKey();
        }

        public static void StartTaskMethod_1()
        {
            var taskA = new Task(() =>
            {
                Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId);
            });
            taskA.Start();

            //new Task(() => Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId)).Start();
        }

        public static void StartTaskMethod_2()
        {
            // 使用Task.Factory
            var taskA = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId);
            });


            //Task.Factory.StartNew(() => Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId));
        }

        public static void StartTaskMethod_3()
        {
            // 使用Task.Run启动，在.Net4.0中
            var task = Task.Run(() =>
            {
                Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId);
            });
        }

        public static void StartTaskMethod_4()
        {
            var taskA = new Task(() => {
                Console.WriteLine("Thread ID = {0},", Thread.CurrentThread.ManagedThreadId);
            });

            // 先执行完TaskA，然后继续执行主线程
            taskA.RunSynchronously();

            Console.WriteLine("Main Thread");
        }

        public static void TaskWithResult()
        {
            var taskA = new Task<int>(() => {
                int sum = 0;
                for (int i = 1; i <= 100; ++i)
                {
                    sum += i;
                    Console.WriteLine("Task i = {0}, Sum = {1}", i, sum);
                }
                    
                return sum;
            });
            taskA.Start();

//             for (int i = 100; i < 200; ++i)
//             {
//                 Console.WriteLine("Main Thread i = {0}", i);
//             }

            Console.WriteLine("Is Task A Complete!");
            if (taskA.IsCompleted)
            {
                Console.WriteLine("Task Result : " + taskA.Result + " here 1");
            }
            else
            {
                Console.WriteLine("Task Result : " + taskA.Result + " here 2");
            }
        }
            

            
    }


}
