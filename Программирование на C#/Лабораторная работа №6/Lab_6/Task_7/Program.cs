using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ошибки в решении задачи

/*
6.7. Удалите строку и столбец, на пересечении которых находится минимальный элемент массива.
Вставьте строку и столбец из нулей между средними строками и столбцами получившегося массива 
(если их четное количество). Операции удаления и вставки строк/столбцов осуществить путем сдвига 
соответствующих элементов двумерного массива. Решение задачи путем вывода нескольких фрагментов 
исходного массива считается неверным. Программа должна содержать метод по выводу элементов массива 
с индекса [0, 0] до индекса [n, m].
*/

namespace Task_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // инициализация и заполнение массива
            int[,] array = {
            { 5, 6, 2, 1 },
            { 3, 4, 8, 9 },
            { 7, 2, 6, 3 },
            { 4, 5, 9, 7 }
            };

            // нахождение минимального элемента и его позиции
            int min = array[0, 0];
            int minRow = 0;
            int minCol = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                        minRow = i;
                        minCol = j;
                    }
                }
            }

            // удаляем строку и столбец с минимальным элементом
            int[,] newArray = new int[array.GetLength(0) - 1, array.GetLength(1) - 1];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i == minRow)
                    continue;

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j == minCol)
                        continue;

                    int newRow = i;
                    int newCol = j;

                    if (i > minRow)
                        newRow--;

                    if (j > minCol)
                        newCol--;

                    newArray[newRow, newCol] = array[i, j];
                }
            }

            // создаем новый массив с нулями между средними строками и столбцами
            int size = newArray.GetLength(0) + 2;
            if (newArray.GetLength(0) % 2 == 0)
                size++;

            int[,] resultArray = new int[size, size];
            int middleRow1 = newArray.GetLength(0) / 2;
            int middleRow2 = middleRow1 + 1;
            int middleCol1 = newArray.GetLength(1) / 2;
            int middleCol2 = middleCol1 + 1;

            for (int i = 0; i < resultArray.GetLength(0); i++)
            {
                for (int j = 0; j < resultArray.GetLength(1); j++)
                {
                    if (i < middleRow1)
                    {
                        if (j < middleCol1)
                        {
                            resultArray[i, j] = newArray[i, j];
                        }
                        else if (j == middleCol1)
                        {
                            resultArray[i, j] = 0;
                        }
                        else if (j > middleCol1)
                        {
                            resultArray[i, j] = newArray[i, j - 1];
                        }
                    }
                    else if (i == middleRow1)
                    {
                        if (j < middleCol1)
                        {
                            resultArray[i, j] = 0;
                        }
                        else if (j == middleCol1)
                        {
                            resultArray[i, j] = 0;
                        }
                        else if (j > middleCol1)
                        {
                            resultArray[i, j] = newArray[i - 1, j - 1];
                        }
                    }
                    else if (i > middleRow1)
                    {
                        if (j < middleCol1)
                        {
                            resultArray[i, j] = newArray[i - 1, j];
                        }
                        else if (j == middleCol1)
                        {
                            resultArray[i, j] = 0;
                        }
                        else if (j > middleCol1)
                        {
                            resultArray[i, j] = newArray[i - 1, j - 1];
                        }
                    }
                }
            }

            // вывод элементов получившегося массива
            for (int i = 0; i < resultArray.GetLength(0); i++)
            {
                for (int j = 0; j < resultArray.GetLength(1); j++)
                {
                    Console.Write(resultArray[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
