using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Task_2._1
{
    public partial class Form1 : Form
    {
        int x, y;
        float[] dashValues = { 5, 4 }; // Длина пунктира и расстояние между пунктирами
        Pen MyPen;
        Graphics Graph;
        Random Rand;

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            MyPen = new Pen(Color.Black);
            MyPen.DashPattern = dashValues;
            Rand = new Random(); // Инициализация генератора случайных чисел
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MyPen.Width = Rand.Next(10); // Произвольная ширина (Random)
            dashValues[0] = Rand.Next(1, 10); // Произвольная длина пунктира
            dashValues[1] = Rand.Next(1, 10); // Произвольное расстояние между пунктирами
            MyPen.DashPattern = dashValues;

            // Определение трех целых случайных чисел [0..255]
            int a = Rand.Next(256);
            int b = Rand.Next(256);
            int c = Rand.Next(256);
            MyPen.Color = Color.FromArgb(a, b, c);

            x = e.X;
            y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Graph.DrawLine(MyPen, x, y, e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose();
            Graph.Dispose();
        }

        /*
            Pen MyPen = new Pen(Color.Red, 2);
            Graph.DrawLine(MyPen, 25, 25, 250, 250);

            if (e.Button == MouseButtons.Left)
            {
                // Определение трех целых случайных чисел [0..255]
                int a = Rand.Next(256);
                int b = Rand.Next(256);
                int c = Rand.Next(256);
                MyPen.Color = Color.FromArgb(a, b, c);
            }
        */
    }
}
