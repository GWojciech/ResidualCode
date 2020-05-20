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
                label7.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label7.Text = "Error: " + e.Error.Message;
            }
            else
            {
                button1.Enabled = true;
                label7.Text = "Zakończono działanie!";
                label5.Text = info;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label6.Text = "zmina!";
    
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
            arguments.Add(listBox1.SelectedItem.ToString());
            arguments.Add(textBox2.Text);
            arguments.Add(textBox3.Text);
            arguments.Add(textBox4.Text);

            backgroundWorker1.RunWorkerAsync(arguments);

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
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

            threads[0] = new Thread(delegate () { entranceElement.SendData((string)genericlist[0], (string)genericlist[1], (string)genericlist[2], (string)genericlist[3]); });
            threads[0].Start();
            threads[1] = new Thread(delegate () { info = comparator.getData(); });
            threads[1].Start();

            threads[2] = new Thread(delegate () { multiplierSystem1.getData(1); });
            threads[2].Start();
            threads[2].Join();

            threads[3] = new Thread(delegate () { multiplierSystem2.getData(2); });
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
    }
}