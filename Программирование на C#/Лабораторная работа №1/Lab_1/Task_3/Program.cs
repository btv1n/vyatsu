using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.3.	Напишите программу, которая запрашивает с клавиатуры названия театра и 
             * спектакля, время представления и выводит сообщение (например, если введено 
             * название театра «Этсетера», название спектакля «Тихие ночи», время 
             * представления «21:00», цена билета «2350»): «Приглашаем в наш театр 
             * “Этсетера”. У нас вы можете насладиться спектаклем “Тихие ночи” в 21:00. 
             * Стоимость билета 2350 руб.».*/

            Console.WriteLine("Введите название театра: ");
            string nameTheater = Console.ReadLine();
            Console.WriteLine("Введите название спектакля: ");
            string nameSpect = Console.ReadLine();
            Console.WriteLine("Введите время представления: ");
            string time = Console.ReadLine();
            Console.WriteLine("Введите цену билета: ");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("<Приглашаем в наш театр \"" + nameTheater + "\". У нас вы можете насладиться спектаклем \"" + nameSpect + "\" в " + time +
                " Стоимость билета " + price + " руб.>");
        }
    }
}
