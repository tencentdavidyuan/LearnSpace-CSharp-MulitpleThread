using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample {
    class TaskFactoryWhenExample {
        static void Main(string[] args) {
            //TaskFactoryWhenAll();
            TaskFactoryWhenAny();
            Console.ReadKey();
        }

        // 任务的延续
        public static void TaskFactoryWhenAll() {
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

            Task.Factory.ContinueWhenAll(new Task[3]{ taskA, taskB, taskC }, (task) => {
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " " + task.Length+ " " + DateTime.Now);

                
            });

            for (int i = 0; i < 50; ++i)
                Console.WriteLine(i);
        }

        public static void TaskFactoryWhenAny() {
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

            Task.Factory.ContinueWhenAny(new Task[3] { taskA, taskB, taskC }, (condition) => {
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " completed ! Time : " + DateTime.Now);
                Console.WriteLine("Tid = " + Thread.CurrentThread.ManagedThreadId + " " + condition.IsCompleted.ToString() + " " + DateTime.Now);
            });

            for (int i = 0; i < 50; ++i)
                Console.WriteLine(i);
        }
    }
}
