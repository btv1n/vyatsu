using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2._4
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Pen Pen;
        int x1, y1;
        int x2, y2;
        int state = 0; // состояние

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Pen = new Pen(Color.Black);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case 0: 
                    x1 = e.X; 
                    y1 = e.Y; 
                    break;

                case 1:
                    x2 = e.X; 
                    y2 = e.Y;
                    Graph.DrawLine(Pen, x1, y1, x2, y2); // рисует линию между кликом #1 и кликом #2
                    break;

                case 2:
                    Graph.DrawLine(Pen, x1, y1, e.X, e.Y); // рисует линию между кликом #1 и кликом #2
                    Graph.DrawLine(Pen, x2, y2, e.X, e.Y); // рисует линию между кликом #2 и кликом #3
                    break;
            }

            state = (state + 1) % 3; // определяет три нажатия
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose();
            Pen.Dispose();
        }
    }
}
