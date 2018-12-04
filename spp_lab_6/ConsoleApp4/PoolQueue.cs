using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace ConsoleApp4
{ 

    public class PoolQueue
    {
        private BlockingCollection<MyDelegate> FuncQueue =
        new BlockingCollection<MyDelegate>(new ConcurrentQueue<MyDelegate>());

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

        public void EnqueueTask(MyDelegate task)
        {
            this.FuncQueue.Add(task);
        }
    }
}
