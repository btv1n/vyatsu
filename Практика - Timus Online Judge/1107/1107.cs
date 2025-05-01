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
    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] parts;
            int n, k, m;

            string str = Console.ReadLine();
            parts = str.Split(' ');

            n = Convert.ToInt32(parts[0].Trim());
            k = Convert.ToInt32(parts[1].Trim());
            m = Convert.ToInt32(parts[2].Trim());

            //for (int k = 0; k < parts.Length; k++)
            //{
            //    Console.WriteLine(parts[k]);
            //}


            Console.WriteLine("YES");

            for (uint i = 0; i < k; ++i)
            {
                str = Console.ReadLine();
                parts = str.Split(' ');

                int q = Convert.ToInt32(parts[0].Trim());

                int s = 0;
                for (uint j = 1; j <= q; ++j)
                {
                    int x = Convert.ToInt32(parts[j].Trim());
                    s += x;
                }

                Console.WriteLine(1 + s % m);
            }
        }
    }
}
