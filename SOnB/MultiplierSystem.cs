using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOnB
{
    class MultiplierSystem
    {
        public int MultiplyWithCheck(int firstNumber, int secondNumber, int modulo, int errorInMultiply = 0, int errorInCheck = 0)
        {
            int result = (firstNumber * secondNumber + errorInMultiply) & 15;
            int moduloResult = result % modulo;

            int checkResult = (firstNumber % modulo) * (secondNumber % modulo) + errorInCheck;
            int checkModuloResult = checkResult % modulo;

            if(moduloResult == checkModuloResult)
            {
                return result;
            }
            else
            {
                return -1;
            }
        }


    }
}
