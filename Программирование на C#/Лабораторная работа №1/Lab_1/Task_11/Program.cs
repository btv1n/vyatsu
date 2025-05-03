using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.11.	Напишите программу, запрашивающую имя и возраст в годах человека мужского пола. 
             * Определить возрастную категорию (до года – «младенец», от года до 11 лет – «ребенок», 
             * от 12 до 15 лет – «подросток», от 16 до 25 лет – «юноша», от 26 до 70 лет – «мужчина», 
             * от 70 до 99 лет – «старик», более 100 лет – «долгожитель»). Вывести на экран сообщение: 
             * (например, введены имя Иван и возраст 20) «Иван – юноша. Ему 20 лет.».*/

            // лет: 0
            // год: 1
            // года: 2 3 4
            // лет: 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20
            // год: 21
            // года: 22 23 24
            // лет: 25 26 27 28 29 30
            // лет: 111 112 113 114 115 116 117 118 119 120 125 126 127 128 129
            // год: 121
            // года: 122 123 124
            // лет: 211 212 213 214 215 216 217 218 219 220
            // год: 221
            // года: 222 223 224
            // лет: 225 226 227 228 229 230

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());

            string status = "человек";

            Console.WriteLine();

            if (age <= 1)
                status = "младенец";
            if ((age > 1) && (age <= 11))
                status = "ребенок";
            if ((age >= 12) && (age <= 15))
                status = "подросток";
            if ((age >= 16) && (age <= 25))
                status = "юноша";
            if ((age >= 26) && (age <= 70))
                status = "мужчина";
            if ((age > 70) && (age <= 99))
                status = "старик";
            if (age >= 100)
                status = "долгожитель";


            if (age == 0)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " лет.");
            }
            else if (age == 1)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " год.");
            }
            else if (age >= 2 && age <= 4)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " года.");
            }
            else if (age >= 5 && age <= 20)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " лет.");
            }
            else if (age % 100 >= 5 && age % 100 <= 20)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " лет.");
            }
            else if (age % 10 == 1)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " год.");
            }
            else if (age % 10 >= 2 && age % 10 <= 4)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " года.");
            }
            else if (age % 10 >= 5 && age % 10 <= 9 || age % 10 == 0)
            {
                Console.WriteLine(name + " - " + status + ". Ему " + age + " лет.");
            }


            Console.WriteLine();
        }
    }
}