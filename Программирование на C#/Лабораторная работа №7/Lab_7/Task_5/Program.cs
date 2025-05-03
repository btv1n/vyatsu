using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
7.5.Напишите программу, выводящую на экран иерархический список всех подкаталогов и файлов, 
вложенных в каталог, заданный пользователем.
*/

namespace Task_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Путь к каталогу
            string directoryPath = "C:\\Task_5";

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            if (!directory.Exists) // проверка, существует ли указанный каталог
            {
                Console.WriteLine("Указанный каталог не существует.");
            }
            else
            {
                PrintDirectoryContents(directory, "");  // пустая строка, используется в качетсве отступа
            }

            Console.ReadLine();
        }

        static void PrintDirectoryContents(FileSystemInfo obj, string indent)
        {
            Console.WriteLine(indent + obj.Name); // вывод на консоль имени объекта с отступом

            if (obj is DirectoryInfo) // является ли объект каталогом
            {
                DirectoryInfo directory = (DirectoryInfo)obj; // приведение объекта к типу DirectoryInfo

                FileSystemInfo[] objects = directory.GetFileSystemInfos(); // массив объектов из каталога

                foreach (FileSystemInfo object_ in objects) // перебор объектов внутри каталога
                {
                    PrintDirectoryContents(object_, indent + "    "); // рекурсивный вызов - 4 отступа
                }
            }
        }
    }
}