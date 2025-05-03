using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
7.3.Во входном файле input.txt содержится текст. В файл output.txt выведите отредактированный текст, 
в котором верно расставлены пробелы перед и после знаков препинания (точка, запятая, тире, точка с запятой, 
двоеточие, многоточие, круглые, квадратные и фигурные скобки): перед каждым знаком препинания (кроме тире и 
открывающихся скобок) пробел отсутствует, а после любого знака препинания (кроме открывающихся скобок) стоит 
ровно один пробел; в многоточии между точками пробелы отсутствуют. Первое слово в предложении должно начинаться 
с заглавной буквы, между словами – один пробел.
*/


namespace Task_3
{
    internal class Program
    {
        static void Main()
        {
            string inputFilePath = "C:\\input.txt";
            string outputFilePath = "C:\\output.txt";

            string text = File.ReadAllText(inputFilePath);

            string editedText = EditText(text);

            File.WriteAllText(outputFilePath, editedText);
        }

        static string EditText(string text)
        {
            // Добавляем пробелы перед и после знаков препинания
            string editedText = Regex.Replace(text, @"([^a-zA-Z0-9а-яА-ЯёЁ-])(?![\s-])", "$1 ");

            // Удаление лишних пробелов
            editedText = Regex.Replace(editedText, @"\s+", " ");

            // Заглавная буква в начале предложения
            editedText = Regex.Replace(editedText, @"(^\s*\w|[\.\?\!]\s*\w)", m => m.Value.ToUpper());

            // Удаление пробелов в многоточии
            editedText = Regex.Replace(editedText, @"\.\s*\.\s*\.", "...");

            return editedText;
        }
    }
}