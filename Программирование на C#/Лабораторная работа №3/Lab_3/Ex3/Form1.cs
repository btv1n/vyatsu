using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex3
{
    public partial class MainForm : Form
    {
        Graphics Graph;
        Pen MyPen;

        public MainForm()
        {
            InitializeComponent();

            Graph = CreateGraphics();
            MyPen = new Pen(Color.Black);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose();
            Graph.Dispose();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int x0 = this.ClientSize.Width / 2;
            int y0 = this.ClientSize.Height / 2;
            int x1, y1, x2, y2;

            double x, y;

            const double xMin = -5;
            const double xMax = 5;
            const double step = 0.01;
            const double k = 5.5;

            if (e.KeyCode == Keys.Enter)
            {
                //отображение графика на форме

                //фактические координаты в начальной точке заданного диапазона
                x = xMin;
                y = -x * x + 3;
                //соответствующие им экранные координаты
                x1 = (int)(x0 + x * k);
                y1 = (int)(y0 - y * k);
                while (x < xMax)
                {
                    //определение фактических координат графика в следующей точке
                    x = x + step;
                    y = -x * x + 3;
                    //соответствующие им экранные координаты
                    x2 = (int)(x0 + x * k);
                    y2 = (int)(y0 - y * k);
                    //вывод отрезка графика на экран
                    Graph.DrawLine(MyPen, x1, y1, x2, y2);
                    //запоминаем текущие координаты
                    x1 = x2;
                    y1 = y2;
                }
            }
        }
    }
}
