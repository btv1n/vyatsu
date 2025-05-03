using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите стороны треугольника: ");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());
            int c = Convert.ToInt32(Console.ReadLine());


            /* Согласно неравенству треугольника для длин его сторон: любая сторона 
             * треугольника меньше, чем сумма двух других сторон треугольника. Для 
             * проверки истинности данного неравенства достаточно взять самую большую 
             * сторону, если она меньше суммы двух других сторон, то такой треугольник 
             * существует.*/

            if ((a <= b + c) && (b <= a + c) && (c <= a + b))
            {
                double p = (a + b + c) / 2; // полупериметр
                double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c)); // формула Герона

                Console.WriteLine("Площадь треугольника равна: {0, 2:F}", area);
            }
            else
            {
                Console.WriteLine("Треугольник не существует");
            }
        }
    }
}
