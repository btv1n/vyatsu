using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.6.	Определить, является ли семизначное число, введенное с клавиатуры, 
             * палиндромом. Программа должна содержать проверку того, что введенное число 
             * действительно семизначное*/

            Console.Write("Введите семизначное число: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if ((number >= 1000000) && (number < 10000000))
            {
                int n1 = number / 1000000; //первая цифра
                int n2 = (number / 100000) % 10;
                int n3 = (number / 10000) % 10;
                int n4 = (number / 1000) % 10;
                int n5 = (number / 100) % 10;
                int n6 = (number / 10) % 10;
                int n7 = number % 10; //последная цифра

                if ((n1 == n7) && (n2 == n6) && (n3 == n5))
                {
                    Console.WriteLine("Введенное число является палиндромом");
                }
                else
                {
                    Console.WriteLine("Введенное число не является палиндромом");
                }
            }
            else
            {
                Console.WriteLine("Введенное число не является семизначным");
            }
        }
    }
}
