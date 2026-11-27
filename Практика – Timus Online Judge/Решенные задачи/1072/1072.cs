using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneWordSearch
{
    class Program
    {
        // Объявление глобальных переменных 
        static int n, k, start, end_;
        static List<List<string>> inter = new List<List<string>>();
        static List<List<int>> A = new List<List<int>>();
        static List<int> D = new List<int>();
        static List<int> P = new List<int>();
        static Queue<int> q = new Queue<int>();
        static Stack<int> path = new Stack<int>();

        // Функция для преобразования числа в двоичное представление
        static void trans(ref string s, int n, int pos)
        {
            for (int i = 128; i >= 1; i /= 2)
            {
                s = s.Insert(pos++, (n / i).ToString());
                n %= i;
            }
        }

        // Функция для выполнения операции побитового "и" над двумя строками
        static void AND(ref string s, string m)
        {
            for (int i = m.Length - 1; i >= 0; i--)
            {
                if (m[i] == '0')
                    s = s.Remove(i, 1).Insert(i, "0");
                else
                    return;
            }
        }

        // Функция для ввода данных
        static void Input()
        {
            for (int i = 0; i < n; i++)
            {
                k = Convert.ToInt32(Console.ReadLine());
                for (int j = 0; j < k; j++)
                {
                    string s = "00000000000000000000000000000000";
                    string m = "00000000000000000000000000000000";

                    string[] parts; // Trim() - обрезает пробелы по краям
                    string input;
                    int part_1, part_2, part_3, part_4, part_5, part_6, part_7, part_8;
                    //int t;

                    input = Console.ReadLine();
                    parts = input.Split('.', ' ');

                    //for (int k = 0; k < parts.Length; k++)
                    //{
                    //    Console.WriteLine(parts[k]);
                    //}

                    part_1 = Convert.ToInt32(parts[0].Trim());
                    part_2 = Convert.ToInt32(parts[1].Trim());
                    part_3 = Convert.ToInt32(parts[2].Trim());
                    part_4 = Convert.ToInt32(parts[3].Trim());
                    part_5 = Convert.ToInt32(parts[4].Trim());
                    part_6 = Convert.ToInt32(parts[5].Trim());
                    part_7 = Convert.ToInt32(parts[6].Trim());
                    part_8 = Convert.ToInt32(parts[7].Trim());
                    trans(ref s, part_1, 0);
                    trans(ref s, part_2, 8);
                    trans(ref s, part_3, 16);
                    trans(ref s, part_4, 24);
                    trans(ref m, part_5, 0);
                    trans(ref m, part_6, 8);
                    trans(ref m, part_7, 16);
                    trans(ref m, part_8, 24);

                    AND(ref s, m);

                    inter[i].Add(s);

                    for (int x = i - 1; x >= 0; x--)
                    {
                        for (int l = 0; l < inter[x].Count; l++)
                        {
                            if (s == inter[x][l])
                            {
                                A[i].Add(x);
                                A[x].Add(i);
                            }
                        }
                    }

                }
            }
            string[] inputs = Console.ReadLine().Split(' ');
            start = Convert.ToInt32(inputs[0]) - 1;
            end_ = Convert.ToInt32(inputs[1]) - 1;
        }

        // Функция BFS для поиска кратчайшего пути в графе
        static void BFS(int u)
        {
            D[u] = 1;
            q.Enqueue(u);
            while (q.Count > 0)
            {
                u = q.Dequeue();
                for (int v = 0; v < A[u].Count; v++)
                {
                    if (D[A[u][v]] == 0)
                    {
                        D[A[u][v]] = D[u] + 1;
                        q.Enqueue(A[u][v]);
                        P[A[u][v]] = u;
                    }
                }
            }
        }

        // Функция восстановления пути
        static void ReturnPath()
        {
            int u = end_;
            path.Push(u);
            do
            {
                u = P[u];
                path.Push(u);
            } while (u != start);
        }

        static void Main()
        {
            n = Convert.ToInt32(Console.ReadLine());

            // Инициализация / выделение памяти для векторов
            for (int i = 0; i < n; i++)
            {
                inter.Add(new List<string>());
                A.Add(new List<int>());
                D.Add(0);
                P.Add(0);
            }

            Input();

            BFS(start);

            if (D[end_] > 0)
            {
                ReturnPath();
                Console.WriteLine("Yes");
                foreach (int node in path)
                {
                    //Console.Write($"{path.Peek() + 1} ");
                    //path.Pop();
                    Console.Write($"{node + 1} ");
                }
            }
            else // если нет пути к конечному узлу
            {
                Console.WriteLine("No");
            }

            // string iiu = Console.ReadLine();
        }
    }
}
