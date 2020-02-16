using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample {
    class ThreadMoniterPlus {

        // 当一个线程尝试着lock一个同步对象的时候，该线程就在就绪队列中排队。
        // 一旦没人拥有该同步对象，就绪队列中的线程就可以占有该同步对象。这也是我们平时最经常用的lock方法。
        // 为了其他的同步目的，占有同步对象的线程也可以暂时放弃同步对象，并把自己流放到等待队列中去。
        // 这就是Monitor.Wait。由于该线程放弃了同步对象，其他在就绪队列的排队者就可以进而拥有同步对象。
        // 比起就绪队列来说，在等待队列中排队的线程更像是二等公民：他们不能自动得到同步对象，甚至不能自动升舱到就绪队列。
        // 而Monitor.Pulse的作用就是开一次门，使得一个正在等待队列中的线程升舱到就绪队列；
        // 相应的Monitor.PulseAll则打开门放所有等待队列中的线程到就绪队列。
        static void Main(string[] args) {
            //MoniterPlusAll();

            //MoniterPlus();

            //MoniterNoPlus();

            //MultipleMoniterPlus();

            //MultipleMoniterPlusAll_Error();

            MultipleMoniterPlusAll_Correct();

            int id = Environment.CurrentManagedThreadId;

            Console.ReadKey();
        }

        public static void MoniterPlusAll() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");

                    Console.WriteLine("Work ThreadC Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }

        public static void MoniterPlus() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.Pulse(objectLock);

                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.Pulse(objectLock);

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");

                    Console.WriteLine("Work ThreadC Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.Pulse(objectLock);

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }

        public static void MoniterNoPlus() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    //Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的程序进入就绪队列");
                    //Monitor.Pulse(objectLock);

                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }
                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    //Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的程序进入就绪队列");
                    //Monitor.Pulse(objectLock);

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");

                    //Console.WriteLine("Work ThreadC Pulse()! 通知等待队列的程序进入就绪队列");
                    //Monitor.Pulse(objectLock);

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }

        public static void MultipleMoniterPlus() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.Pulse(objectLock);
                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }
                    Console.WriteLine("Work ThreadA Terminate....!");
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的程序进入就绪队列");
                    Monitor.Pulse(objectLock);
                    Console.WriteLine("Work ThreadB Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);


                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadB Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");
                              
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Monitor Wait, 通知等待队列的程序进入就绪队列 !");
                    Monitor.Pulse(objectLock);
                    Console.WriteLine("Work ThreadC Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadC Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }

        public static void MultipleMoniterPlusAll_Error() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Terminate....!");
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadB Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);


                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadB Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Monitor Wait, 通知等待队列的所有线程进入就绪队列 !");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadC Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadC Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }

        public static void MultipleMoniterPlusAll_Correct() {
            object objectLock = new object();
            int sum = 0;

            // 工作线程A优先获得objectLock

            var workThreadA = new Thread(() => {
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadA Start......!");

                    Console.WriteLine("Work ThreadA Sleep 5s!");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadA Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadA Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread A " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    Console.WriteLine("Work ThreadA Terminate....!");
                }
            });

            var workThreadB = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadB Start......!");
                    Console.WriteLine("Work ThreadB Sleep 3s!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadB Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadB Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);


                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadB Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread B " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    Console.WriteLine("Work ThreadB Terminate....!");
                }
            });

            var workThreadC = new Thread(() => {
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //进入就绪队列 
                lock (objectLock) {
                    Console.WriteLine("Work ThreadC Start......!");

                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadC Monitor Wait, 通知等待队列的所有线程进入就绪队列 !");
                    Monitor.PulseAll(objectLock);
                    Console.WriteLine("Work ThreadC Monitor Wait, 放入等待队列中 !");
                    Monitor.Wait(objectLock);

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Work ThreadC Calculate");
                    for (int i = 0; i < 10; ++i) {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        sum += i;
                        Console.WriteLine("Work Thread C " + i + " sum = " + sum);
                    }

                    Console.WriteLine("Work ThreadA Pulse()! 通知等待队列的所有线程进入就绪队列");
                    Monitor.PulseAll(objectLock);

                    Console.WriteLine("Work ThreadC Terminate....!");
                }
            });

            workThreadA.Start();
            workThreadB.Start();
            workThreadC.Start();


            Console.WriteLine("Main Thread Finish！");
        }
    }
}
