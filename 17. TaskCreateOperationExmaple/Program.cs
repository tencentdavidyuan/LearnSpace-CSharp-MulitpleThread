using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample {
    class TaskCreateOperationExmaple {
        static void Main(string[] args) {

            //AttachedToParent();
            //DenyChildAttach();
            LongRunning();

            Console.ReadKey();
        }

        public static void AttachedToParent() {
            // TaskCreationOptions.AttachedToParent，让子任务附加到主任务上。
            // 用途：子任务附加上父任务上都，父任务要等所有子任务执行完成后才执行其自身。
            // 与WaitAll的同。
            var task = new Task(() => {
                var childTaskA = new Task(() => {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    Console.WriteLine("Sub Task A Complete! Time : " + DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                var childTaskB = new Task(() => {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    Console.WriteLine("Sub Task B Complete! Time : " + DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                childTaskA.Start();
                childTaskB.Start();
            });
            task.Start();
            task.Wait();
            Console.WriteLine("Main Task Complete! Time : " + DateTime.Now);

            Console.WriteLine("Main Thread Complete! Time : " + DateTime.Now);
        }

        public static void DenyChildAttach() {

            // TaskCreationOptions.DenyChildAttach禁止子任务附加到父任务上。
            var task = new Task(() => {
                var childTaskA = new Task(() => {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    Console.WriteLine("Sub Task A Complete! Time : " + DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                var childTaskB = new Task(() => {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    Console.WriteLine("Sub Task B Complete! Time : " + DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                childTaskA.Start();
                childTaskB.Start();
            }, TaskCreationOptions.DenyChildAttach);
            task.Start();
            task.Wait();
            Console.WriteLine("Main Task Complete! Time : " + DateTime.Now);

            Console.WriteLine("Main Thread Complete! Time : " + DateTime.Now);
        }

        public static void LongRunning() {
            var task = new Task(() => {
                Console.WriteLine("Task Start! Time : " + DateTime.Now);
                for (int i = 0; i < 120; ++i) {
                    Console.WriteLine("Count " + i + " Time : " + DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }                    
                Console.WriteLine("Main Task Complete! Time : " + DateTime.Now);
            }, TaskCreationOptions.LongRunning);
            task.Start();
            task.Wait();

            Console.WriteLine("Main Thread Complete! Time : " + DateTime.Now);
        }
    }
}
