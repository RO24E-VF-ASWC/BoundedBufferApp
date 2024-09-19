using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BufferLib
{
    public class BoundedBufferCQ<T>
    {
        // instans vars
        private readonly ConcurrentQueue<T> _q;
        private readonly Semaphore _empty;
        private readonly Semaphore _full;
        
        public BoundedBufferCQ(int size)
        {
            _q = new ConcurrentQueue<T>();
            _empty = new Semaphore(0, size);
            _full = new Semaphore(size, size);
        }



        public void Insert(T item)
        {
            _full.WaitOne();
            _q.Enqueue(item);
            _empty.Release();
        }



        public T Take()
        {
            _empty.WaitOne();
            T result;
            _q.TryDequeue(out result);
            _full.Release();

            return result;
        }
    }
}
