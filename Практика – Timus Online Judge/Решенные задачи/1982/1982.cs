using System;

public class Program
{
    static void Main()
    {
        string [] input;
        string str;
        str = Console.ReadLine();
        input = str.Split(' ');

        int n, k;
        n = Convert.ToInt32(input[0].Trim());
        k = Convert.ToInt32(input[1].Trim());

        const int N = 100;
        int[] powerstation = new int[N + 1];
        int[,] graph = new int[N + 1, N + 1];
        int[] vertices = new int[N + 1];
        int[] processed = new int[N + 1];

        for (int i = 0; i <= n; i++)
        {
            powerstation[i] = 0;
            vertices[i] = int.MaxValue;
            processed[i] = 0;
        }

        str = Console.ReadLine();
        input = str.Split(' ');
        for (int i = 0, temp = 0; i < k; i++)
        {
            //temp = Convert.ToInt32(Console.ReadLine());
            temp = Convert.ToInt32(input[i]);
            powerstation[temp] = 1;
        }

        for (int i = 1; i <= n; i++)
        {
            str = Console.ReadLine();
            input = str.Split(' ');
            for (int j = 1; j <= n; j++)
            {
                graph[i, j] = Convert.ToInt32(input[j-1]);
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (powerstation[i] == 1)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (powerstation[j] == 1)
                        graph[i, j] = 0;

                    if (i == j)
                        graph[i, j] = int.MinValue;
                }
            }
        }

        vertices[1] = 0;

        for (int i = 1, temp = int.MaxValue, iter = 0; i < n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (vertices[j] < temp && processed[j] == 0)
                {
                    temp = vertices[j];
                    iter = j;
                }
            }

            processed[iter] = 1;

            for (int j = 1; j <= n; j++)
            {
                if (processed[j] == 0)
                {
                    if (graph[iter, j] >= 0 && graph[iter, j] < vertices[j])
                    {
                        vertices[j] = graph[iter, j];
                    }
                }
            }

            temp = int.MaxValue;
        }

        int u = 0;

        for (int i = 1; i <= n; i++)
        {
            u += vertices[i];
        }

        Console.WriteLine(u);
    }
}
