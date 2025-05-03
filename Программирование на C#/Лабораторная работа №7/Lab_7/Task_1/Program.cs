using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // подключено
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;

/*
(1) +
(2) +
(3) -
(4) -
(5) +
(6) +
(7) +
(8) -
*/

/*
7.1.Во входном файле input.txt в столбец записаны целые числа. В файл out1.txt запишите все четные числа, 
а в файл out2.txt – все нечетные.
*/

/*
// Использовать: обработчик ошибок; StreamReader R; StreamWriter W;
string inputFilePath = "C:\\input.txt";
string outputFilePath = "C:\\output.txt";

static void PrintArray(int[] array)
{
    foreach (int element in array)
    {
        Console.Write(element);
        Console.Write(" ");
    }
}

static void PrintArray(string[] array)
{
    foreach (string element in array)
    {
        Console.Write(element);
        Console.Write(" ");
    }
}
*/

namespace Task_1
{
    internal class Programl
    {
        static void Main()
        {
            // Задаем путь к входному файлу
            string inputFile = "C:\\input.txt";

            // Задаем пути к выходным файлам
            string outputFile1 = "C:\\out1.txt";
            string outputFile2 = "C:\\out2.txt";

            // Попытка чтение данных из файла
            try
            {
                // Создаем объект для чтения из входного файла
                using (StreamReader reader = new StreamReader(inputFile))
                {
                    // Создаем объекты для записи в выходные файлы
                    using (StreamWriter writer1 = new StreamWriter(outputFile1, false)) // полная перезапись файла (false)
                    using (StreamWriter writer2 = new StreamWriter(outputFile2, false))
                    {
                        // Считываем строки из входного файла
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            int number;
                            // Пытаемся преобразовать строку в число (если не преобразовывается значит это не цифра, не число)
                            if (int.TryParse(line, out number))
                            {
                                // Если число четное, записываем его в первый выходной файл
                                if (number % 2 == 0)
                                {
                                    writer1.WriteLine(number);
                                }
                                // Если число нечетное, записываем его во второй выходной файл
                                else
                                {
                                    writer2.WriteLine(number);
                                }
                            }
                        }
                    }
                }

                // Выводим сообщение об успешной записи чисел в файлы
                Console.WriteLine("Числа успешно записаны в файлы.");
            }
            // Обрабатываем исключения
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка: входной файл не найден.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка: возникла проблема при чтении файла.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

            // Ждем ввода пользователя перед закрытием программы
            Console.ReadLine();
        }
    }
}
