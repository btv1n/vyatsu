using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
7.2.	Во входном файле input.txt содержится текст. В файл output.txt выведите текст в зашифрованном виде: 
каждая буква исходного текста заменяется на следующую за ней в алфавите (буква ‘z’ заменяется на ‘а’).
*/

namespace Task_2
{
    internal class Program
    {
        static void Main()
        {
            // Задаем путь к входному файлу
            string inputFile = "C:\\input.txt";

            // Задаем пути к выходным файлам
            string outputFile = "C:\\output.txt";

            try
            {
                // Чтение файла
                using (StreamReader reader = new StreamReader(inputFile))
                {
                    string inputText = reader.ReadToEnd();

                    // Зашифровка текста
                    string encryptedText = EncryptText(inputText);

                    // Запись зашифрованного текста в файл
                    using (StreamWriter writer = new StreamWriter(outputFile))
                    {
                        writer.Write(encryptedText);
                    }
                }

                Console.WriteLine("Текст успешно зашифрован и записан в файл output.txt.");
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка чтения файла: " + e.Message);
            }


            Console.ReadLine();
        }

        static string EncryptText(string inputText)
        {
            char[] inputChars = inputText.ToCharArray(); // преобразуем входной текст в массив символов для работы с каждым симоволом отдельно

            for (int i = 0; i < inputChars.Length; i++) // перебираем весь массив символов
            {
                if (char.IsLetter(inputChars[i])) // проверяем является ли текущий символ буквой
                {
                    // Замена каждой буквы на следующую в алфавите
                    if (inputChars[i] == 'z')
                    {
                        inputChars[i] = 'a';
                    }
                    else if (inputChars[i] == 'Z')
                    {
                        inputChars[i] = 'A';
                    }
                    else
                    {
                        inputChars[i]++; // следущая буква в алфавите
                    }
                }
            }

            return new string(inputChars); // возвращаем полученный массив символов в виде строки
        }
    }
}
