using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Examples
{
    public enum Transport
    {
        Car,
        Bus,
        Subway,
        Bike,
        Walk
    }
    public static class TransportExtension
    {
        public static char GetTransportChar(this Transport tra)
        {
            switch (tra)
            {
                case Transport.Car:
                    return 'C';
                    break;
                case Transport.Bus:
                    return 'U';
                    break;
                case Transport.Subway:
                    return 'S';
                    break;
                case Transport.Bike:
                    return 'B';
                    break;
                case Transport.Walk:
                    return 'W';
                    break;
                default:
                    throw new Exception("Unknwn Transport");
                    break;
            }
        }

        public static ConsoleColor GetTransportColor(this Transport tra)
        {
            switch (tra)
            {
                case Transport.Car:
                    return ConsoleColor.Red;
                    break;
                case Transport.Bus:
                    return ConsoleColor.DarkGreen;
                    break;
                case Transport.Subway:
                    return ConsoleColor.DarkMagenta;
                    break;
                case Transport.Bike:
                    return ConsoleColor.Blue;
                    break;
                case Transport.Walk:
                    return ConsoleColor.Yellow;
                    break;
                default:
                    throw new Exception("Unkown Transport");
                    break;
            }
        }
    

    }
}
