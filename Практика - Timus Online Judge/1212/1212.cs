using System;
using System.Collections.Generic;
using System.Linq;

public class Ship
{
    public uint x;
    public uint y;
    public uint k;
    public char t;
}

class Program
{
    static uint Count(List<uint> p, uint k)
    {
        p.Sort();
        p = p.Distinct().ToList();

        uint q = 0;
        for (int i = 1; i < p.Count; i++)
        {
            uint d = p[i] - p[i - 1] - 1;
            if (d >= k)
                q += d - k + 1;
        }
        return q;
    }

    static uint Solve(uint n, uint m, List<Ship> s, uint k)
    {
        uint v = 0;

        for (uint y = 1; y <= n; y++)
        {
            List<uint> p = new List<uint> { 0, m + 1 };
            foreach (Ship q in s)
            {
                switch (q.t)
                {
                    case 'H':
                        if (q.y >= y - 1 && q.y <= y + 1)
                        {
                            p.Add(q.x - 1);
                            for (uint i = 0; i <= q.k; i++)
                                p.Add(q.x + i);
                        }
                        break;
                    case 'V':
                        if (q.y <= y + 1 && q.y + q.k >= y)
                        {
                            p.Add(q.x - 1);
                            p.Add(q.x);
                            p.Add(q.x + 1);
                        }
                        break;
                }
            }

            v += Count(p, k);
        }

        for (uint x = 1; x <= m; x++)
        {
            List<uint> p = new List<uint> { 0, n + 1 };
            foreach (Ship q in s)
            {
                switch (q.t)
                {
                    case 'H':
                        if (q.x <= x + 1 && q.x + q.k >= x)
                        {
                            p.Add(q.y - 1);
                            p.Add(q.y);
                            p.Add(q.y + 1);
                        }
                        break;
                    case 'V':
                        if (q.x >= x - 1 && q.x <= x + 1)
                        {
                            p.Add(q.y - 1);
                            for (uint i = 0; i <= q.k; i++)
                                p.Add(q.y + i);
                        }
                        break;
                }
            }

            v += Count(p, k);
        }

        return k == 1 ? v / 2 : v;
    }

    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        uint n = uint.Parse(input[0]);
        uint m = uint.Parse(input[1]);
        uint q = uint.Parse(input[2]);

        List<Ship> s = new List<Ship>();
        for (int i = 0; i < q; i++)
        {
            input = Console.ReadLine().Split();
            Ship ship = new Ship
            {
                x = uint.Parse(input[0]),
                y = uint.Parse(input[1]),
                k = uint.Parse(input[2]),
                t = char.Parse(input[3])
            };
            s.Add(ship);
        }

        uint k = uint.Parse(Console.ReadLine());

        Console.WriteLine(Solve(n, m, s, k));
    }
}
