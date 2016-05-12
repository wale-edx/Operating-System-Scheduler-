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
    public partial class Form2 : Form
    {
       
        
        LinkedList<process> FinalList;
       
        private void DrawIt(LinkedList<process> Final)
        {
            

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Width = 80* FinalList.Count;
            panel1.AutoScroll = true;
            int x = 0;
            int y = 0;
            int x2 = 25;
            int y2 = 25;
            foreach (process process in FinalList)
            {
                string drawString = process.getName();
                Console.WriteLine(drawString);
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                System.Drawing.Graphics graphics = panel1.CreateGraphics();
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(x, y, 80, 80);
                graphics.DrawString(drawString, drawFont, drawBrush, x2, y2);
                x = x + 80;
                x2 = x2 + 80;
                graphics.DrawRectangle(System.Drawing.Pens.Black, rectangle);
            }
           

        }
        public Form2(double waitingtime , LinkedList<process> Final)
        {
            InitializeComponent();
           
            label1.Text = "Average Waiting Time is : "+waitingtime.ToString()+" ms" ;
            FinalList = Final;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
           
        }
        
      
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void panel1_Resize(object sender, EventArgs e)
        {
            //panel1.Paint += new PaintEventHandler(panel1_Paint);

        }
    }
}
