using System;
using System.Collections.Generic;

class Program
{
    static int Gcd(int a, int b)
    {
        return a == 0 ? b : Gcd(b % a, a);
    }
    static int Gcd(int a, int b, int c)
    {
        return Gcd(a, Gcd(b, c));
    }

    // Определение структуры Vector3D, представляющей трехмерный вектор
    public struct Vector3D
    {
        // Координаты
        public int x, y, z;

        // Перегрузка операторов вычитания, деления и сравнения для векторов.
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D { x = v1.x - v2.x, y = v1.y - v2.y, z = v1.z - v2.z };
        }
        public static Vector3D operator /(Vector3D v, int d)
        {
            return new Vector3D { x = v.x / d, y = v.y / d, z = v.z / d };
        }
        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }
        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3D)
            {
                return this == (Vector3D)obj;
            }
            return false;
        }

        // Хэш-функция для вектора
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 11) ^ (z.GetHashCode() << 22);
        }
    }

    static void Main()
    {
        const int SIZE = 2000;
        Vector3D[] A = new Vector3D[SIZE];

        int n;
        int r = 1;

        n = int.Parse(Console.ReadLine()); // ввод кол-ва объектов в пространстве

        // Ввод координат векторов в массив A
        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            A[i] = new Vector3D { x = int.Parse(inputs[0]), y = int.Parse(inputs[1]), z = int.Parse(inputs[2]) };
        }

        for (int i = 0; i < n; i++) // цикл по всем векторам в массиве
        {
            Dictionary<Vector3D, int> m = new Dictionary<Vector3D, int>();

            // Цикл по всем остальным векторам для нахождения НОД и обновления максимального значения "r".
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    continue;

                Vector3D v2 = A[j] - A[i]; // вычисление разности векторов
                Vector3D g = v2 / Gcd(v2.x, v2.y, v2.z); // вычисление вектора с единичными координатами
                if (m.ContainsKey(g))
                {
                    m[g]++;
                    r = Math.Max(r, 1 + m[g]); // обновление значения "r" с учетом нахождения одинаковых единичных векторов
                }
                else
                {
                    m.Add(g, 1);
                    r = Math.Max(r, 2); // обновление значения "r" при добавлении нового единичного вектора
                }
            }
        }

        Console.WriteLine(r); // вывод максимального количества точек
    }
}
