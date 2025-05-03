using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
7.6.Напишите программу, которая для заданного каталога создает подкаталоги, соответствующие дате создания каждого 
отдельного файла, и перемещает каждый файл в соответствующий дате каталог.
*/

namespace Task_6
{
    internal class Program
    {
        static void Main()
        {
            string directoryPath = "C:\\for_task6";
            string dateDirectory;
            string destinationFile;

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            
            if (!directory.Exists) // проверка существования указанного каталога
            {
                Console.WriteLine("Указанный каталог не существует.");
            }
            else
            {
                foreach (FileInfo file in directory.GetFiles()) // перебор всех файлов в указанном каталоге
                {
                    // Задание пути каталога с датой создания файла
                    dateDirectory = Path.Combine(directoryPath, file.CreationTime.ToString("dd-MM-yyyy"));

                    // Создание директории с указанным путем
                    Directory.CreateDirectory(dateDirectory);

                    // Путь, который указывает, куда нужно перенести файл
                    destinationFile = Path.Combine(dateDirectory, file.Name);

                    // Перемещение файла в указанную директорию
                    file.MoveTo(destinationFile);
                }

                Console.WriteLine("Файлы успешно перенесены.");
            }
            Console.ReadLine();
        }
    }
}
