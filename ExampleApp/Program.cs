using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var it = DateTime.MinValue.AddHours(23);
            Console.Write(it.ToShortDateString() + " : " + it.ToShortTimeString());
            Console.ReadLine();
        }
    }
}
