using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.1.	Напишите программу, запрашивающую у пользователя длину, ширину и высоту помещения 
             * (в метрах), а также стоимость одного квадратного метра обоев и определяющую общую стоимость обоев, 
             * необходимых для оклейки всех стен и потолка помещения. Все значения могут быть не целыми.*/

            double length, width, height, price, result;

            Console.Write("Введите длину помещения в метрах: ");
            length = Math.Abs(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Введите ширину помещения в метрах: ");
            width = Math.Abs(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Введите высоту помещения в метрах: ");
            height = Math.Abs(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Введите стоимость одного квадратного метра обоев: ");
            price = Math.Abs(Convert.ToDouble(Console.ReadLine()));

            // Формула для рассчета стоимости квадратных метров обоев
            result = (2 * (width + length) * height) * price;

            Console.WriteLine("Определяющая стоимость обоев: " + result);
        }
    }
}
