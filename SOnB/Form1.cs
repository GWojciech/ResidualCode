using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace SOnB
{


    public partial class Form1 : Form
    {


        private string info;
        private int[] errors = { 1, 1, 1, 1 };
        private int [][] resultsFromMultipliersSystem = new int [2][];

        //   Comparator comparator;
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);

        }




        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label7.Visible = true;
                label7.Text = "Przerwano!";
            }
            else if (e.Error != null)
            {
                label7.Visible = true;
                label7.Text = "Błąd: " + e.Error.Message;
            }
            else
            {
                multiplicationEquation1.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][0]) 
                    + " * " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][1]);
                multiplicationEquation2.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][0]) 
                    + " * " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][1]);
                multiplier1Result.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][3]);
                multiplier2Result.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][3]);

                moduloResult1.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][3]) + " % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][2])
                    + "\n?\n((" + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][0])
                    + " % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][2]) + ") * (" + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][1]) + " % "
                    + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][2]) + ")) % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[0][2]);

                moduloResult2.Text = resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][3]) + " % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][2])
                    + "\n?\n((" + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][0])
                    + " % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][2]) + ") * (" + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][1]) + " % "
                    + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][2]) + ")) % " + resultFromSelectedNumberSystem(resultsFromMultipliersSystem[1][2]);

                button1.Enabled = true;
                label7.Text = "Zakończono działanie!";
                label7.Visible = true;
                label14.Text = info;
                label14.Visible = true;
                multiplicationEquation1.Visible = true;
                multiplicationEquation2.Visible = true;
                moduloResult1.Visible = true;
                moduloResult2.Visible = true;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label7.Text = "zmina!";

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            List<object> arguments = new List<object>();


            string selectedSystem = listBox1.SelectedItem.ToString();
            string firstNumberTxt = textBox2.Text;
            string secondNumberTxt = textBox3.Text;
            string moduleTxt = textBox4.Text;
            int multResult;
            int modResult;


            arguments.Add(selectedSystem);
            arguments.Add(firstNumberTxt);
            arguments.Add(secondNumberTxt);
            arguments.Add(moduleTxt);
            arguments.Add(0);
            arguments.Add(0);
            arguments.Add(0);
            arguments.Add(0);
            label8.Text = textBox2.Text;
            label9.Text = textBox3.Text;
            label10.Text = textBox2.Text;
            label11.Text = textBox3.Text;


            if (radioButton1.Checked == false)
            {

                backgroundWorker1.RunWorkerAsync(arguments);


            }
            else
            {
                if (checkedListBox1.SelectedIndex == null)
                {
                }

                if (checkedListBox1.SelectedIndex == 0)
                {
                    arguments[4] = errors[0];
                }
                if (checkedListBox1.SelectedIndex == 1)
                {
                    arguments[5] = errors[1];

                }
                if (checkedListBox1.SelectedIndex == 2)
                {
                    arguments[6] = errors[2];
                  
                }
                if (checkedListBox1.SelectedIndex == 3)
                {
                    arguments[7] = errors[3];
                }



                backgroundWorker1.RunWorkerAsync(arguments);
            }

            multiplier2Result.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            multiplier1Result.Visible = true;
            moduloResult1.Visible = true;
            moduloResult2.Visible = true;
            label14.BringToFront();
            pictureBox1.Visible = true;


        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            MultiplierSystem multiplierSystem1 = new MultiplierSystem();
            MultiplierSystem multiplierSystem2 = new MultiplierSystem();
            EntranceElement entranceElement = new EntranceElement();
            Comparator comparator = new Comparator();
            Thread[] threads = new Thread[4];
            List<object> genericlist = e.Argument as List<object>;



            int firstMultError = (int)genericlist[4];
            int secondMultError = (int)genericlist[5];
            int firstModError = (int)genericlist[6];
            int secondModError = (int)genericlist[7];

            threads[0] = new Thread(delegate () { entranceElement.SendData((string)genericlist[0], (string)genericlist[1], (string)genericlist[2], (string)genericlist[3]); });


            threads[0].Start();
            threads[1] = new Thread(delegate () { info = comparator.getData(); });
            threads[1].Start();

            threads[2] = new Thread(delegate () { resultsFromMultipliersSystem[0] = multiplierSystem1.getData(1, firstMultError, firstModError); });
            threads[2].Start();
            threads[2].Join();

            threads[3] = new Thread(delegate () { resultsFromMultipliersSystem[1] = multiplierSystem2.getData(2, secondMultError, secondModError); });
            threads[3].Start();
            threads[3].Join();

            threads[0].Join();
            threads[1].Join();


        }



        private void bin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-1^]"))
            {
                e.Handled = true;
            }
        }

        private void dec_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^]"))
            {

                e.Handled = true;
            }
        }

        private void hex_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {


            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^+^a-z^+^A-Z^]"))
            {

                e.Handled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button1.Enabled = false;


            var system = listBox1.SelectedItem;
            string mode = system.ToString();

            Console.WriteLine(mode);
            validation(mode);



        }


        private void validation(string mode)
        {
            if (mode == "BIN")
            {
                textBox2.KeyPress += new KeyPressEventHandler(bin_KeyPress);
                textBox3.KeyPress += new KeyPressEventHandler(bin_KeyPress);
                textBox4.KeyPress += new KeyPressEventHandler(bin_KeyPress);
            }
            if (mode == "DEC")
            {
                textBox2.KeyPress += new KeyPressEventHandler(dec_KeyPress);
                textBox3.KeyPress += new KeyPressEventHandler(dec_KeyPress);
                textBox4.KeyPress += new KeyPressEventHandler(dec_KeyPress);

            }
            if (mode == "HEX")
            {
                textBox2.KeyPress += new KeyPressEventHandler(hex_KeyPress);
                textBox3.KeyPress += new KeyPressEventHandler(hex_KeyPress);
                textBox4.KeyPress += new KeyPressEventHandler(hex_KeyPress);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            ifEmpty();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ifEmpty();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            ifEmpty();
        }

        private void ifEmpty()
        {

            button1.Enabled = false;
            if (textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "")
            {
                button1.Enabled = true;
            }
        }


        private string addErrorNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next(0, 100000);
            return number.ToString();
        }

        private int ConvertFromHex(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

        }

        private int ConvertFromBin(string binValue)
        {
            return Convert.ToInt32(binValue, 2);
        }

        public int[] convertToInt(string typeOfValue, string firstNumberString, string secondNumberString, string moduloString)
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



            int[] intArray = new int[] { firstNumber, secondNumber, moduloNumber };

            return intArray;

        }

        public string resultFromSelectedNumberSystem(int number)
        {
            string result;

            if (listBox1.SelectedIndex == 0)
            {
                result = number.ToString("X");
                return result;

            }
            else if (listBox1.SelectedIndex == 1)
            {
                return number.ToString();
            }
            else
            {

                result = Convert.ToString(number, 2);
                return result;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errors[0] > 0)
            {
                errors[0]--;
                label17.Text = resultFromSelectedNumberSystem(errors[0]);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            errors[0]++;
            label17.Text = resultFromSelectedNumberSystem(errors[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (errors[1] > 0)
            {
                errors[1]--;
                label19.Text = resultFromSelectedNumberSystem(errors[1]);
            }
    }

        private void button7_Click(object sender, EventArgs e)
        {

            errors[1]++;
            label19.Text = resultFromSelectedNumberSystem(errors[1]);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (errors[2] > 0)
            {
                errors[2]--;
                label21.Text = resultFromSelectedNumberSystem(errors[2]);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            errors[2]++;
            label21.Text = resultFromSelectedNumberSystem(errors[2]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (errors[3] > 0)
            {
                errors[3]--;
                label23.Text = resultFromSelectedNumberSystem(errors[3]);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            errors[3]++;
            label23.Text = resultFromSelectedNumberSystem(errors[3]); ;
        }
    }
}