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
        static void doSmth()
        {
            Console.WriteLine("func 1, current proc id = " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
        }

        static void doSmth2()
        {
            Console.WriteLine("func2, current proc id = " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            TaskDelegate[] tasks = { doSmth, doSmth2, doSmth, doSmth2, doSmth, doSmth2 };
            ParallelWaiting.WaitAll(tasks, 2);

            Console.ReadKey();
        }
    }
}
