using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
6.9. Подсчитайте сумму чисел, встречающихся в строке. Символ ‘–’ перед 
числом считается знаком отрицательного числа. Все остальные символы (кроме 
цифр и минуса) считаются разделителями между числами.
*/

namespace Task_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string str = "-5j 10 ij --15- lkj ji -20 ij -25--"; // не работает со строкой "--15-" | -0
            string str = "-5j 10 ij -15     lkj ji -20 ij#$%#@&* 25";
            int sum = 0;
            string currentNumber = "";
            bool flag = true;
            bool flag2 = false;

            str = Console.ReadLine(); // Ввод строки
           
            str += " "; // добавляет дополнительный символ для того, чтобы не выйти за границы строки

            string strError = str.Replace(" ", ""); // удаляем лишние пробелы


            // Проверка строки на правильность
            if (strError == "-")
            {
                Console.WriteLine("Неверная строка.");
                flag = false;
            }
                
            for (int i = 0; i < str.Length; i++) 
            {
                if ((i != 0) &&
                    ((str[i-1] == '-' && str[i] == '-') 
                    || (char.IsDigit(str[i-1]) && str[i] == '-')
                    || (str[i-1] == ' ' && str[i] == '-' && !char.IsDigit(str[i+1])))) // определяет " - "
                {
                    flag = false;
                    Console.WriteLine("Неверная строка.");
                    break;
                }
                else if (!char.IsDigit(str[i]) && str[i] != '-' && str[i] != ' ' && str[i] != '+')
                {
                    flag2 = true;
                }
            }
            if (flag2 && flag)
                Console.WriteLine("Введенная строка содержит неподходящие символы.");


            if (flag)
            {
                foreach (char c in str) // перебираем каждый символ в строке
                {
                    if (char.IsDigit(c) || c == '-') // является ли текущий символ - цифрой или знаком минуса
                    {
                        currentNumber += c; // добавляем к текущему числу
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentNumber)) // проверяем является текущая строка пустой или равной нулю (сurrentNumber)
                        {
                            sum += int.Parse(currentNumber); // текущее число преобразуется в целое и добавляется к сумме
                            currentNumber = "";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(currentNumber)) // проверяет является текущая строка пустой или равной нулю
                {
                    sum += int.Parse(currentNumber);
                }

                Console.WriteLine("Сумма чисел: " + sum);
            }
            
            Console.ReadLine();
        }
    }
}
