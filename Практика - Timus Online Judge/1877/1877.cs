using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int codeLock_1, codeLock_2;

            codeLock_1 = Convert.ToInt32(Console.ReadLine());
            codeLock_2 = Convert.ToInt32(Console.ReadLine());

            if (codeLock_1 % 2 == 0 || codeLock_2 % 2 != 0)
                Console.WriteLine("yes");
            else
                Console.WriteLine("no");

            // string input = Console.ReadLine();
        }
    }
}
