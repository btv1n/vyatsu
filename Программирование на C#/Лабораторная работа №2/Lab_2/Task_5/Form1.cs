using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Task_2._5
{
    public partial class Form1 : Form
    {   
        // объявление
        Graphics Graph;
        Pen MyPen;
        int x, y;
        int xFirst, yFirst;
        bool draw;
        bool first;

        public Form1()
        {
            // инициализация
            InitializeComponent();
            Graph = CreateGraphics();
            MyPen = new Pen(Color.Black);
            draw = false;
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            draw = !draw;
            if (draw)
            {
                xFirst = e.X; // координата х первой точки
                yFirst = e.Y; // координата y первой точки
                first = true; // первая точка поставлена
            }
            else
            {
                Graph.DrawLine(MyPen, x, y, e.X, e.Y); // соединяем предпоследнию точку с последней
                Graph.DrawLine(MyPen, xFirst, yFirst, e.X, e.Y); // соединяем первую точку с последней
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (first && draw)
            {
                Graph.DrawLine(MyPen, xFirst, yFirst, e.X, e.Y);
                x = e.X;
                y = e.Y;
                first = !first;

            }
            else if (draw)
            {
                Graph.DrawLine(MyPen, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose();
            Graph.Dispose();
        }
    }
}
