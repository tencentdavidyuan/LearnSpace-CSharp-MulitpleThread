using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadExample {
    class TResultTask {
        static void Main(string[] args) {
            TaskWithResult();
            //TaskWhenAllWithResult();
            //TaskWhenAllWithResultNoWait();

            Console.ReadKey();
        }

        public static void TaskWithResult() {
            var taskA = new Task<long>(() => {
                long sum = 0;
                for (long i = 0; i < 100000; ++i) {
                    sum += i;
                    Console.WriteLine("TaskA " + i + " sum " + sum);
                }
                    
                return sum;
            });
            taskA.Start();

            Console.WriteLine("Task.Reslut " + taskA.Result);
        }

        public static void TaskWhenAllWithResult() {
            var taskA = new Task<int>(() => {
                int sum = 0;
                for (int i = 0; i < 100; ++i) {
                    sum += i;
                    Console.WriteLine("TaskA " + i + " sum " + sum);
                }

                return sum;
            });

            var taskB = new Task<int>(() => {
                int sum = 0;
                for (int i = 100; i < 200; ++i) {
                    sum += i;
                    Console.WriteLine("TaskB " + i + " sum " + sum);
                }

                return sum;
            });

            taskA.Start();
            taskB.Start();

            var task = Task.WhenAll<int>(new Task<int>[2] { taskA, taskB }).ContinueWith((tasks) => {
                Console.WriteLine("TaskA ,TaskB Complete!!!");

                int sum = 0;

                int[] results = tasks.Result;
                foreach (int result in results) {
                    sum += result;
                }
                return sum;
            });
            task.Wait();

            Console.WriteLine("Task.Reslut " + task.Result);
        }

        public static void TaskWhenAllWithResultNoWait() {
            var taskA = new Task<int>(() => {
                int sum = 0;
                for (int i = 0; i < 100; ++i) {
                    sum += i;
                    Console.WriteLine("TaskA " + i + " sum " + sum);
                }

                return sum;
            });

            var taskB = new Task<int>(() => {
                int sum = 0;
                for (int i = 100; i < 200; ++i) {
                    sum += i;
                    Console.WriteLine("TaskB " + i + " sum " + sum);
                }

                return sum;
            });

            taskA.Start();
            taskB.Start();

            var task = Task.WhenAll<int>(new Task<int>[2] { taskA, taskB }).ContinueWith((tasks) => {
                Console.WriteLine("TaskA ,TaskB Complete!!!");

                int sum = 0;

                int[] results = tasks.Result;
                foreach (int result in results) {
                    sum += result;
                }
                return sum;
            });
            //task.Wait();

            Console.WriteLine("Task.Reslut " + task.Result);
        }
    }
}
