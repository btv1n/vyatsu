using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seven
{
    // Описание сотрудника
    internal class Employee
    {
        // Номер работника
        public int idEmployee { get; set; }

        // Количество договоров
        public int contractsCount { get; set; }

        // Сумма договоров
        public int sumFromContracts { get; set; }

        // Количество жалоб
        public int complains { get; set; }

        // Количество опозданий
        public int tardiness { get; set; }

        // Итоговый рейтинг
        public double rating { get; set; }

        // Премия сотрудника
        public double bonus { get; set; }
    }
}
