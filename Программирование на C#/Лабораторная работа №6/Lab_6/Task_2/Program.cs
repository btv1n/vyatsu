using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
6.2. В консольном приложении за минимальное число действий реализуйте указанную 
задачу, используя одномерный массив 15 целых чисел. Массив инициализируется 
константами. Даны два неубывающих массива. Найдите их «пересечение», т. е. 
массив, содержащий общие элементы исходных массивов, причем кратность каждого 
элемента в итоговом массиве равняется минимуму из его кратностей в исходных массивах.
*/

namespace Task_2
{
    internal class Program
    {
        static void FillArray(int[] array)
        {
            bool flag = true;
            for (int i = 0; i < array.Length; i++) // заполнение массива, проверка ввода
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

        static int[] FindIntersection(int[] arr1, int[] arr2) // находит пересечение двух массивов с учетом кратности элеметов
        {
            List<int> result = new List<int>();

            int i = 0, j = 0;
            while (i < arr1.Length && j < arr2.Length) // пока не дошли до конца обоих массивов
            { 
                if (arr1[i] < arr2[j]) { i++; } // массив 1 содержит уникальный элемент
                else if (arr1[i] > arr2[j]) { j++; } // массив 2 содержит уникальный элемент
                else
                {
                    int minCount = Math.Min(CountElements(arr1, i), CountElements(arr2, j)); // находит минимальную встречаемость элемента в двух массивах
                    for (int k = 0; k < minCount; k++)
                    {
                        result.Add(arr1[i]); // добавляем общий символ (к) раз т.е. столько, сколько раз он встречается в обоих массивах
                    }

                    // увеличиваем для того, чтобы не проверять уже обработанные элементы
                    i += minCount;
                    j += minCount;
                }
            }

            return result.ToArray();
        }

        static int CountElements(int[] arr, int index) // находит кратность элементов (кол-во уникальных элементов в массиве)
        {
            int count = 0;
            int num = arr[index]; // элемент

            for (int i = index; i < arr.Length; i++) // от индекса проверяемого элемента до конца массива
            {
                if (arr[i] == num) // находим кол-во повторяющихся чисел
                    count++;
            }

            return count;
        }

        static void PrintArray(int[] array)
        {
            foreach (int num in array)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] array1 = { 1, 2, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            int[] array2 = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };

            int[] intersection = FindIntersection(array1, array2); // находит пересечения массивов

            Console.WriteLine("Пересечение массивов:");
            PrintArray(intersection);

            Console.ReadLine();
        }
    }
}
