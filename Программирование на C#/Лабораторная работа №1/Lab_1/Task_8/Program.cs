using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.8.	Выведите на экран все возможные трехзначные числа, полученные путем 
             * перестановки цифр трехзначного числа, вводимого с клавиатуры.*/
            Console.Write("Введите трехзначное число: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Все возможные числа, полученные перестановкой цифр: ");

            if ((100 <= number) && (number < 1000))
            {
                int n1 = (number / 100); //первое число
                int n2 = (number / 10) % 10;
                int n3 = number % 10;

                if ((n1 == 0 && n2 == 0) || (n2 == 0 && n3 == 0) || (n3 == 0 && n1 == 0))
                {
                    Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                }
                else if (n2 == 0 || n3 == 0)
                {
                    if ((n2 == 0) && (n1 != n3))
                    {
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                        Console.WriteLine(n3 * 100 + n1 * 10 + n2);
                        Console.WriteLine(n3 * 100 + n2 * 10 + n1);
                    }
                    if ((n3 == 0) && (n1 != n2))
                    {
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                        Console.WriteLine(n2 * 100 + n1 * 10 + n3);
                        Console.WriteLine(n2 * 100 + n3 * 10 + n1);
                    }
                    if ((n2 == 0) && (n1 == n3))
                    {
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                    }
                    if ((n3 == 0) && (n1 == n2))
                    {
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                    }

                }
                else if ((n1 != n2) && (n2 != n3) && (n3 != n1)) // все три цифры не одинаковы
                {
                    Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                    Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                    Console.WriteLine(n2 * 100 + n1 * 10 + n3);
                    Console.WriteLine(n2 * 100 + n3 * 10 + n1);
                    Console.WriteLine(n3 * 100 + n2 * 10 + n1);
                    Console.WriteLine(n3 * 100 + n1 * 10 + n2);
                }
                else if ((n1 == n2) && (n2 == n3)) // все три цифры одинаковы
                {
                    Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                }
                else // две одинаковые цифры
                {
                    if (n1 == n2)
                    {
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                        Console.WriteLine(n3 * 100 + n1 * 10 + n2);
                    }
                    if (n2 == n3)
                    {
                        Console.WriteLine(n2 * 100 + n3 * 10 + n1);
                        Console.WriteLine(n2 * 100 + n1 * 10 + n3);
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                    }
                    if (n1 == n3)
                    {
                        Console.WriteLine(n1 * 100 + n3 * 10 + n2);
                        Console.WriteLine(n1 * 100 + n2 * 10 + n3);
                        Console.WriteLine(n2 * 100 + n1 * 10 + n3);
                    }
                }
            }
            else
            {
                Console.WriteLine("Введенное число не является трехзначным");
            }
        }
    }
}
