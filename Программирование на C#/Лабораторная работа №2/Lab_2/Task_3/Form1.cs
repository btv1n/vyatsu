using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2._3
{
    public partial class Form1 : Form
    {
        Random Rand;
        Graphics Graph;
        SolidBrush Brush;
        Pen MyPen;
        int x, y;

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Brush = new SolidBrush(Color.Black);
            MyPen = new Pen(Color.Black);
            Rand = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int w = Math.Abs(e.X - x); // Ширина эллипса
            int h = Math.Abs(y - e.Y); // Высота эллипса

            x = Math.Min(e.X, x);
            y = Math.Min(e.Y, y);

            Color randColor = Color.FromArgb(Rand.Next(256), Rand.Next(256), Rand.Next(256));
            Brush.Color = randColor;

            MyPen.Color = Color.FromArgb(Rand.Next(256), Rand.Next(256), Rand.Next(256));
            MyPen.Width = Rand.Next(20) + 2;

            Graph.DrawEllipse(MyPen, x, y, w, h); // Рисует эллипс
            Graph.FillEllipse(Brush, x, y, w, h); // Заполняет цветом эллипс
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X; 
            y = e.Y;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose(); 
            MyPen.Dispose(); 
            Brush.Dispose();
        }
    }
}
