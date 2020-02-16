using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{
    class ThreadDataSlot
    {
        static void Main(string[] args)
        {
            MethodA();
            //MethodB();
            //MethodC();

            Console.ReadKey();
        }


        static void MethodA()
        {
            // 设置主线程名称
            Thread.CurrentThread.Name = "Main Thread";

            // 为所有线程分配未命名的“数据槽位”
            var slot = Thread.AllocateDataSlot();
            // 为所有线程分配命名（username）的“数据槽位”
            //var slot = Thread.AllocateNamedDataSlot("username");

            // 在主线程设置指定槽位的数据
            Thread.SetData(slot, "Hello World!");

            // 启动工作线程
            var wordThread = new Thread(() =>
            {
                // 在工作线程获取指定槽位的数据
                Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
            });
            wordThread.Name = "Work Thread A";
            wordThread.Start();

            // 在主线程获取指定槽位的数据
            Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
        }

        static void MethodB()
        {
            // 设置主线程名称
            Thread.CurrentThread.Name = "Main Thread";

            // 为所有线程分配未命名的“数据槽位”
            var slot = Thread.AllocateDataSlot();
            // 为所有线程分配命名（username）的“数据槽位”
            //var slot = Thread.AllocateNamedDataSlot("username");
            
            // 启动工作线程
            var wordThread = new Thread(() =>
            {
                // 在工作线程设置指定槽位的数据
                Thread.SetData(slot, "Hello World!");
                // 在工作线程获取指定槽位的数据
                Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
            });
            wordThread.Name = "Work Thread A";
            wordThread.Start();

            // 打印主线程的名称和槽位数据
            Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
        }

        static void MethodC()
        {
            // 设置主线程名称
            Thread.CurrentThread.Name = "Main Thread";

            // 为所有线程分配未命名的“数据槽位”
            var slot = Thread.AllocateDataSlot();
            // 为所有线程分配命名（username）的“数据槽位”
            //var slot = Thread.AllocateNamedDataSlot("username");
            Thread.SetData(slot, "Hello World A !");

            // 启动工作线程
            var wordThread = new Thread(() =>
            {
                // 在工作线程设置指定槽位的数据
                Thread.SetData(slot, "Hello World B !");
                // 在工作线程获取指定槽位的数据
                Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
            });
            wordThread.Name = "Work Thread A";
            wordThread.Start();

            // 打印主线程的名称和槽位数据
            Console.WriteLine(Thread.CurrentThread.Name + " - Slot Data - : " + Thread.GetData(slot));
        }
    }
}
