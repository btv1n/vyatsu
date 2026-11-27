using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnogomernRandomOptimization
{
    class Program
    {// Задаваемая функция
        static double Function(double[] x)
        {
            // Пример функции: x1^2 + x2^2 (можно заменить на свою функцию)
            return Math.Pow(x[0], 2) + Math.Pow(x[1], 2);
        }

        // Проверка принадлежности точки области допустимых решений
        static bool IsValid(double[] x)
        {
            // Пример ограничений: x1, x2 ∈ [-10, 10]
            foreach (double xi in x)
            {
                if (xi < -10 || xi > 10) return false;
            }
            return true;
        }

        public static int menu()
        {
            Console.WriteLine("1.Метод слепого поиска + метод с локализацией");
            Console.WriteLine("2.Метод случайных направлений");
            Console.WriteLine("3.Завершить программу");
            Console.Write("Введите номер => ");
            return int.Parse(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            int num = menu();
            while (num != 3)
            {
                switch (num)
                {
                    case 1:
                        int dimensions = 2; //размерность      
                        int iterations = 1000; // Количество итераций
                        double min = -10;// Границы области поиска
                        double max = 10;
                        Console.WriteLine("Метод слепого поиска:");
                        BlindSearch(dimensions, iterations, min, max);
                        Console.WriteLine("\nМетод с локализацией:");
                        BlindSearch(dimensions, iterations, min, max, localized: true);
                        break;
                    case 2:
                        dimensions = 2; // размерность
                        double[] startPoint = { 5.0, 5.0 };// Начальная точка
                        double step = 1.0; // Начальный шаг
                        int maxFailures = 5;// Количество неудач для уменьшения шага
                        double epsilonX = 1e-5;// Точность
                        double epsilonF = 1e-5;
                        double stepReductionFactor = 0.5;// Коэффициент уменьшения шага
                        int maxIterations = 1000;// Максимальное число итераций
                        Console.WriteLine("Метод случайных направлений с возвратом при неудачном шаге:");
                        RandomDirectionSearch(dimensions, startPoint, step, maxFailures, epsilonX, epsilonF, stepReductionFactor, maxIterations);
                        break;
                    case 3:
                        Console.WriteLine("Программа завершена");
                        break;
                    default:
                        Console.WriteLine("Выбран неправильный номер!");
                        break;
                }
                num = menu();
            }
        }

        // Генерация случайной точки
        static double[] GenerateRandomPoint(int dimensions, double min, double max, Random rand)
        {
            double[] point = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                point[i] = rand.NextDouble() * (max - min) + min;
            }
            return point;
        }

        // Метод слепого поиска
        static void BlindSearch(int dimensions, int iterations, double min, double max, bool localized = false)
        {
            Random rand = new Random();
            List<double[]> points = new List<double[]>();

            double[] bestPointMax = null, bestPointMin = null;
            double fMax = double.MinValue, fMin = double.MaxValue;

            double localizationFactor = 0.5; // Коэффициент сужения области поиска

            for (int iter = 0; iter < iterations; iter++)
            {
                double[] point;

                if (localized && bestPointMax != null)
                {
                    // Генерация точки в суженной области
                    point = GenerateRandomPoint(dimensions, bestPointMax[0] - localizationFactor, bestPointMax[0] + localizationFactor, rand);
                }
                else
                {
                    // Генерация точки в полной области
                    point = GenerateRandomPoint(dimensions, min, max, rand);
                }

                if (IsValid(point))
                {
                    points.Add(point);

                    double fValue = Function(point);
                    if (fValue > fMax)
                    {
                        fMax = fValue;
                        bestPointMax = point;
                    }

                    if (fValue < fMin)
                    {
                        fMin = fValue;
                        bestPointMin = point;
                    }
                }
            }

            // Вывод результатов
            Console.WriteLine("Сгенерированные точки:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var p in points)
            {
                Console.WriteLine($"({string.Join(", ", p)}) -> f = {Function(p)}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nМаксимум функции: Fmax = {fMax} в точке ({string.Join(", ", bestPointMax)})");
            Console.WriteLine($"Минимум функции: Fmin = {fMin} в точке ({string.Join(", ", bestPointMin)})");
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Генерация случайного нормализованного направления
        static double[] GenerateRandomDirection(int dimensions, Random rand)
        {
            double[] direction = new double[dimensions];
            double length = 0;

            for (int i = 0; i < dimensions; i++)
            {
                direction[i] = rand.NextDouble() * 2 - 1; // Генерация в диапазоне [-1, 1]
                length += Math.Pow(direction[i], 2);
            }

            length = Math.Sqrt(length);

            // Нормализация
            for (int i = 0; i < dimensions; i++)
            {
                direction[i] /= length;
            }

            return direction;
        }

        // Метод случайных направлений с возвратом при неудачном шаге
        static void RandomDirectionSearch(int dimensions, double[] startPoint, double step, int maxFailures, double epsilonX, double epsilonF, double stepReductionFactor, int maxIterations)
        {
            Random rand = new Random();
            double[] currentPoint = (double[])startPoint.Clone();
            double currentF = Function(currentPoint);
            double[] bestPoint = (double[])currentPoint.Clone();
            double bestF = currentF;

            int failureCount = 0;
            int iteration = 0;

            Console.WriteLine("Начальная точка: ({0}), f = {1}", string.Join(", ", currentPoint), currentF);

            while (iteration < maxIterations)
            {
                double[] direction = GenerateRandomDirection(dimensions, rand);
                double[] newPoint = new double[dimensions];
                for (int i = 0; i < dimensions; i++)
                {
                    newPoint[i] = currentPoint[i] + step * direction[i];
                }

                if (IsValid(newPoint))
                {
                    double newF = Function(newPoint);

                    if (Math.Abs(newF - currentF) < epsilonF)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Достигнута точность по функции: |Fnew - Fcurrent| < εF");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                    if (newF < currentF) // Условие улучшения
                    {
                        currentPoint = newPoint;
                        currentF = newF;
                        failureCount = 0; // Сброс счетчика неудач

                        if (newF < bestF)
                        {
                            bestF = newF;
                            bestPoint = (double[])newPoint.Clone();
                        }
                    }
                    else
                    {
                        failureCount++;
                        if (failureCount >= maxFailures)
                        {
                            step *= stepReductionFactor;
                            failureCount = 0; // Сброс счетчика
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Шаг уменьшен до: {0}", step);
                        }
                    }
                }
                else
                {
                    failureCount++;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Итерация {0}: Точка ({1}), f = {2}, шаг = {3}", iteration + 1, string.Join(", ", currentPoint), currentF, step);
                Console.ForegroundColor = ConsoleColor.White;
                if (step < epsilonX)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Достигнута точность по координатам: шаг < εX");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                iteration++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nРезультаты:");
            Console.WriteLine("Минимум функции: Fmin = {0} в точке ({1})", bestF, string.Join(", ", bestPoint));
            Console.WriteLine("Число итераций: {0}", iteration);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

/*Пример функции с несколькими экстремумами
 f(x1, x2) = sin(x1) * cos(x2) */