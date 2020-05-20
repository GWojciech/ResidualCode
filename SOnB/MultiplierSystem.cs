using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SOnB
{
    class MultiplierSystem
    {
        private static Socket s;
        private int firstNumber;
        private int secondNumber;
        private int modulo;


        private int MultiplyWithCheck(int errorInMultiply = 0, int errorInCheck = 0)
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


        private static void Connect(int port)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse("127.0.0.1");
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            s.Connect(EPhost);
        }

        private void Receive(int number)
        {
            Byte[] receivedBytes = new Byte[100];
            int ret = s.Receive(receivedBytes, receivedBytes.Length, 0);
            string tmp = null;
            tmp = System.Text.Encoding.ASCII.GetString(receivedBytes);
            var values = tmp.Split(';');
            firstNumber = Int32.Parse(values[0]);
            secondNumber = Int32.Parse(values[1]);
            modulo = Int32.Parse(values[2]);
            if (tmp.Length > 0)
            {
                Console.WriteLine("\nUkład mnożący {0}:", number);
                Console.WriteLine("Pierwsza liczba: {0}, druga liczba: {1}, a podstawa kodu {2}\n", firstNumber, secondNumber, modulo);

            }
            s.Close();
        }

        private void Send(string result)
        {
            Byte[] byteData = Encoding.ASCII.GetBytes(result.ToCharArray());
            s.Send(byteData, byteData.Length, 0);
            s.Close();
        }

        public void getData(int number)
        {
            Connect(2222);
            Receive(number);
            Connect(2223);
            int result = MultiplyWithCheck();
            Send(Convert.ToString(result));

        }


    }
}
