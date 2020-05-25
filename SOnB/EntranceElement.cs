using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SOnB
{
    class EntranceElement
    {
        private int ConvertFromHex(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

        }

        private int ConvertFromBin(string binValue)
        {
            return Convert.ToInt32(binValue, 2);
        }

        private void Serve(int firstNumber, int secondNumber, int moduloString)
        {
            TcpListener tcpLsn;
            int i = 0;
            Socket sckt;
            string text = firstNumber + ";" + secondNumber + ";" + moduloString;
            Byte[] byteData = Encoding.ASCII.GetBytes(text.ToCharArray());
            tcpLsn = new TcpListener(IPAddress.Parse("127.0.0.1"), 2222); 
            tcpLsn.Start();
            while (i < 2)
            {
                sckt = tcpLsn.AcceptSocket();
                sckt.Send(byteData, byteData.Length, 0);
                sckt.Close();
                i++;

            }
            tcpLsn.Stop();
        }

        public void SendData(string typeOfValue, string firstNumberString, string secondNumberString, string moduloString)
        {
            int firstNumber;
            int secondNumber;
            int moduloNumber;

            Console.WriteLine("WARTOŚĆ: " + typeOfValue);

            if (typeOfValue.Equals("DEC"))
            {
                firstNumber = Convert.ToInt32(firstNumberString);
                secondNumber = Convert.ToInt32(secondNumberString);
                moduloNumber = Convert.ToInt32(moduloString);
            }
            else if (typeOfValue.Equals("HEX"))
            {
                firstNumber = ConvertFromHex(firstNumberString);
                secondNumber = ConvertFromHex(secondNumberString);
                moduloNumber = ConvertFromHex(moduloString);
            }
            else
            {
                firstNumber = ConvertFromBin(firstNumberString);
                secondNumber = ConvertFromBin(secondNumberString);
                moduloNumber = ConvertFromBin(moduloString);

            }

            firstNumber = firstNumber & 15;
            secondNumber = secondNumber & 15;

            Serve(firstNumber, secondNumber, moduloNumber);
        }

    }
}