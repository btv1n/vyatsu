using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp
{
    public class FunctionCalculator
    {
        public double Calculator(double x)
        {
            if (x == 10)
                throw new ArgumentException("В x = 10 функция не определена.");
                
            return 1 / (10 - x);
        }
    }
}
