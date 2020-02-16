using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample {
    class TaskWaitExample {
        static void Main(string[] args) {
            //ThreadJoin();
            TaskWait();
           //TaskWaitAll();
           //TaskWaitAny();

            Console.ReadKey();
        }

        public static void ThreadJoin() {
            Thread.CurrentThread.Name = "Main Thread";

            var workThreadA = new Thread(() => {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });
            workThreadA.Name = "Work Thread A";
            

            var workThreadB = new Thread(() => {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });
            workThreadB.Name = "Work Thread B";

            var workThreadC = new Thread(() => {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });
            workThreadC.Name = "Work Thread B";

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();

            workThreadA.Join();
            workThreadB.Join();
            workThreadC.Join();


            Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
        }

        public static void TaskWait() {
            Thread.CurrentThread.Name = "Main Thread";

            var taskA = new Task(() => {
                Thread.CurrentThread.Name = "Task A";
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskB = new Task(() => {
                Thread.CurrentThread.Name = "Task B";
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskC = new Task(() => {
                Thread.CurrentThread.Name = "Task C";
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            taskA.Start();
            taskB.Start();
            taskC.Start();
            taskA.Wait();   // 同Thread.Join()
            taskB.Wait();
            taskC.Wait();


            Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
        }

        public static void TaskWaitAll() {
            Thread.CurrentThread.Name = "Main Thread";

            var taskA = new Task(() => {
                Thread.CurrentThread.Name = "Task A";
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskB = new Task(() => {
                Thread.CurrentThread.Name = "Task B";
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskC = new Task(() => {
                Thread.CurrentThread.Name = "Task C";
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            taskA.Start();
            taskB.Start();
            taskC.Start();
            Task.WaitAll(taskA, taskB, taskC);


            Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
        }

        public static void TaskWaitAny() {
            Thread.CurrentThread.Name = "Main Thread";

            var taskA = new Task(() => {
                Thread.CurrentThread.Name = "Task A";
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskB = new Task(() => {
                Thread.CurrentThread.Name = "Task B";
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            var taskC = new Task(() => {
                Thread.CurrentThread.Name = "Task C";
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
            });

            taskA.Start();
            taskB.Start();
            taskC.Start();
            Task.WaitAny(taskA, taskB, taskC);


            Console.WriteLine(Thread.CurrentThread.Name + " Completed! Time : " + DateTime.Now);
        }
    }
}
