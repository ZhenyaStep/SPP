using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SPP_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 10;
            var mutex = new Mutex();
            for (int i = 0; i < N; i++)
            {
                new Thread(() =>
                {
                    mutex.Lock();
                    Console.WriteLine("current Thread id: " + Thread.CurrentThread.ManagedThreadId + " lock Thread");
                    Thread.Sleep(400);
                    Console.WriteLine("current Thread id: " + Thread.CurrentThread.ManagedThreadId + " unlock Thread");
                    mutex.Unlock();
                }
                ).Start();
            }
            Console.ReadKey();
        }
    }
}
