using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a thread
            Thread thread = new Thread(MySampleThread);
            thread.Name = "my worker thread";
            thread.Start();

            Thread.CurrentThread.Name = "Main IC Thread";
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Z" + "_" + i);
            }

            Console.ReadLine();
        }

        private static void MySampleThread()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("A" + "_" + i);
            }
        }
    }
}
