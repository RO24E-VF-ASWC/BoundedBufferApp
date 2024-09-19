using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundedBufferApp.model
{
    public class Item
    {
        private static int nextId = 1;



        public int Id { get; private set; }
        public int Value { get; set; }

        public int ProducerNo { get; set; }

        public Item(int value)
        {
            Id = nextId++;
            Value = value;
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Value)}={Value.ToString()}, {nameof(ProducerNo)}={ProducerNo.ToString()}}}";
        }
    }
}
