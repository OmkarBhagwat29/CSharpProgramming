using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class OneDArray <T>
    {
        T[] ar;

        public OneDArray(int size)
        {
            ar = new T[size];
        }

        public void Create(int size)
        {
            ar = new T[size];
        }

        public void Print()
        {
            for (int i = 0; i < ar.Length; i++)
            {
                Console.WriteLine(ar[i]);
            }
        }

        public void Add<T>(T item)
        {
          
        }
    }
}
