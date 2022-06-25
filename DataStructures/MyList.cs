using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class MyList<T> : CollectionBase
    {
        T[] data;
        int size = 0;
        int capacity;

        public T this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        public int Size { get
            { return size; } }

        public bool IsEmpty { get { return size == 0; } }
    }
}
