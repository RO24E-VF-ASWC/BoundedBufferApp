using BoundedBufferApp.model;
using BufferLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundedBufferApp
{
    public class Experiment
    {
        private const int NO_OF_PRODUCER = 4;
        private const int NO_OF_CONSUMER = 2;


        private readonly Random rnd = new Random(DateTime.Now.Millisecond);


        //public void Producer(Queue<Item> items, int no)
        public void Producer(BoundedBuffer<Item> items, int no)

        {
            int myNo = no;
            if (myNo%10 == 0)
            Console.WriteLine($"producer {myNo} started");
            while (true)
            {
                Thread.Sleep(rnd.Next(20) + 10); // 10-30 msec wait
                Item item = new Item(rnd.Next(12000));
                item.ProducerNo = myNo;
                items.Insert(item);
            }
        }

        //public void Consumer(Queue<Item> items)
        public void Consumer(BoundedBuffer<Item> items)
        {
            Console.WriteLine("consumer started");
            while (true)
            {
                Item item = items.Take();
                Console.WriteLine(item);
                Thread.Sleep(rnd.Next(500)); // 0-500 msec wait

            }
        }

        public void Start()
        {
            //Queue<Item> q = new Queue<Item>();
            BoundedBuffer<Item> q = new BoundedBuffer<Item>(10);

            for (int i = 0; i < NO_OF_PRODUCER; i++)
            {
                int no = i + 1;
                Task.Run(() =>  Producer(q, no));
            }

            Thread.Sleep(100);
            for (int i = 0; i < NO_OF_CONSUMER; i++)
            {
                Task.Run(() => Consumer(q));
            }
        }
    }
}
