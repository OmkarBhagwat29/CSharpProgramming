using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvetsLambdas.EventsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass myC = new MyClass();
            myC.ValueChanged += MyC_ValueChanged;
            myC.ValueChanged += delegate (string s)
            {
                Console.WriteLine($"Adding extension to {s}, final value {s + "_something random"}");
            };

            myC.objectChnaged += MyC_objectChnaged;

            while (true)
            {
                Console.WriteLine("Enter value: ");
                string val = Console.ReadLine();
                if (val == "exit")
                    return;

                myC.Val = val;
            }
        }

        private static void MyC_objectChnaged(object sender, MyEventArgs e)
        {
            Console.WriteLine($"{sender.GetType()} is type of the object and value is changed of property {e.propChanged}");
        }

        private static void MyC_ValueChanged(string s)
        {
            Console.WriteLine($"value is changed to {s}.");
        }
    }
}
