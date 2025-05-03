using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
6.5. Определите, является ли массив логическим квадратом (суммы по всем горизонталям, 
вертикалям и двум диагоналям равны). Если не является, то отсортируйте столбцы массива по 
неубыванию минимальных элементов в них (можно сформировать промежуточный одномерный массив 
минимумов).
*/

// public void Fill2DArray(int[,] array, int size) {}

namespace Task_5
{
    internal class Program
    {
        static void Print2DArray(int[,] array, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(array[i, j].ToString().PadRight(4) + " ");
                }
                Console.WriteLine();
            }
        }

        static bool IsLogicalSquare(int[,] array)
        {
            int n = array.GetLength(0); // Размерность двумерного квадратного массива
            int sum = 0; // эталон для сравнения

            for (int i = 0; i < n; i++)
            {
                sum += array[i, 0];
            }

            // Проверка сумм горизонталей и вертикалей
            for (int i = 0; i < n; i++)
            {
                int rowSum = 0;
                int colSum = 0;

                for (int j = 0; j < n; j++)
                {
                    rowSum += array[i, j];
                    colSum += array[j, i];
                }

                if (rowSum != colSum)
                    return false;
                if ((rowSum != sum) || (colSum != sum))
                    return false;
            }

            // Проверка сумм двух диагоналей
            int mainDiagonalSum = 0;
            int antiDiagonalSum = 0;

            for (int i = 0; i < n; i++)
            {
                mainDiagonalSum += array[i, i]; // \
                antiDiagonalSum += array[i, n - 1 - i]; // /
            }

            if (mainDiagonalSum != antiDiagonalSum)
                return false;
            if ((mainDiagonalSum != sum) || (antiDiagonalSum != sum))
                return false;

            return true;
        }

        static void BubbleSortAlgorithm(int[] array)
        {
            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // Swap elements
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        swapped = true;
                    }
                }

                // Если во внутреннем цикле не было заменено ни одного элемента, то массив уже отсортирован.
                if (!swapped)
                    break;
            }
        }


        static void Main(string[] args)
        {
            int size = 0;
            Console.Write("Введите размерность массива: ");


            bool flag = true;
            while (flag) // проверка ввода
            {
                try { size = Convert.ToInt32(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }


            int[,] resultArray = new int[size, size];
            int[,] array = new int[size, size];
            int[] firstElements = new int[size];

            // Заполнение массива случайными числами
            Random random = new Random();
            int[,] arrayFill = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    array[i, j] = random.Next(-50, 151); // [-50,150]
                }
            }


            Console.WriteLine();
            Console.WriteLine("Исходный массив: ");
            Print2DArray(array, size);
            Console.WriteLine();


            // Проверка, является ли массив логическим квадратом
            bool logic = IsLogicalSquare(array);
            if (logic)
            {
                Console.WriteLine("Массив является логическим квадратом.");
                arrayFill = array;
            }
            else
            {
                Console.WriteLine("Массив не является логическим квадратом.");

                // Транспонирование массива (чтобы строки стали столбцами иначе не сработает метод sort
                // он не работает для столбцов)
                int[,] transposedArr = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        transposedArr[j, i] = array[i, j];
                    }
                }

                // Сортировка столбцов
                for (int i = 0; i < size; i++)
                {
                    int[] column = new int[size];
                    for (int j = 0; j < size; j++)
                    {
                        column[j] = transposedArr[i, j];
                    }
                    Array.Sort(column);
                    for (int j = 0; j < size; j++)
                    {
                        transposedArr[i, j] = column[j];
                    }
                }

                // Обратное транспонирование массива
                int[,] sortedArr = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        sortedArr[j, i] = transposedArr[i, j];
                    }
                }
                
                arrayFill = sortedArr;


                //Console.WriteLine("Массив с отсортированными по неубыванию элементами столбцов: ");
                //Print2DArray(arrayFill, size);
                Console.WriteLine();


                // Сортировка столбцов по неубыванию минимальных элементов в них //
                
                // Создаем массив для хранения первых элементов стобцов
                for(int col = 0; col < size; col++)
                {
                    firstElements[col] = arrayFill[0, col];
                }

                BubbleSortAlgorithm(firstElements);

                // Вывод массива
                //for (int i = 0; i < size; i++)
                //{
                //    Console.Write(firstElements[i] + " ");
                //}
                //Console.WriteLine();


                // Отсортировать исходный массив по возрастанию минимальных элементов в стобцах
                int index_i = 0, index_j = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (firstElements[i] == arrayFill[0, j]) // таким образом находим индекс столбцам с минимальным элементом
                        {
                            //Console.WriteLine(firstElements[i] + " " + arrayFill[0, j]);
                            for (index_i = 0; index_i < size; index_i++) // переписываем столбец, все что ниже минимального элемента
                            {
                                resultArray[index_i, index_j] = array[index_i, j];
                            }
                            index_j++; // переход на другой столбец
                        }
                    }
                }
            }


            Console.WriteLine("Отсортированный по столбцам массив: ");
            Print2DArray(resultArray, size);

            Console.WriteLine();
            Console.WriteLine("Минимальный элементы в столбцах массива: ");
            for (int i = 0; i < size; i++)
            {
                Console.Write(firstElements[i] + " ");
            }

            //Print2DArray(arrayFill, size);
            Console.ReadLine();
        }
    }
}