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
            int n = int.Parse(Console.ReadLine()); // вводим кол-во чисел

            int[] p = Array.ConvertAll(Console.ReadLine().Split(), int.Parse); // вводим сами числа

            Console.WriteLine((double)p.Sum() / n); // выводим среднее арифметическое

            //string input = Console.ReadLine();
        }
    }
}
