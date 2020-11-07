using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResources
{
    class Program
    {
        private static bool isCompleted;
        static readonly object LockCompleted = new object();
        static void Main(string[] args)
        {
            Thread thread = new Thread(HellowWorld);
            thread.Start();

            HellowWorld();

            Console.ReadLine();
        }

        private static void HellowWorld()
        {
            lock (LockCompleted)
            {
                if (!isCompleted)
                {
                    Console.WriteLine("Hello World should be printed only once.");
                    isCompleted = true;
                }
            }


        }
    }
}
