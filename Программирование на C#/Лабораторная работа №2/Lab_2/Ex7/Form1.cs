using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex7
{
    public partial class Form1 : Form
    {
        int x, y;
        Pen MyPen;
        Graphics Graph;

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            MyPen = new Pen(Color.Blue);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int w = Math.Abs(e.X - x);
            int h = Math.Abs(y - e.Y);
            x = Math.Min(e.X, x);
            y = Math.Min(e.Y, y);
            Graph.DrawRectangle(MyPen, x, y, w, h); // Если метод DrawRectangle() поменять на DrawEllipse(), то вместо прямоугольников будут рисоваться эллипсы.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose(); // Освобождение ресурсов объекта Pen
            Graph.Dispose(); // Освобождение ресурсов объекта Graphics
        }
    }
}
