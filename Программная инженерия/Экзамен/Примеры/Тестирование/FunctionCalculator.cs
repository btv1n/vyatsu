using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp
{
    public class FunctionCalculator
    {
        public double Calculate(double x)
        {
            // Проверка корректности входных double-значений
            if (double.IsNaN(x) || double.IsInfinity(x))
                throw new ArgumentException("x должен быть конечным числом");

            // Проверка области определения функции: подкоренное выражение (x - 1) не должно быть меньше 0.
            // Это запрещает любые x < 1: включая 0.999, 0 и отрицательные числа
            if (x < 1)
                throw new ArgumentOutOfRangeException(nameof(x), "x не может быть меньше 1, так как подкоренное выражение становится отрицательным");

            // Проверка на деление на ноль (x не должен быть равен 4)
            if (x == 4)
                throw new DivideByZeroException("происходит деление на ноль при x = 4");

            // Вычисление функции
            //return Math.Sqrt(x / Math.Pow(x - 10, 2));
            // y = ((x-1)/(x-4)^2)^0.5
            return Math.Sqrt((x - 1) / Math.Pow((x - 4), 2));
        }
    }
}