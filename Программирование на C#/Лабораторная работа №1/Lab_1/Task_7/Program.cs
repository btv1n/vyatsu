using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.7.	Вывести на экран номер четверти, которой принадлежит точка с 
             * координатами (x, y), или же написать, что точка является началом 
             * координат или лежит на одной из осей x или y. Минимизируйте в коде 
             * количество условий.*/

            Console.Write("Введите координату х: ");
            double x = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите координату y: ");
            double y = Convert.ToDouble(Console.ReadLine());

            if (x == 0 && y == 0)
            {
                Console.WriteLine("Точка является началом координат.");
            }
            else if (x == 0)
            {
                Console.WriteLine("Точка лежит на оси x.");
            }
            else if (y == 0)
            {
                Console.WriteLine("Точка лежит на оси y.");
            }
            else if (x > 0 && y > 0)
            {
                Console.WriteLine("Точка принадлежит первой четверти.");
            }
            else if (x < 0 && y > 0)
            {
                Console.WriteLine("Точка принадлежит второй четверти.");
            }
            else if (x < 0 && y < 0)
            {
                Console.WriteLine("Точка принадлежит третьей четверти.");
            }
            else
            {
                Console.WriteLine("Точка принадлежит четвертой четверти.");
            }
        }
    }
}
