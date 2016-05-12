using CPUScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FCFS 
            double waitingtime;
            List<process> FCFS = new List<process>();
            string[] allLines = textBox1.Text.Split('\n');
            foreach (string text in allLines)
            {
                string[] allparams = text.Split(',');
                FCFS.Add(new process(allparams[0], Convert.ToDouble(allparams[1]), Convert.ToDouble(allparams[2])));
                
            }
             LinkedList<process> output = Algorithms.FCFS(FCFS ,out waitingtime);
            Console.WriteLine(waitingtime);
            Form2 Chart = new Form2(waitingtime,output);
            Chart.Show();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RR 
            double waitingtime;
            double q = Convert.ToDouble(textBox3.Text);
            List<process> RR = new List<process>();
            string[] allLines = textBox2.Text.Split('\n');
            foreach (string text in allLines)
            {
                string[] allparams = text.Split(',');
                RR.Add(new process(allparams[0], Convert.ToDouble(allparams[1]), Convert.ToDouble(allparams[2])));

            }
            LinkedList<process> output = Algorithms.RoundRobin(q,RR, out waitingtime);
            Console.WriteLine(waitingtime);
            Form2 Chart = new Form2(waitingtime, output);
            Chart.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SJF 
            double waitingtime;
            List<process> SJF = new List<process>();
            string[] allLines = textBox4.Text.Split('\n');
            foreach (string text in allLines)
            {
                string[] allparams = text.Split(',');
                SJF.Add(new process(allparams[0], Convert.ToDouble(allparams[1]), Convert.ToDouble(allparams[2])));

            }
            LinkedList<process> output = new LinkedList<process>();
            if (radioButton1.Checked) { 
                output = Algorithms.Preemptive_SJF(SJF, out waitingtime);
                Console.WriteLine(waitingtime);
                Form2 Chart = new Form2(waitingtime, output);
                Chart.Show();
            }
            else if(radioButton2.Checked)
            {
                output = Algorithms.NonPreemptive_SJF( SJF, out waitingtime);
                Console.WriteLine(waitingtime);
                Form2 Chart = new Form2(waitingtime, output);
                Chart.Show();
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Priority 
            double waitingtime;
            List<process> Priority = new List<process>();
            string[] allLines = textBox5.Text.Split('\n');
            foreach (string text in allLines)
            {
                string[] allparams = text.Split(',');
                Priority.Add(new process(allparams[0], Convert.ToDouble(allparams[1]), Convert.ToDouble(allparams[2]), Convert.ToInt32(allparams[3])));

            }
            LinkedList<process> output = new LinkedList<process>();
            if (radioButton3.Checked)
            {
                output = Algorithms.Preemptive_Priority(Priority, out waitingtime);
                Console.WriteLine(waitingtime);
                Form2 Chart = new Form2(waitingtime, output);
                Chart.Show();
            }
            else if (radioButton4.Checked)
            {
                output = Algorithms.NonPreemptive_Priority(Priority, out waitingtime);
                Console.WriteLine(waitingtime);
                Form2 Chart = new Form2(waitingtime, output);
                Chart.Show();
            }
            
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
