using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_9
{
    internal class Program
    {
        // Функция для определения високосного года
        static bool IsLeapYear(int year)
        {
            if (year % 400 == 0)
                return true;

            if (year % 100 == 0)
                return false;

            if (year % 4 == 0)
                return true;

            return false;
        }

        static void Main(string[] args)
        {

            /*1.9.	Вводится дата (день, месяц и год числами). Определить название месяца, время года и
             * количество дней, оставшихся до конца месяца. Программа должна проверять корректность 
             * введенной даты.*/

            int monthDays31 = 31, monthDays30 = 30, monthDays29 = 29, monthDays28 = 28;

            Console.Write("Введите день текущего месяца: ");
            int day = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите текущий месяц: ");
            int month = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите текущий год: ");
            int year = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            if ((day > 0) && (month > 0) && (year > 0) &&
               (((day <= 31) && (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)) ||
               ((day <= 30) && (month == 4 || month == 6 || month == 9 || month == 11)) ||
               (day <= 28 && month == 2 && !IsLeapYear(year)) ||
               (day <= 29 && month == 2 && IsLeapYear(year))))
            {
                switch (month)
                {
                    case 1:
                        Console.WriteLine("Текущий месяц: Январь");
                        Console.WriteLine("Текущее время года: Зима");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 2:
                        Console.WriteLine("Текущий месяц: Февраль");
                        Console.WriteLine("Текущее время года: Зима");
                        if (IsLeapYear(year)) //проверка на высокосный год
                        {
                            Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays29 - day));
                        }
                        else
                        {
                            Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays28 - day));
                        }
                        break;
                    case 3:
                        Console.WriteLine("Текущий месяц: Март");
                        Console.WriteLine("Текущее время года: Весна");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 4:
                        Console.WriteLine("Текущий месяц: Апрель");
                        Console.WriteLine("Текущее время года: Весна");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays30 - day));
                        break;
                    case 5:
                        Console.WriteLine("Текущий месяц: Май");
                        Console.WriteLine("Текущее время года: Весна");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 6:
                        Console.WriteLine("Текущий месяц: Июнь");
                        Console.WriteLine("Текущее время года: Лето");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays30 - day));
                        break;
                    case 7:
                        Console.WriteLine("Текущий месяц: Июль");
                        Console.WriteLine("Текущее время года: Лето");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 8:
                        Console.WriteLine("Текущий месяц: Август");
                        Console.WriteLine("Текущее время года: Лето");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 9:
                        Console.WriteLine("Текущий месяц: Сентябрь");
                        Console.WriteLine("Текущее время года: Осень");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays30 - day));
                        break;
                    case 10:
                        Console.WriteLine("Текущий месяц: Октябрь");
                        Console.WriteLine("Текущее время года: Осень");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    case 11:
                        Console.WriteLine("Текущий месяц: Ноябрь");
                        Console.WriteLine("Текущее время года: Осень");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays30 - day));
                        break;
                    case 12:
                        Console.WriteLine("Текущий месяц: Декабрь");
                        Console.WriteLine("Текущее время года: Зима");
                        Console.WriteLine("Количество дней, оставшихся до конца месяца: " + (monthDays31 - day));
                        break;
                    default:
                        Console.WriteLine("Такого месяца не существует");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Введенные данные неверны");
            }

            Console.WriteLine();
        }
    }
}
