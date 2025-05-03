using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите раздельно значения x, y, z: ");
            int x = Convert.ToInt32(Console.ReadLine());
            int y = Convert.ToInt32(Console.ReadLine());
            int z = Convert.ToInt32(Console.ReadLine());

            // abs() модуль - Sqrt() корень - Pow() степень

            double result = ((Math.Pow(x, 12) + Math.Sqrt(Math.Pow(z, 6) - 5 * x * y)) / Math.Abs(-7 * Math.Pow(x, 2) * Math.Pow(y, 8) + z)) + 7;
            //Console.WriteLine("E={0:0.000000}", x); // ровно шесть знаков
            //Console.WriteLine("Результат: " + result);
            Console.Write("Результат: ");
            Console.WriteLine("{0:0.00000}", result);
        }
    }
}
