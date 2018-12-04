using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;


namespace SPP_lab_5
{

    public delegate void TaskDelegate();

    public static class PoolQueue
    {
        private static BlockingCollection<TaskDelegate> FuncQueue =
        new BlockingCollection<TaskDelegate>(new ConcurrentQueue<TaskDelegate>());

        public static int getCountProcess()
        {
            return FuncQueue.Count;
        }
        
        public static void Start(int queueCount)
        {
            
            for (int i = 0; i < queueCount; i++)
            {
                var thread = new Thread(threadWork) { IsBackground = true }; ;
                thread.Start();
            }
        }
        
        public static void threadWork()
        {
            while (true)
            {
                var task = FuncQueue.Take();
                try
                {
                    task();
                }
                catch (ThreadStateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ThreadAbortException ex)
                {
                    Thread.ResetAbort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void EnqueueTask(TaskDelegate task)
        {
            FuncQueue.Add(task);
        }
    }


   


    class ParallelWaiting
    {
        //public PoolQueue poolQueue;

       

        public static void WaitAll(TaskDelegate[] tasks, int poolNum)
        {
            
            for (int i = 0; i < tasks.Length; i++)
            {
                PoolQueue.EnqueueTask(tasks[i]);
            }
            PoolQueue.Start(poolNum);
            while (PoolQueue.getCountProcess() != 0) { Thread.Sleep(50); };
        }
    }


    class Program
    {
        static void doSmth1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("func 1 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }

        static void doSmth2()
        {
            Thread.Sleep(100);
            Console.WriteLine("func 2 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }

        static void doSmth3()
        {
            Thread.Sleep(300);
            Console.WriteLine("func 3 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }

        static void doSmth4()
        {
            Thread.Sleep(100);
            Console.WriteLine("func 4 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }

        static void doSmth5()
        {
            Thread.Sleep(600);
            Console.WriteLine("func 5 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }

        static void doSmth6()
        {
            Thread.Sleep(10);
            Console.WriteLine("func 6 current proc id = " + Thread.CurrentThread.ManagedThreadId);
            
        }


        static void Main(string[] args)
        {
            TaskDelegate[] tasks = { doSmth1, doSmth2, doSmth3, doSmth4, doSmth5, doSmth6 };
            ParallelWaiting.WaitAll(tasks, 4);

            Console.ReadKey();
        }
    }
}
