using System;

struct Point
{
    public double x, y; // координаты
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y); // вычитание точек
    }

    public static bool operator !=(Point a, Point b)
    {
        return a.x != b.x && a.y != b.y; // сравнение точек
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y; // сравнение точек
    }

    public double Length()
    {
        return Math.Sqrt(x * x + y * y); // вычисление длины вектора
    }
}

class Program
{
    static void Intersect(ref double s, ref double t, Point p1, Point p2, Point q1, Point q2)
    {
        Point d = new Point(q1.x - p1.x, q1.y - p1.y);
        double det = q2.x * p2.y - p2.x * q2.y;
        s = (q2.x * d.y - d.x * q2.y) / det;
        t = (p2.x * d.y - d.x * p2.y) / det;
    }

    static void Main()
    {
        //decimal eps = 1e-9m;//
        double eps = 0.000000001;

        int n, k;
        double h, d, lx, ly, s = 0, t = 0;

        // Прочитать все введенные данные
        string[] input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        h = double.Parse(input[1]);

        Point[] f = new Point[n];

        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine().Split();
            f[i] = new Point(double.Parse(input[0]), double.Parse(input[1]));
        }

        input = Console.ReadLine().Split();
        d = double.Parse(input[0]);
        lx = double.Parse(input[1]);
        ly = double.Parse(input[2]);
        //k = int.Parse(input[3]);
        k = int.Parse(Console.ReadLine());

        Point[] v = new Point[k];
        bool[] ans = new bool[k];

        for (int i = 0; i < k; i++)
        {
            input = Console.ReadLine().Split();
            v[i] = new Point(double.Parse(input[0]), double.Parse(input[1]));
        }

        for (int i = 0; i < n; i++)
        {
            Point p1 = f[i];
            Point p2 = f[(i + 1) % n] - f[i];

            for (int j = 0; j < k; j++)
            {
                Point q1 = new Point(lx, ly);
                Point q2 = v[j] - q1;

                
                Intersect(ref s ,ref t, p1, p2, q1, q2);

                if (q1 == v[j] || (s >= -eps && s <= 1 + eps && t >= 0))
                {
                    if (Math.Sqrt(h * h / (t * t) + q2.Length() * q2.Length()) < d + eps)
                    {
                        ans[j] = true;
                    }
                }
            }
        }

        int result = 0;
        foreach (bool val in ans)
        {
            if (val)
            {
                result++;
            }
        }

        Console.WriteLine(result); // вывод количества градин, попавших в область действия лазера и уничтоженных

        //string str = Console.ReadLine();
    }
}
