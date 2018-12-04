using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SPP_3
{
    public class Mutex
    {
        private int curId = -1;

        public void Lock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            while(Interlocked.CompareExchange(ref this.curId,
                id, -1) != -1)
            {
                Thread.Sleep(10);
            }
        }

        public void Unlock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref this.curId,
                -1, id);
        }
    }
}
