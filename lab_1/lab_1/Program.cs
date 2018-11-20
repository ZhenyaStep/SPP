using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace lab_1
{
    public delegate void TaskDelegate();

    

    public class PoolQueue
    {
        private BlockingCollection<TaskDelegate> FuncQueue =
        new BlockingCollection<TaskDelegate>(new ConcurrentQueue<TaskDelegate>());

        private int queueCount;

        public PoolQueue(int queueCount)
        {
            this.queueCount = queueCount;
            for (int i = 0; i < queueCount; i++)
            {
                var thread = new Thread(threadWork) { IsBackground = true }; ;
                thread.Start();
            }
        }

        public void threadWork()
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

        public void EnqueueTask(TaskDelegate task)
        {
            this.FuncQueue.Add(task);
        }
    }

    static class Program
    {
        static void doSmth()
        {
            Console.WriteLine("current proc id = " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            PoolQueue PoolQueue = new PoolQueue(3);
            Console.WriteLine("current proc id = " + Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 10; i++)
            {
                PoolQueue.EnqueueTask(doSmth);
            }
            Console.ReadLine();
        }

    }



}
