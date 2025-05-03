using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
7.4.Создайте программным образом файл input.txt и заполните его 50 целыми числами, полученными с помощью 
генератора случайных чисел из диапазона [–150…250]. Напишите программу, выводящую на экран и в файл output.txt 
числа из файла input.txt, исключив повторные вхождения.
*/

namespace Task_4
{
    internal class Program
    {
        static void Main()
        {
            // Задаем путь к входному файлу
            string inputFile = "C:\\input.txt";

            // Задаем пути к выходным файлам
            string outputFile = "C:\\output.txt";


            /////////// Символьный поток ///////////

            // Генерируем случайные числа и записываем их в файл input.txt
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(inputFile))
            {
                for (int i = 0; i < 50; i++)
                {
                    int number = random.Next(-150, 251);
                    writer.WriteLine(number);
                }
            }

            // Читаем числа из файла input.txt, исключая повторные вхождения
            HashSet<int> distinctNumbers = new HashSet<int>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int number = int.Parse(line);
                    distinctNumbers.Add(number);
                }
            }

            // Выводим числа на экран
            Console.WriteLine("Уникальные числа:");
            foreach (int number in distinctNumbers)
            {
                Console.WriteLine(number);
            }

            // Записываем числа в файл output.txt
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (int number in distinctNumbers)
                {
                    writer.WriteLine(number);
                }
            }

            /////////// Символьный поток ///////////


            /////////// Байтовый поток ///////////

            byte[] dataToWrite = new byte[50];

            // Генерируем случайные числа
            for (int i = 0; i < 50; i++)
            {
                dataToWrite[i] = (byte)random.Next(-150, 251);
            }

            // Читаем числа из файла input.txt, исключая повторные вхождения
            HashSet<int> distinctNumbersByte = new HashSet<int>();
            using (StreamReader reader = new StreamReader("C:\\input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int number = int.Parse(line);
                    distinctNumbers.Add(number);
                }
            }

            // Запись байтовых данных в файл
            using (FileStream fileStream = new FileStream("C:\\output2.txt", FileMode.Create)) // "file.bin"
            {
                fileStream.Write(dataToWrite, 0, dataToWrite.Length);
            }

            //// Чтение байтовых данных из файла
            //byte[] dataToRead = new byte[dataToWrite.Length];
            //using (FileStream fileStream = new FileStream("file.bin", FileMode.Open))
            //{
            //    fileStream.Read(dataToRead, 0, dataToRead.Length);
            //}

            //foreach (byte data in dataToRead)
            //{
            //    Console.WriteLine(data);
            //}

            /////////// Байтовый поток ///////////


            /////////// Двоичный  поток ///////////

            // Запись двоичных данных в файл
            BinaryWriter binaryWriter = new BinaryWriter(File.Open("file.bin", FileMode.Create));
            int intValue = 10;
            float floatValue = 3.14f;
            binaryWriter.Write(intValue);
            binaryWriter.Write(floatValue);
            binaryWriter.Close();

            // Чтение двоичных данных из файла
            BinaryReader binaryReader = new BinaryReader(File.Open("file.bin", FileMode.Open));
            int readIntValue = binaryReader.ReadInt32();
            float readFloatValue = binaryReader.ReadSingle();
            binaryReader.Close();

            Console.WriteLine(readIntValue);
            Console.WriteLine(readFloatValue);

            /////////// Двоичный  поток ///////////


            Console.ReadLine();
        }
    }
}