using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DistanceUpdateTool
{
    public class MutipleThreadResetEvent : IDisposable
    {
        private readonly ManualResetEvent done;
        private long current;
        public MutipleThreadResetEvent(int total)
        {
            current = total;
            done = new ManualResetEvent(false);
        }
        public void SetOne()
        {
            if (Interlocked.Decrement(ref current) == 0)
            {
                done.Set();
            }
        }
        public void WaitAll()
        {
            done.WaitOne();
        }
        public void StopAll()
        {
                //Thread.CurrentThread.Abort();
                ((IDisposable)done).Dispose();
        }
        public void Dispose()
        {
            ((IDisposable)done).Dispose();
        }
    }
}
