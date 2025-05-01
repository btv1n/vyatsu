using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int s, n;
            //s = Convert.ToInt32(Console.ReadLine());
            //n = Convert.ToInt32(Console.ReadLine());
            int[] A = Array.ConvertAll(Console.ReadLine().Split(), int.Parse); // A[0] - s; a[1] - n

            int[] data = new int[A[0]];

            for (int i = 0; i < A[0]; i++)
            {
                data[i] = Convert.ToInt32(Console.ReadLine());
            }

            int sum = 0;
            for (int i = 0; i < A[0]; i++)
            {
                sum += data[i];
                if (sum < (i + 1))
                {
                    Console.WriteLine("NO");
                    return;
                }
            }

            sum = 0;
            for (int i = 0; i < A[0]; i++)
            {
                sum += data[A[0] - 1 - i];
                if (sum < (i + 1))
                {
                    Console.WriteLine("NO");
                    return;
                }
            }

            Console.WriteLine("YES");

            //string input = Console.ReadLine();
        }
    }
}
