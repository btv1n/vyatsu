using System;

class Program
{
    const double M_PI = Math.PI; // Число ПИ

    // Формулы для вычисления размера части ананаса
    static double PieceSize(double x1, double x2, double bh, double ah)
    {
        return M_PI * Math.Pow(ah, 2) * ((x2 - x1) + 1 / (3.0 * Math.Pow(bh, 2)) * (Math.Pow(x1, 3) - Math.Pow(x2, 3)));
    }

    static void Main()
    {
        double a, b;
        int n;

        // a - ширина ананаса (см)
        // b - высота ананаса (см)
        // n - кол-во кусков
        string[] input = Console.ReadLine().Split(' ');
        a = double.Parse(input[0]);
        b = double.Parse(input[1]);
        n = int.Parse(input[2]);

        // Вычисляем шаг и начальное значение x
        double dx = b / n;
        double x = -b / 2 + dx;

        // Выводит вес каждого куска в граммах с точностью до микрограмма в порядке их отрезания
        while (x <= b / 2 + 0.000001) // идем до середины ширины, поскольку дальше результаты симметричны
        {
            //Console.WriteLine(pieceSize(x - dx, x, b / 2, a / 2));
            Console.WriteLine("{0:F6}", PieceSize(x - dx, x, b / 2, a / 2));
            x += dx;
        }

        string str = Console.ReadLine();
    }
}
