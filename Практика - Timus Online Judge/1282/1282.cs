using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

class Node
{
    public List<int> subnodes = new List<int>();
    public int value = -1;
}

class Program
{
    static int Solve(List<Node> g, int x, int m)
    {
        if (g[x].subnodes.Count == 0)
            return g[x-1].value;

        int value = -m;
        foreach (int v in g[x].subnodes)
        {
            int t = Solve(g, v, -m);
            value = m < 0 ? Math.Min(value, t) : Math.Max(value, t);
        }
        return value;
    }

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        List<Node> g = new List<Node>();
        for (int i = 1; i < n; ++i)
        {
            string[] input = Console.ReadLine().Split();
            char t = char.Parse(input[0]);
            int p = int.Parse(input[1]);

            if (t == 'L')
                g.Add(new Node() { value = int.Parse(input[2]) });
            else
                g.Add(new Node());

            g[p - 1].subnodes.Add(i);
        }

        string[] s = { "-1", "0", "+1" };
        g.Add(new Node());
        Console.WriteLine(s[Solve(g, 0, 1) + 1]);
        string str = Console.ReadLine();
    }
}
