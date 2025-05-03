using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
5.1.	Разработайте консольное приложение, которое при запуске выдает «меню» со списком возможных заданий,
а также вариант («8») – закрытие приложения. Пользователь вводит номер задания.

1)	В данном натуральном числе переставьте цифры таким образом, чтобы образовалось наибольшее из
записанных этими же цифрами число.

2)	Найдите все пары дружественных чисел, не превышающих A. Два числа называются дружественными, 
если каждое из них равно сумме всех делителей другого (само число в качестве делителя не рассматривается). 
Целое число A вводится с клавиатуры. Числа в каждой паре должны стоять по возрастанию.
Например, для А = 5000 необходимо вывести пары чисел:
220	284
1184	1210
2620	2924

3)	Найдите все автоморфные числа, меньшие A (A вводится с клавиатуры). Автоморфным называется число, 
равное последним цифрам своего квадрата (например, 252 = 625). Учитывайте, что автоморфное число может 
заканчиваться цифрами 1, 5 или 6, а не проверяйте все числа подряд.

4)	Определите, можно ли сложить несколько подряд идущих натуральных чисел (не обязательно начиная с единицы), 
чтобы получить число A (A вводится с клавиатуры). Если это возможно, то выведите, из каких чисел получается 
такая сумма (один вариант).

5)	Пользователь вводит натуральное число A. Выясните, имеются ли среди чисел от A до 2  A близнецы, 
т. е. простые числа, разность между которыми равна двум.

6)	Используя рекурсию, выведите латинский алфавит следующим образом:
А	B	C	D	E	…	V	W	X	Y	Z
	B	C	D	E	…	V	W	X	Y	
		C	D	E	…	V	W	X		
					...					
		C	D	E	…	V	W	X		
	B	C	D	E	…	V	W	X	Y	
А	B	C	D	E	…	V	W	X	Y	Z
В самом среднем ряду должно выводиться N букв (0 < N ≤ 26, определяется случайным образом), следовательно, 
количество строк варьируется в зависимости от N.

После выполнения каждого задания меню отображается заново.
*/

namespace Task_1
{
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine("Выберете опцию: ");
            Console.WriteLine("(1)");
            Console.WriteLine("(2)");
            Console.WriteLine("(3)");
            Console.WriteLine("(4)");
            Console.WriteLine("(5)");
            Console.WriteLine("(6)");
            Console.WriteLine("(8)");
            Console.Write(">> ");
        }

        static void Task1()
        {
            string s = "";
            int number = 0;
            bool flag = true;

            Console.Write("Введите число: ");
            while (flag) // проверка ввода
            {
                try { number = Convert.ToInt32(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }
            s = number.ToString();

            List<int> array = new List<int>(); // массив для цифер

            foreach (char i in s) // добавляет все цифры в массив
            {
                array.Add(int.Parse(i.ToString()));
            }
    
            array.Sort();
            array.Reverse();

            string str= ""; // строка для записи числа

            foreach (int i in array) // добавляет все цифры в строку, получаем число
            {
                str += i.ToString();
            }

            Console.WriteLine(str); // return str; (static string Task1(string n))
        }

        static void Task2()
        {
            bool flag = true;
            int A = 0;

            Console.Write("Введите число A: ");

            while (flag) // проверка ввода
            {
                try { A = int.Parse(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }

            // Массивы с суммами делителей чисел
            int[] sumDel1 = new int[A];
            int[] sumDel2 = new int[A];

            for (int i = 1; i < A; i++)
            {
                for (int j = 1; j <= i / 2; j++) // внутренний цикл для нахождения делителей числа i
                {
                    if (i % j == 0) // если i делиться нацело на j, то добавляем j к сумме делителей числа
                    {
                        sumDel1[i] += j;
                    }
                }
                for (int j = 1; j <= sumDel1[i] / 2; j++) // второй внутренний цикл для нахождения делителей суммы делителей числа i
                {                                         // поиск делителей второго числа
                    if (sumDel1[i] % j == 0)
                    {
                        sumDel2[i] += j;
                    }
                }
                if (sumDel2[i] == i && i < sumDel1[i]) // проверка, что числа не являются одинаковыми и проверка на суммы делителей
                {
                    Console.WriteLine("{0} {1}", i, sumDel1[i]);
                }
            }
        }

        
        static void Task3()
        {
            bool flag = true;
            int A = 0;
            Console.Write("Введите число A: ");

            while (flag) // проверка ввода
            {
                try { A = int.Parse(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }

            for (int i = 1; i < A; i++) // цикл до числа А
            {
                if (i % 10 == 5 || i % 10 == 6 || i % 10 == 1) // проверяем числа которые оканчиваются на определенные цифры
                {
                    int square = i * i; // квадрат числа
                    string iStr = i.ToString(); // преобразуем i в строку
                    string squareStr = square.ToString(); // преобразуем square в строку

                    // Эта формула проверяет, заканчивается ли строка iStr подстрокой, сравнивая ее с обрезанной версией строки squareStr. 
                    // Функция EndsWith() проверяет заканчивается ли строка iStr указанной подстрокой.
                    // Функция Substring() вычисляет подстроку
                    // Если последние цифры squareStr совпадают с последними цифрами iStr.
                    if (iStr.EndsWith(squareStr.Substring(squareStr.Length - iStr.Length)))
                    {
                        Console.WriteLine(i);
                    }
                }

            }
        }
        
        static void Task4()
        {
            int A = 0;
            bool flag = true;

            Console.Write("Введите число A: ");
            
            while (flag) // проверка ввода
            {
                try { A = int.Parse(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }

            for (int i = 1; i < A; i++)
            {
                int sum = i; // перебираем суммы последовательностей начиная с единицы и до А
                for (int j = i + 1; j <= A; j++)
                {
                    sum += j; // прибавляем к сумме все последующие числа, которые больше i
                    if (sum == A)
                    {
                        Console.Write("Можно получить сумму из чисел: ");
                        for (int k = i; k <= j; k++)
                        {
                            Console.Write(k + " ");
                        }
                        Console.WriteLine();
                        return;
                    }
                    // можно добавить проверку, если сумма больше А, то выходим из внутреннего цикла
                }
            }

            Console.WriteLine("Невозможно получить такую сумму из подряд идущих чисел.");
        }

        static void Task5()
        {
            bool flag = true;
            int A = 0;

            Console.Write("Введите число A: ");
            // int A = int.Parse(Console.ReadLine()); - преобразует число в целочисленный вид
            while (flag) // проверка ввода
            {
                try { A = int.Parse(Console.ReadLine()); flag = false; }
                catch { Console.WriteLine("Ошибка ввода."); }
            }

            for (int i = A; i <= 2 * A; i++)
            {
                bool isPrime = true; // простое число
                for (int j = 2; j <= Math.Sqrt(i); j++) // проверяем является ли текущее число простым т.е. имеет ли еще делители, кроме (1 и себя самого)
                {
                    if (i % j == 0)
                    {
                        isPrime = false; // число не простое
                        break;
                    }
                }

                if (isPrime && i > A + 1) // проверяет, является ли текущее число простым и больше A+1. A+1, потому что вычитаем (-2)
                {
                    bool isTwin = true; // число близнец
                    for (int k = 2; k <= Math.Sqrt(i - 2); k++)
                    {
                        if ((i - 2) % k == 0) // проверяем, является число простым
                        {
                            isTwin = false; // (i-2) не является близнецом для числа (i)
                            break;
                        }
                    }

                    if (isTwin) // если является близнецом, то выводим эти два числа
                    {
                        Console.WriteLine($"Найдена пара близнецов: {i - 2} и {i}");
                        return;
                    }
                }
            }
            Console.WriteLine("Таких пар не существует.");
        }

        static void Task6(string alphabet, int k, int N)
        {
            if (alphabet.Count<char>() == 2) // если кол-во символов алфавита = 2
            {
                for (int i = 0; i < k; i++) { Console.Write(" "); } // выводит отступы
                Console.Write(alphabet); // выводит алфавит
                Console.WriteLine(); // переход на следующу строку
                for (int i = 0; i < k; i++) { Console.Write(" "); } // выводит отступы
                Console.Write(alphabet); // выводит алфавит
                Console.WriteLine(); // переход на следующу строку
                return;
            }
            else if (N == 1) // для нечетного N, чтоб выводилась общая нечетная строка
            {
                for (int i = 0; i < k; i++) { Console.Write(" "); } // выводит отступы
                Console.Write(alphabet); // выводит алфавит
                Console.WriteLine(); // переход на следующую строку
                return;
            }
            else if (N <= 0) // для четного N, чтобы завершить рекурсию
            {
                return;
            }
            else // если кол-во символов алфавита > 2
            {
                N -= 2;
                for (int i = 0; i < k; i++) { Console.Write(" "); } // выводит отступы
                Console.Write(alphabet); // выводит алфавит
                Console.WriteLine(); // переход на следующую строку
                Task6(alphabet.Substring(1, alphabet.Count<char>() - 2), k + 1, N); // рекурсивный вызов функции
                for (int i = 0; i < k; i++) { Console.Write(" "); } // выводит отступы
                Console.Write(alphabet); // выводит алфавит
                Console.WriteLine(); // переход на следующую строку
             }
        }
        
        static void Main(string[] args)
        {
            int options = 0;
            bool exit = false;
            string s = "";
            bool flag = true;

            while (!exit)
            {
                Menu();
                while (flag)
                {
                    try { options = Convert.ToInt32(Console.ReadLine()); flag = false; } 
                    catch { Console.WriteLine("Ошибка ввода."); }
                }
                flag = true;

                switch (options)
                {
                    case 1:
                        Task1();
                        break;
                    case 2: 
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    case 5:
                        Task5();
                        break;
                    case 6:
                        Random random = new Random();
                        int N = random.Next(1, 27); // генерируем случайное число N от 1 до 26
                        N = N / 2; // Потому что симметричный вывод (кол-во строк -> 26/2 = 13)
                        Task6("abcdefghijklmnopqrstuvwxyz", 0, N);
                        //Task6("abcdefghijklmnopqrstuvwxyz", 0);
                        Console.WriteLine(N);
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Такой опции не существует.");
                        break;
                   }

                Console.Write("Введите любой символ для продолжения: ");
                s = Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
