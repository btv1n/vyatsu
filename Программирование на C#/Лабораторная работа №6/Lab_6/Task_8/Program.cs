using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
6.8. Определите, какой из двух символов, введенных с клавиатуры, встречается в строке чаще. 
Если они встречаются одинаковое число раз, то определите, сколько различных символов встречается 
в строке.
*/

namespace Task_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            Console.Write("Введите первый символ: ");
            char firstChar = Console.ReadKey().KeyChar;
            Console.WriteLine();

            Console.Write("Введите второй символ: ");
            char secondChar = Console.ReadKey().KeyChar;
            Console.WriteLine();


            int firstCharCount = 0;
            int secondCharCount = 0;


            foreach (char c in input) // подсчет кол-ва вхождений символов в строку
            {
                if (c == firstChar)
                {
                    firstCharCount++;
                }
                else if (c == secondChar)
                {
                    secondCharCount++;
                }
            }


            if (firstCharCount > secondCharCount)
            {
                Console.WriteLine("Первый символ встречается чаще.");
            }
            else if (secondCharCount > firstCharCount)
            {
                Console.WriteLine("Второй символ встречается чаще.");
            }
            else
            {
                Console.WriteLine("Оба символа встречаются одинаковое число раз.");
                int differentCharsCount = 0;
                foreach (char c in input)
                {
                    if (c != firstChar && c != secondChar)
                    {
                        differentCharsCount++;
                    }
                }
                // Различные символы - это те, которые не являются ни firstChar, ни secondChar. Кол-во таких символов
                Console.WriteLine($"Число различных символов в строке: {differentCharsCount}");

                HashSet<char> uniqueChars = new HashSet<char>(input); // класс с методом подсчета уникальных символов
                int count = uniqueChars.Count;
                Console.WriteLine($"Число уникальных символов в строке: {count}");
            }


            Console.ReadLine();
        }
    }
}

