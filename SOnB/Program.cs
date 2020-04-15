using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOnB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj pierwszą liczbę");
            int firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj drugą liczbę");
            int secondNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj podstawę kodu residualnego");
            int codeNumber = Convert.ToInt32(Console.ReadLine());


            firstNumber = firstNumber & 15;
            secondNumber = secondNumber & 15;

            Console.WriteLine("{0} * {1}", firstNumber, secondNumber);

            MultiplierSystem multiplierSystem1 = new MultiplierSystem();
            MultiplierSystem multiplierSystem2 = new MultiplierSystem();

            int result1 = multiplierSystem1.MultiplyWithCheck(firstNumber, secondNumber, codeNumber);
            int result2= multiplierSystem2.MultiplyWithCheck(firstNumber, secondNumber, codeNumber);
            
            if (result1 != -1 && result2 == -1)
            {
                Console.WriteLine("Wynik to {0}, ale układ 2 nie jest sprawny", result1);

            }
            else if (result1 == -1 && result2 != -1)
            {
                Console.WriteLine("Wynik to {0}, ale układ 1 nie jest sprawny", result2);
            }
            else if(result1 == -1 && result2 == -1)
            {
                Console.WriteLine("Coś poszło nie tak, wynik1 = {0} , wynik2 = {1}", result1, result2);
            }
            else if (result1 == result2)
            {
                Console.WriteLine("Wynik to {0}, oba układy sprawne", result1);
            }

            Console.ReadKey();

        }
    }
}
