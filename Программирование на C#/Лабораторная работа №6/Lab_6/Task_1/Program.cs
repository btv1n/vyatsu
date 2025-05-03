using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
(1) +
(2) +
(3) +
(4) - (массив должен быть заполнен с помощью циклов по лесенке)
(5) +
(6) - (изменить перестановку элементов, должно работать для более одного столбца с двумя отрицательными числами)
(7) -
(8) +
(9) +
(10) +
(11) +
(12) + (реализовано с использованием словаря)
*/

/*
6.1. В одномерном массиве 10 целых чисел, элементы которого считываются с клавиатуры, 
поменяйте порядок элементов массива, расположенных между первым максимальным и 
последним минимальным элементами, на обратный.
*/

namespace Task_1
{
    internal class Program
    {
        static void PrintArray(int[] array)
        {
            foreach (int element in array)
            {
                Console.Write(element);
                Console.Write(" ");
            }
        }
        static void FillArray(int[] array)
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

        // Возвращает индекс максимального элемента
        public static int FindMaxElementInArray(int[] array, int maxIndex)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex]) // Неравество строгое, чтобы найти первый максимальный элемент
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public static int FindMinElementInArray(int[] array, int minIndex)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] <= array[minIndex]) // Неравенство не сторогое, чтобы найти последний минимальный элемент
                {
                    minIndex = i;
                }
            }

            return minIndex;
        }

        static void Main(string[] args)
        {
            int maxIndex = 0, minIndex = 0;

            int[] array = new int[10] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            FillArray(array); // Считываем элементы массива с клавиатуры

            maxIndex = FindMaxElementInArray(array, maxIndex);
            minIndex = FindMinElementInArray(array, minIndex);

            Console.Write("Массив до изменения порядка элементов: ");
            PrintArray(array);
            Console.WriteLine();

            if (maxIndex < minIndex) // максимальный находится до минимального  (Max 1 2 3 Min) -> (Max 3 2 1 Min)
            {
                for (int i = maxIndex + 1, j = minIndex - 1; i < j; i++, j--)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            else if (maxIndex > minIndex) // максимальный находится после минимального
            {
                for (int i = minIndex + 1, j = maxIndex - 1 ; i < j; i++, j--)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            Console.Write("Массив после изменения порядка элементов: ");
            PrintArray(array);
            Console.WriteLine();

            Console.ReadLine(); // чтоб программа не закрывалась моментально

            //Console.WriteLine(minIndex); 9
            //Console.WriteLine(maxIndex); 0
        }
    }
}
