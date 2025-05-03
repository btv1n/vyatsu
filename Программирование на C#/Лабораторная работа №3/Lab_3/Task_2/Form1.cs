using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
3.2.	Напишите программу отображения графика функции y=tg(x). Введите два коэффициента масштабирования: 
kх – коэффициент масштабирования по оси х и ky – коэффициент масштабирования по оси у. При нажатии на клавиши 
управления курсором Вверх/Вниз – увеличивается/уменьшается значение коэффициента kx, при нажатии на клавиши 
Влево/Вправо – увеличивается/уменьшается значение коэффициента ky, а график перерисовывается.
*/

// Количество веток зависит от текущего масштаба

namespace Task_3._2
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Pen Pen;
        SolidBrush Brush;
        Font Font;

        // const int xMin = -10;
        // const int xMax = 10;
        const double step = 0.01;
        const int ArrowSize = 5;

        int kx = 20; 
        int ky = 5;
        
        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Pen = new Pen(Color.Black);
            Brush = new SolidBrush(Color.Black);
            Font = new Font("Arial", 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Метод для отрисовки графика
        private void DrawGraph(int kx, int ky)
        {
            int x0 = this.ClientSize.Width / 2; // координата x центра экрана
            int y0 = this.ClientSize.Height / 2; // координата y центра экрана
            int xMax = this.ClientSize.Width; // максимальное значение x
            int xMin = -xMax; // минимальное значение x

            Graph.DrawLine(Pen, 0, y0, x0 * 2, y0); // отрисовка оси OX
            Graph.DrawLine(Pen, x0, 0, x0, y0 * 2); // отрисовка оси OY

            // Отрисовка стрелочек на оси OX
            Graph.DrawLine(Pen, x0 * 2, y0, x0 * 2 - ArrowSize, y0 + ArrowSize);
            Graph.DrawLine(Pen, x0 * 2, y0, x0 * 2 - ArrowSize, y0 - ArrowSize);
            Graph.DrawString("X", Font, Brush, x0 * 2 - 13, y0 + 10); // добавление текста "X" возле стрелочки на оси OX

            // Отрисовка стрелочек на оси OY
            Graph.DrawLine(Pen, x0, 0, x0 - ArrowSize, ArrowSize);
            Graph.DrawLine(Pen, x0, 0, x0 + ArrowSize, ArrowSize);
            Graph.DrawString("Y", Font, Brush, x0 + 5, 0); // Добавление текста "Y" возле стрелочки на оси OY



            int x1, y1, x2, y2;
            double x, y;
            x = xMin;
            y = Math.Tan(x);
            x1 = (int)(x0 + x * kx);
            y1 = (int)(y0 - y * ky);

            // Отрисовка графика функции
            while (x < xMax) 
            {
                x += step;
                y = Math.Tan(x);

                x2 = (int)(x0 + x * kx); 
                y2 = (int)(y0 - y * ky);

                // Отрисовка линии графика только если он не пересекает ось OX
                if (!(y2 > y0 && y1 < y0)) 
                    Graph.DrawLine(Pen, x1, y1, x2, y2);

                x1 = x2; 
                y1 = y2;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Graph.Clear(Color.White);
            if (e.KeyCode == Keys.Right) ky--;
            if (e.KeyCode == Keys.Left) ky++;
            if (e.KeyCode == Keys.Up) kx++; 
            if (e.KeyCode == Keys.Down) kx--; 
            DrawGraph(kx, ky);
        }
    }
}