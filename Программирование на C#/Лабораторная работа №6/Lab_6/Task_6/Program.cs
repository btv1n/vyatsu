using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
6.6. Определите, есть ли в данном массиве столбец, содержащий ровно два отрицательных элемента. 
Если есть, то переставьте этот столбец после последнего столбца (количество столбцов остается 
неизменным).
*/

namespace Task_6
{
    internal class Program
    {
        static void Print2DArray(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array[i, j].ToString().PadRight(4) + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            /*
            int rows = 0, cols = 0;
            
            Console.Write("Введите размерность массива (сначала кол-во строк, потом кол-во столбцов): ");
            bool flag = true;
            while (flag) // проверка ввода
            {
                try { rows = Convert.ToInt32(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }
            flag = true;
            while (flag) // проверка ввода
            {
                try { rows = Convert.ToInt32(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }

            int[,] array = new int[rows, cols];

            // Заполнение массива случайными числами
            Random random = new Random();
            int[,] arrayFill = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = random.Next(-50, 151); // [-50,150]
                }
            }

            //Print2DArray(array);
            //Console.WriteLine();
            */

            int n = 3; // количество строк
            int m = 6; // количество столбцов

            int[,] matrix = 
            { 
                { -3, -9, -1, 2, 3, 4 }, 
                { -5, -9, 5, -6, -7, 8 }, 
                { 0, -9, 9, 10, 11, 12 } 
            }; // исходный массив

            bool found = false; // флаг наличия столбца с двумя отрицательными элементами
            int columnIndex = -1; // индекс столбца с двумя отрицательными элементами
            //int[] arrayForIndex = new int[m]; // массив для индексов

            int counterNegativeCols = 0;
            // Проверка наличия столбца с двумя отрицательными элементами
            for (int j = 0; j < m; j++)
            {
                int negativeCount = 0; // количество отрицательных элементов в столбце

                for (int i = 0; i < n; i++)
                {
                    if (matrix[i, j] < 0)
                    {
                        negativeCount++;
                    }
                }

                if (negativeCount == 2)
                {
                    found = true;
                    columnIndex = j;
                    counterNegativeCols++;
                    break;
                }
            }
            //Console.WriteLine("Кол-во столбцов с двумя отрицательными элементами: " + counterNegativeCols);

            // Переставляем все столбцы, которые содержат ровная 2 отрицательных элемента
            while (counterNegativeCols != 0)
            {
                // Перемещение столбца с двумя отрицательными элементами в конец массива
                if (found)
                {
                    int[] temp = new int[n]; // временный массив

                    for (int i = 0; i < n; i++)
                    {
                        // перестановка (!) НУЖНО ИЗМЕНИТЬ ПЕРЕСТАНОВКУ ЭЛЕМЕНТОВ
                        temp[i] = matrix[i, columnIndex];
                        matrix[i, columnIndex] = matrix[i, m - 1];
                        matrix[i, m - 1] = temp[i];
                    }
                }

                counterNegativeCols--;
            }


            Print2DArray(matrix); // Вывод измененного массива на экран
           

            Console.ReadLine();
        }
    }
}
