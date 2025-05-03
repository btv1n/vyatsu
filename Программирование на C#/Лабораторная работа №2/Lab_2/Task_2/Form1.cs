using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2._2
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Pen MyPen;
        const int size = 9;
        double angle = Math.Cos(180 / 8);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            MyPen = new Pen(Color.White); // Цвет линии
            MyPen.Width = 3F; // Толщина линии
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y; // + size;
            // int y1 = y - size;
            
            Graph.TranslateTransform(e.X, e.Y); // Сдвиг центра координат в точку клика

            for (int i = 0; i < 4; i++) // Рисование и поворот линий
            {
                // Данный код рисует вертикальную линию с позиции (0, size) до позиции (0, -size) на графическом элементе, используя указанное перо.
                Graph.DrawLine(MyPen, 0, size, 0, -size); // Используемое перо - координаты начальной точки линии - координаты конечной точки линии
                
                Graph.RotateTransform(45);
            }
          
            Graph.RotateTransform(180); // Возвращение системы координат в первоначальный угол
            
            Graph.TranslateTransform(-e.X, -e.Y); // Смещение центра координат обратно
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose();
            Graph.Dispose();
        }
    }
}
