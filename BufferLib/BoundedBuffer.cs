using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BufferLib
{
    public class BoundedBuffer<T>
    {
        // instans vars
        private readonly Queue<T> _q;
        private readonly Semaphore _empty;
        private readonly Semaphore _full;
        private readonly object _lock;

        public BoundedBuffer(int size)
        {
            _q = new Queue<T>(size);
            _empty = new Semaphore(0, size);
            _full = new Semaphore(size, size);
            _lock = new object();
        }



        public void Insert(T item)
        {
            _full.WaitOne();

            lock (_lock)
            {
                _q.Enqueue(item);
            }

            _empty.Release();
        }



        public T Take()
        {
            _empty.WaitOne();
            T result;
            lock (_lock)
            {
                result = _q.Dequeue();
            }
            _full.Release();

            return result;
        }

    }
}
