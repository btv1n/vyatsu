using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
7.7.Напишите программу, которая удаляет все подкаталоги, вложенные в заданный.
*/

/*
                try
                {
                    
                }
                catch (IOException)
                {
                    Console.WriteLine("Каталог не пуст");
                }
*/


namespace Task_7
{
    internal class Program
    {
        static void Main()
        {
            // Заданный путь к каталогу
            string path = "C:\\for_task";

            DirectoryInfo directory = new DirectoryInfo(path);

            // Проверка существования каталога
            if (directory.Exists)
            { 
                DeleteSubdirectories(directory); // удаление всех подкаталогов
                Console.WriteLine("Все подкаталоги удалены.");
            }
            else
            {
                Console.WriteLine("Указанный каталог не существует.");
            }

            Console.ReadLine();
        }

        static void DeleteSubdirectories(DirectoryInfo directory)
        {
            string[] files;

            foreach (var subdirectory in directory.GetDirectories()) // удаление всех подкаталогов
            {
                DeleteSubdirectories(subdirectory); // рекурсивный вызов для удаления подкаталогов внутри

                files = Directory.GetFiles("C:\\for_task\\" + subdirectory.Name); // путь к файлам в подкаталоге

                foreach (string file in files) // перебираем файлы в подкаталоге и удаляем их
                {
                    File.Delete(file);
                }

                subdirectory.Delete(); // удаление текущего подкаталога
            }
        }
    }
}
