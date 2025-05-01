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
            // Создание очереди строк
            Queue<string> result = new Queue<string>();

            // Кол-во букв
            int n = int.Parse(Console.ReadLine());

            // Добавляем строки в очередь
            result.Enqueue("a");
            result.Enqueue("b");
            result.Enqueue("c");

            // Создание массива символов
            char[] abc = { 'a', 'b', 'c' };

            for (int i = 1; i < n; i++)
            {
                while (true)
                {
                    // Получение ссылки на первый элемент очереди
                    string s = result.Peek();

                    // Если размер строки больше i выходим из цикла
                    if (s.Length > i)
                        break;

                    // Получение последнего символа строки
                    char last = s[s.Length - 1];

                    if (s.Length > 1)
                    {
                        // Получение предпоследнего символа строки
                        char lbo = s[s.Length - 2];

                        for (int k = 0; k < 3; k++)
                        {
                            // Получение k-ого символа из массива abc
                            char c = abc[k];

                            // Если последний и предпоследний символы не равны символу "с" добавляем новую строку в очередь
                            if (last != c && lbo != c)
                                result.Enqueue(s + c);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            // Получение k-ого символа из массива abc
                            char c = abc[k];

                            // Если последний символ не равен символу "c" добавляем новую строку в очередь
                            if (last != c)
                                result.Enqueue(s + c);
                        }
                    }
                    // Удаление первого элемента из очереди
                    result.Dequeue();
                }
                // Если размер очереди умноженный на (i+1) больше 100000 
                if ((i + 1) * result.Count > 100000)
                {
                    Console.WriteLine("TOO LONG");
                    return;
                }
            }

            // Пока очередь не пуста, выводим первый элемент очереди и удаляем его
            while (result.Count > 0)
            {
                Console.WriteLine(result.Peek());
                result.Dequeue();
            }

            //string input = Console.ReadLine();
        }
    }
}
