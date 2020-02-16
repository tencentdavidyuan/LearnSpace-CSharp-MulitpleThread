using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadExample
{
    class ThreadPoolTimer
    {
        static void Main(string[] args)
        {
            // AutoResetEvent 事件锁，它继承WaitHandle
            ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(true), new WaitOrTimerCallback((state, timeout) =>
            {
                Console.WriteLine("state = {0}, threadId = {1}, Time = {2}", state, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            }), "Hello World", 1000, false);

            Console.ReadKey();
        }
    }
}
