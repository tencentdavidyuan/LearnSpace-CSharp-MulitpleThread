using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{
    class ThreadLockExample
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Incorrect counter");

            var counter = new Counter();

            var workThread1 = new Thread(() => TestCounter(counter));
            var workThread2 = new Thread(() => TestCounter(counter));
            var workThread3 = new Thread(() => TestCounter(counter));

            workThread1.Start();
            workThread2.Start();
            workThread3.Start();

            workThread1.Join();
            workThread2.Join();
            workThread3.Join();

            Console.WriteLine("Total count: {0}", counter.Count);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Correct counter");

            var counterLocked = new CounterWithLock();

            var workThread4 = new Thread(() => TestCounter(counterLocked));
            var workThread5 = new Thread(() => TestCounter(counterLocked));
            var workThread6 = new Thread(() => TestCounter(counterLocked));
            workThread4.Start();
            workThread5.Start();
            workThread6.Start();

            workThread4.Join();
            workThread5.Join();
            workThread6.Join();

            Console.WriteLine("Total Count : {0}", counterLocked.Count);

            Console.ReadKey();
        }

        public abstract class CounterBase
        {
            public abstract void Increment();
            public abstract void Decrement();
        }


        /// <summary>
        /// 使用lock关键字
        /// 如果锁定了一个对象，需要访问对象的所有其他线程则会处于阻塞状态，并
        /// 等待直到该对象解除锁定。
        /// 缺点：可能会导致严重的性能问题。
        /// </summary>
        public class CounterWithLock : CounterBase
        {
            private readonly object _syncRoot = new Object();
            public int Count { get; private set; }
            public override void Decrement()
            {
                lock (_syncRoot)
                {
                    Count++;
                }
            }

            public override void Increment()
            {
                lock (_syncRoot)
                {
                    Count--;
                }
            }
        }


        // race conditon (竞争条件）
        public class Counter : CounterBase
        {
            public int Count { get; private set; }
            public override void Decrement()
            {
                Count++;
            }

            public override void Increment()
            {
                Count--;
            }
        }

        static void TestCounter(CounterBase c)
        {
            for(int i = 0; i < 10000; ++i)
            {
                c.Increment();
                c.Decrement();
            }
        }




    }
}
