using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample {
    class TaskWhenExample {
        static void Main(string[] args) {
            //TaskWhenAll();
            TaskWhenAny();
            Console.ReadKey();
        }

        // 任务的延续
        public static void TaskWhenAll() {
            var taskA = Task.Factory.StartNew(() => {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });

            var taskB = Task.Run(() => {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });

            var taskC = new Task(() => {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });
            taskC.Start();

            Task.WhenAll(taskA, taskB, taskC).ContinueWith((obj) => {
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " " + obj.IsCompleted.ToString() + " " + DateTime.Now);
            });

            for (int i = 0; i < 50; ++i)
                Console.WriteLine(i);
        }

        public static void TaskWhenAny() {
            var taskA = Task.Factory.StartNew(() => {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });

            var taskB = Task.Run(() => {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });

            var taskC = new Task(() => {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
            });
            taskC.Start();

            Task.WhenAny(taskA, taskB, taskC).ContinueWith((obj) => {
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " " + obj.IsCompleted.ToString() + " " + DateTime.Now);
            });

            for (int i = 0; i < 50; ++i)
                Console.WriteLine(i);
        }
    }
}
