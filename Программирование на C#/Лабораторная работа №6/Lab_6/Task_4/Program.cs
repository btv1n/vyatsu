using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
6.4.Заполните массив следующим образом. Размерность массива определяется константами.

	1		3		4		10		11		21
	2		5		9		12		20		22
	6		8		13		19		23		30
	7		14		18		24		29		31
	15		17		25		28		32		35
	16		26		27		33		34		36

После заполнения замените все четные элементы на сумму их соседей (соседними являются элементы, 
расположенные на одну строку выше или ниже в том же столбце или на один столбец левее или правее 
в той же строке; в зависимости от положения элемента количество его соседей может быть два, три 
или четыре).
*/


namespace Task_4
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

        static void Main(string[] args)
        {
            int sum = 0;
            const int SIZE = 6;
            int[,] arrayResult = new int[SIZE, SIZE];
            int[,] array = new int[SIZE, SIZE];
            //{
            //    { 1, 3, 4, 10, 11, 21},
            //    { 2, 5, 9, 12, 20, 22},
            //    { 6, 8, 13, 19, 23, 30},
            //    { 7, 14, 18, 24, 29, 31},
            //    { 15, 17, 25, 28, 32, 35},
            //    { 16, 26, 27, 33, 34, 36}
            //};


            // Заполнение массива лесенкой c помощью двух циклов
            int number = 1;
            int i = 0, j = 0;
            //for (int i_index = 0; i_index < SIZE; i_index++)
            //{
            //    for (int j_index = 0; j_index < SIZE; j_index++)
            //    {
            //    }
            //}

            /*if (i % 2 == 0 && j == 0) // один шаг
                {
                    array[i, j] = number; number++;
                    i++;
                }
                else if (i % 2 == 1 && j == 0)
                {
                    while (i != 0)
                    {
                        array[i, j] = number; number++;
                        i--;
                        j++;
                    }
                }
                else if (j % 2 == 1 && i == 0) // один шаг
                {
                    array[i, j] = number; number++;
                    j++;
                }
                else if (j % 2 == 0 && i == 0)
                {
                    while (j != 0)
                    {
                        array[i, j] = number; number++;
                        i++;
                        j--;
                    }
                }*/
            while (number < 36) // Console.WriteLine(array[1, 0]); // 2
            {

                //Console.WriteLine("(1)");
                //Console.WriteLine("i = " + i + " j = " + j);
                //////////////////////////////////////////////

                if (i % 2 == 0 && j == 5) { array[i, j] = number; number++; i++;}

                if (j % 2 == 1 && i == 0 && i < 5) { array[i, j] = number; number++; j++; }

                if (i % 2 == 1 && j == 5)
                {
                    while (i != 5) { array[i, j] = number; number++; j--; i++; }
                }

                if (j % 2 == 1 && i == 5) { array[i, j] = number; number++; j++; }

                if (j % 2 == 0 && i == 5)
                {
                    while (j != 5) { array[i, j] = number; number++; i--;j++;}
                }

                //////////////////////////////////////////////

                if (i % 2 == 0 && j == 0 && j < 5) { array[i, j] = number; number++; i++; }

                // i < 6 чтобы не выйти за границы массива
                if (i % 2 == 1 && j == 0 && i < SIZE)
                {
                    while (i != 0) {array[i, j] = number; number++; i--; j++;}
                }

                if (j % 2 == 1 && i == 0 && i < 5) { array[i, j] = number; number++; j++;}

                // j < 6 чтобы не выйти за границы массива
                if (j % 2 == 0 && i == 0 && j < SIZE) 
                {
                    while (j != 0) { array[i, j] = number; number++; j--; i++;}
                }

                //////////////////////////////////////////////

                if (j >= 6) j--;
                if (i >= 6) i--;

                //if (number == 22) number--;
                //Console.WriteLine("i = " + i + " j = " + j);
                //Console.WriteLine("number = " + number);
            }



            Console.WriteLine("Исходный массив: ");
            Print2DArray(array, SIZE);
            Console.WriteLine();


            /*
            // Добавляем к элементам массива сумму их соседей
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (array[i, j] % 2 == 0) // Проверяем, является ли элемент четным числом
                    {
                        sum = 0; // сумма соседей

                        // Проверяем соседей элемента вверх и вниз
                        if (i - 1 >= 0)
                        {
                            sum += array[i - 1, j]; // сосед сверху
                        }

                        if (i + 1 < SIZE) // SIZE - ROWS
                        {
                            sum += array[i + 1, j]; // сосед снизу
                        }

                        // Проверяем соседей элемента слева и справа
                        if (j - 1 >= 0)
                        {
                            sum += array[i, j - 1]; // сосед слева
                        }

                        if (j + 1 < SIZE) // SIZE - COLS
                        {
                            sum += array[i, j + 1]; // сосед справа
                        }

                        // Заменяем четный элемент на сумму его соседей
                        arrayResult[i, j] = sum;
                    }
                    else
                    {
                        arrayResult[i, j] = array[i, j]; // переносим элемент из исходного массива в результирующий массив
                    }
                }
            }
            */
            Console.WriteLine("Массив после преобразований: ");
            Print2DArray(arrayResult, SIZE);


            Console.ReadLine();
        }
    }
}
