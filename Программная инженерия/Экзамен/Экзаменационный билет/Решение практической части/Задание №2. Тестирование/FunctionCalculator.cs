using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Используя разработку через тестирование написать программу на C#. Используя все известные вем методы тестирования 
черного и белого ящика дописать модульные тесты к программе. При написании тестов использовать ААА и параметризованные тесты.
Посчитать значения функции y = f(x). Проконтролировать правильность ввода x и корректность вычисляемого выражения y=(x/(x-10)^2)^0.5.
*/

/*
ОДЗ: x >= 0, x != 10
x не может быть < 0: мы должны получить действительное число, а квадратный корень из отрицательного числа в действительных числах не определён.
*/

namespace FuctionApp
{
    public class FunctionCalculator
    {
        public double Calculate(double x)
        {
            // Проверка корректности входных значений
            if (double.IsNaN(x) || double.IsInfinity(x))
                throw new ArgumentException("x должен быть конечным числом");

            // Проверка области определения функции
            if (x < 0)
                throw new ArgumentOutOfRangeException(nameof(x), "x не может быть меньше нуля");

            if (x == 10)
                throw new DivideByZeroException("происходит деление на ноль");

            // Вычисление функции
            return Math.Sqrt(x / Math.Pow(x - 10, 2));
        }
    }
}
