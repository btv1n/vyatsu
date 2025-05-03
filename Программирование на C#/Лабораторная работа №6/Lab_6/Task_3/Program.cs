using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
6.3.	В консольном приложении реализуйте указанную задачу, используя одномерный массив целых чисел. Размерность 
исходного массива считывается с клавиатуры с проверкой корректности ввода, элементы являются случайными числами из 
диапазона [–15…30]. Операции удаления и вставки элементов осуществить путем сдвига соответствующих элементов массива. 
Решение задачи путем вывода нескольких фрагментов исходного массива считается неверным. Программа должна содержать метод 
по выводу первых n элементов массива. Формулировка задачи: вставьте элемент, равный минимальному элементу всего массива, 
перед всеми элементами, равными максимальному элементу. Распечатайте получившийся массив. После этого удалите все 
повторяющиеся элементы, оставив только их первые вхождения, то есть получите массив различных элементов.
*/

namespace Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива: ");
            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size <= 0) // обработка ввода
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                Console.Write("Введите размерность массива: ");

            }


            int[] array = new int[size];
            
            Random random = new Random();
            for (int i = 0; i < size; i++) // заполнение массива случайными числами
            {
                array[i] = random.Next(30, 31);
            }
            
            //FillArray(array);



            Console.WriteLine();
            Console.WriteLine("Исходный массив:"); 
            PrintArray(array); // вывод исходного массива


            int maxValue = array[0], minValue = array[0];
            foreach (int element in array) // нахождение минимального и максимального элемента массива
            {
                if (element > maxValue)
                    maxValue = element;
                if (element < minValue)
                    minValue = element;
            }
            Console.WriteLine(); // вывод максимального и минимального элементов
            Console.WriteLine("Максимальный элемент: " + maxValue);
            Console.WriteLine("Минимальный элемент: " + minValue);
            Console.WriteLine();


            int counterMax = 0;
            foreach (int element in array) // считаем количество масксимальных элементов
            {
                if (element == maxValue)
                    counterMax++;
            }


            // Создает массив и добавляет в него минимальный элемент
            // добавить размер изменяемый
            int[] newArray = new int[size + counterMax]; // увеличиваем размер массива на кол-во максимальных элементов
                                                         //newArray[0] = minValue;
                                                         //newArray[0] = array[0];
            int newIndex = 0;
            foreach (int element in array) // перебираем элементы в исходном массиве
            {
                if (element == maxValue) // если элемент равен максимальному
                    newArray[newIndex++] = minValue; // записываем перед ним минимальный, а потом сам элемент
                newArray[newIndex++] = element; // значение индекса увеличивается после выполнения операции (постфикс)
            }


            Console.WriteLine("Массив после вставки:");
            PrintArray(newArray);
            Console.WriteLine();


            int newSize = 0;
            for (int i = 0; i < newArray.Length; i++) // удаление дубликатов в массиве
            {
                bool isDuplicate = false;
                for (int j = 0; j < i; j++)
                {
                    if (newArray[j] == newArray[i])
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                    newArray[newSize++] = newArray[i];
            }
            Array.Resize(ref newArray, newSize); // изменяет размер массива на новый заданный размер
                                                 // метод возращает новый массив, а изменяет размер текущего


            Console.WriteLine("Массив после удаления повторяющихся элементов:");
            PrintArray(newArray);
            Console.WriteLine();

            Console.ReadLine();
        }


        static void PrintArray(int[] array) // функция для вывода массива
        {
            foreach (int element in array)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        static void FillArray(int[] array) // функция заполнения массива
        {
            bool flag = true;
            for (int i = 0; i < array.Length; i++)
            {
                flag = true;
                Console.Write("Введите элемент {0}: ", i + 1);
                while (flag) // проверка ввода
                {
                    try { array[i] = Convert.ToInt32(Console.ReadLine()); flag = false; }
                    catch { Console.WriteLine("Ошибка ввода."); }
                }
            }
        }
    }
}
