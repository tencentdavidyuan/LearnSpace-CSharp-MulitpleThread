using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{
    class ThreadMonitorExample
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            // 下面两句是等价的
            //new Thread(() => LockTooMuch(lockA, lockB)).Start();
            var workThreadA = new Thread(() => LockTooMuch(lockA, lockB));
            workThreadA.Start();

            lock (lockB)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Moniter.TryEnter allows not to get stuck, returning false after a specified timeout is elapsed");

                //Monitor.TryEnter启动锁，
                //也是区别于Lock的功能，多了一个时间设置，就是等待多少时间后如果还不能进入，则取消此次操作。而Lock则会一直等待下去。
                if (Monitor.TryEnter(lockA, TimeSpan.FromSeconds(5)))
                {
                    Console.WriteLine("Acquired a protected resource succesfully");
                }
                else
                {
                    Console.WriteLine("Timeout acquiring a resource!");
                }

            }

            Console.ReadKey();
        }

        static void LockTooMuch(object lockA, object lockB)
        {
            lock (lockA)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                lock (lockB) ;
            }
        }
    }
}
