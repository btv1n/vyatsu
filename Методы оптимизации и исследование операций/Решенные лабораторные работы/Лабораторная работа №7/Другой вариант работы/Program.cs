using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace seven
{
    class Program
    {
        static void Main(string[] args)
        {
            // ШАГ 1
            // Исходные данные: список из 10 сотрудников
            // 4 критерия:
            //   contractsCount   - количество договоров   (чем больше, тем лучше)
            //   sumFromContracts - сумма договоров, руб.  (чем больше, тем лучше)
            //   complains        - количество жалоб       (чем меньше, тем лучше)
            //   tardiness        - количество опозданий   (чем меньше, тем лучше)
            // Поля rating и bonus пока равны 0
            List<Employee> employees = new List<Employee>
            {
            new Employee { idEmployee = 1, contractsCount = 3, sumFromContracts = 42783, complains = 9, tardiness = 9 },
            new Employee { idEmployee = 2, contractsCount = 11, sumFromContracts = 33792, complains = 11, tardiness = 10 },
            new Employee { idEmployee = 3, contractsCount = 24, sumFromContracts = 44558, complains = 8, tardiness = 3 },
            new Employee { idEmployee = 4, contractsCount = 6, sumFromContracts = 17295, complains = 6, tardiness = 12 },
            new Employee { idEmployee = 5, contractsCount = 13, sumFromContracts = 42543, complains = 15, tardiness = 13 },
            new Employee { idEmployee = 6, contractsCount = 26, sumFromContracts = 29363, complains = 10, tardiness = 3 },
            new Employee { idEmployee = 7, contractsCount = 9, sumFromContracts = 27199, complains = 10, tardiness = 7 },
            new Employee { idEmployee = 8, contractsCount = 23, sumFromContracts = 4036, complains = 13, tardiness = 4 },
            new Employee { idEmployee = 9, contractsCount = 18, sumFromContracts = 8432, complains = 2, tardiness = 7 },
            new Employee { idEmployee = 10, contractsCount = 21, sumFromContracts = 12566, complains = 3, tardiness = 4 }
            };

            // ШАГ 2
            // Веса критериев: показывают, насколько важен каждый критерий
            // Они получены из опроса экспертов: вес критерия = сумма его оценок / сумма всех оценок
            // Сумма весов должна равняться 1
            double weightContracts = 0.29;   // количество договоров
            double weightSum = 0.30;         // сумма договоров
            double weightComplaints = 0.23;  // жалобы
            double weightTardiness = 0.18;   // опоздания

            // ШАГ 3
            // Находим максимум по каждому критерию среди всех сотрудников
            // int.MinValue - самое маленькое число int
            int maxContracts = int.MinValue;
            int maxSum = int.MinValue;
            int maxComplaints = int.MinValue;
            int maxTardiness = int.MinValue;

            // Проходим по каждому сотруднику, если его значение больше текущего максимума по критерию, запоминаем его как новый максимум
            // Должно получится: maxContracts = 26, maxSum = 44558, maxComplaints = 15, maxTardiness = 13.
            foreach (var emp in employees)
            {
                if (emp.contractsCount > maxContracts) maxContracts = emp.contractsCount;
                if (emp.sumFromContracts > maxSum) maxSum = emp.sumFromContracts;
                if (emp.complains > maxComplaints) maxComplaints = emp.complains;
                if (emp.tardiness > maxTardiness) maxTardiness = emp.tardiness;
            }

            // ШАГ 4
            // Считаем рейтинг каждого сотрудника (способ 2: нормировка на максимум)
            double totalRating = 0;   // сумма рейтингов всех сотрудников
            foreach (var emp in employees)
            {
                // Критерии, где больше = лучше: делим на максимум (x / max)
                // (double) - чтобы деление было дробным
                double contractsNorm = (double)emp.contractsCount / maxContracts;
                double sumNorm = (double)emp.sumFromContracts / maxSum;

                // Критерии, где меньше = лучше: считаем 1 - x / max.
                double complaintsNorm = 1 - ((double)emp.complains / maxComplaints);
                double tardinessNorm = 1 - ((double)emp.tardiness / maxTardiness);

                // Итоговый рейтинг - сумма нормированных значений: каждое значение умножаем на вес своего критерия и складываем
                // Чем больше рейтинг, тем лучше сотрудник
                emp.rating = contractsNorm * weightContracts +
                             sumNorm * weightSum +
                             complaintsNorm * weightComplaints +
                             tardinessNorm * weightTardiness;

                totalRating += emp.rating;
            }

            // ШАГ 5
            // Распределяем премиальный фонд пропорционально рейтингу.
            // Доля сотрудника = его рейтинг / сумма рейтингов всех сотрудников
            // Премия = доля * весь фонд
            double bonusFund = 100000;   // общий премиальный фонд
            foreach (var emp in employees)
            {
                emp.bonus = emp.rating / totalRating * bonusFund;
            }

            // ШАГ 6
            // Сортируем сотрудников от лучшего к худшему (по убыванию рейтинга).
            // В сравнении (x, y) поменяны местами: y.rating.CompareTo(x.rating) - это даёт сортировку по убыванию, а не по возрастанию
            employees.Sort((x, y) => y.rating.CompareTo(x.rating));

            // ШАГ 7
            // Выводим результат
            double sum = 0;
            Console.WriteLine("ID\tРейтинг\t\tПремия");
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.idEmployee}\t{emp.rating:F2}\t\t{emp.bonus:F2}");
                sum += emp.bonus; // суммируем все премии для проверки
            }

            // Сумма всех премий должна быт = 100000
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}