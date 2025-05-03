using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.4.	Напишите программу, которая запрашивает с клавиатуры пять целых 
             * чисел и выводит на экран таблицу, в которой для каждого из этих чисел 
             * выводится само число, его модуль, квадратный корень и значение в пятой 
             * степени. При выводе нецелых значений выводите два знака после запятой. 
             * Столбики таблицы должны быть подписаны.*/

            Console.WriteLine("Введите 5 целых чисел: ");
            int n1 = Convert.ToInt32(Console.ReadLine());
            int n2 = Convert.ToInt32(Console.ReadLine());
            int n3 = Convert.ToInt32(Console.ReadLine());
            int n4 = Convert.ToInt32(Console.ReadLine());
            int n5 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"{"Само число",20} {"Модуль",20} {"Квадратный корень",20} {"Значение в 5-ой степени",20}");

            int module = Math.Abs(n1);
            double root = Math.Round(Math.Sqrt(n1), 2);
            double fivePow = Math.Pow(n1, 5);
            Console.WriteLine($"{n1,20} {module,20} {root,20} {fivePow,20}");

            module = Math.Abs(n2);
            root = Math.Round(Math.Sqrt(n2), 2);
            fivePow = Math.Pow(n2, 5);
            Console.WriteLine($"{n2,20} {module,20} {root,20} {fivePow,20}");

            module = Math.Abs(n3);
            root = Math.Round(Math.Sqrt(n3), 2);
            fivePow = Math.Pow(n3, 5);
            Console.WriteLine($"{n3,20} {module,20} {root,20} {fivePow,20}");

            module = Math.Abs(n4);
            root = Math.Round(Math.Sqrt(n4), 2);
            fivePow = Math.Pow(n4, 5);
            Console.WriteLine($"{n4,20} {module,20} {root,20} {fivePow,20}");

            module = Math.Abs(n5);
            root = Math.Round(Math.Sqrt(n5), 2);
            fivePow = Math.Pow(n5, 5);
            Console.WriteLine($"{n5,20} {module,20} {root,20} {fivePow,20}");


            //Console.WriteLine(n1);
            //Console.WriteLine(module);
            //Console.WriteLine("{0, 14:F}", root);
            //Console.WriteLine("{0, 14:F}", fivePow);
            //Console.WriteLine(" F Format:{0,14:F}\t{0,10:F1}\t{0,14:F4}", 1234.567);
            //Console.WriteLine($"Имя: {name,-5} Возраст: {age}"); // пробелы после
            //Console.WriteLine($"Имя: {name,5} Возраст: {age}"); // пробелы до
        }
    }
}
