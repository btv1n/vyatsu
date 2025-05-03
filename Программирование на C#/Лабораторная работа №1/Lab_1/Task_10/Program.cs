using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1.10.	Студент прошел тест, оцениваемый в баллах от нуля до пяти. Вывести на экран оценку,
             * в зависимости от балла: 5 – «отлично», 4 – «хорошо», 3 – «удовлетворительно», 2, 1 или 
             * 0 – «неудовлетворительно» (использовать оператор switch).*/

            Console.Write("Введите кол-во баллов: ");
            int grade = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (grade) // объединить можно 0-2
            {
                case 0:
                    Console.WriteLine("неудовлетворительно");
                    break;
                case 1:
                    Console.WriteLine("неудовлетворительно");
                    break;
                case 2:
                    Console.WriteLine("неудовлетворительно");
                    break;
                case 3:
                    Console.WriteLine("удовлетворительно");
                    break;
                case 4:
                    Console.WriteLine("хорошо");
                    break;
                case 5:
                    Console.WriteLine("отлично");
                    break;
                default:
                    Console.WriteLine("Таких баллов не существует в системе");
                    break;
            }

            Console.WriteLine();
        }
    }
}
