using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesEventsLambdas.Delegates
{
    //insert delegate here
    public delegate int BasicDelegate(int a, int b);

    public delegate double ZonePriceDelegate(double price);

    public class MyClass
    {
        public int Divide(int a, int b) => a / b;

        public double Zone1(double price)
        {
            return (25.0 / 100.0) * price;
        }

        public double Zone2(double price)
        {
            return ((25.0 / 100.0) * price) + ((12.0 / 100.0) * price);
        }

        public double Zone3(double price)
        {
            return (8.0 / 100.0) * price;
        }

        public double Zone4(double price)
        {
            return ((25.0 / 100.0) * price) + ((8.0 / 100.0) * price);
        }
    }

    class Program
    {
 
        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;
        static void Main(string[] args)
        {
            #region creating delegates
            BasicDelegate f = Add;

            int resultAdd = f(5, 4);

            f = Multiply;
            int resultMultiply = f(5, 4);

            MyClass myClass = new MyClass();
            f = myClass.Divide;
            int resultDivide = f(25, 5);

            Console.WriteLine($"Addition Result {resultAdd}\nMultiplication Result {resultMultiply}\nDivision Result {resultDivide}");

            #endregion

            Console.WriteLine("**********************************************************************");

            #region creating AnonymousDelegate
            f = delegate (int arg1, int arg2)
            {
                return (arg1 + arg2);
            };

            Console.WriteLine($"The numeber is {f(7, 3)}. This number is created using Anonymus Delegate ");

            #endregion

            Console.WriteLine("**********************************************************************");

            #region Composable Delegates
            BasicDelegate b1 = Add;
            BasicDelegate b2 = Multiply;

            BasicDelegate chain = b1 + b2;

            Console.WriteLine($"b1 delegate value: {b1(5,5)}\nb2 delegate value: {b2(5,5)}\nchain delegate (b1+b2) value: {chain(5,5)}");

            chain -= b2;
            Console.WriteLine($"chain delegate after subtracting(chian -= b2) value: {chain(5, 5)}");
            #endregion

            Console.WriteLine("**********************************************************************");

            EvaluateZones();

            Console.ReadKey();
        }


        static void EvaluateZones()
        {
            bool invalid = false;
            MyClass myC = new MyClass();
            ZonePriceDelegate zDlg = myC.Zone1;
            while (true)
            {
                Console.WriteLine("Which Zone? ");
                string zoneName = Console.ReadLine();

                Console.WriteLine("Price of the product? ");
                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("invalid price enterd, enter only numbers");
                    return;

                }
                if (zoneName == "zone1")
                {
                    zDlg = myC.Zone1;
                }
                else if (zoneName == "zone2")
                {
                    zDlg = myC.Zone2;
                }
                else if (zoneName == "zone3")
                {
                    zDlg = myC.Zone3;
                }

                else if (zoneName == "zone4")
                {
                    zDlg = myC.Zone4;
                }
                else if (zoneName == "exit")
                {
                    Console.WriteLine("closing down application");
                    return;
                }
                else
                    invalid = true;

                if(!invalid)
                    Console.WriteLine($"price of the product is {price} and shipping price is {zDlg(price)} ");
            }

        }
    }
}
