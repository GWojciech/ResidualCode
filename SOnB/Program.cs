//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Management.Instrumentation;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace SOnB
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            MultiplierSystem multiplierSystem1 = new MultiplierSystem();
//            MultiplierSystem multiplierSystem2 = new MultiplierSystem();
//            EntranceElement entranceElement = new EntranceElement();
//            Comparator comparator = new Comparator();
//            Thread[] threads = new Thread[4];
//            string mode="";

//            Console.WriteLine("Podaj tryb: DEC, HEX, BIN");
//            while (true)
//            {
//                mode = Console.ReadLine();
//                if(mode.Equals("DEC") || mode.Equals("HEX") || mode.Equals("BIN"))
//                {
//                    break;
//                }
//            }
//            Console.WriteLine("Podaj mnożną");
//            string firstNumber = Console.ReadLine();
//            Console.WriteLine("Podaj mnożnik");
//            string secondNumber = Console.ReadLine();
//            Console.WriteLine("Podaj podstawę kodu");
//            string moduloNumber = Console.ReadLine();

//            threads[0] = new Thread(delegate () { entranceElement.SendData(mode, firstNumber, secondNumber, moduloNumber); });
//            threads[0].Start();
//            threads[1] = new Thread(delegate () { comparator.getData(); });
//            threads[1].Start();

//            threads[2] = new Thread(delegate () { multiplierSystem1.getData(1); });
//            threads[2].Start();
//            threads[2].Join();

//            threads[3] = new Thread(delegate () { multiplierSystem2.getData(2); });
//            threads[3].Start();
//            threads[3].Join();

//            threads[0].Join();
//            threads[1].Join();

//            Console.ReadKey();

//        }
//    }
//}
