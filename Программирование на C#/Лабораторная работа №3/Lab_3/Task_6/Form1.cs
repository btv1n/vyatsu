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
 * 3.6.	Создайте приложение с двумя формами, в котором на первой (главной) форме выводится 
 * сообщение «Нажми ‘э’, чтобы увидеть эллипс, ‘к’, чтобы увидеть квадрат, и ‘т’, чтобы увидеть 
 * треугольник. Для закрытия приложения нажми Esc». При нажатии на клавиши ‘к’, ‘п’ или ‘т’ 
 * открывается другая форма, на которой изображена указанная фигура в произвольном месте, 
 * произвольного размера, цвета заливки, а также толщины и цвета контура, а в заголовке 
 * формы указано название фигуры. При нажатии на клавишу Esc по главной форме, приложение 
 * закрывается, предварительно выдав сообщение, уточняющее, точно ли пользователь хочет 
 * закрыть приложение.
*/

namespace Task_3._6
{
    public partial class Form1 : Form
    {
        Graphics graph;
        Pen pen;
        SolidBrush brush;
        Random rand;
        Point p1, p2, p3;

        public Form1()
        {
            InitializeComponent();
            graph = CreateGraphics();
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            rand = new Random();
            p1 = new Point();
            p2 = new Point();
            p3 = new Point();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pen.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            pen.Width = rand.Next(1, 20);
            brush.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

            p1.X = rand.Next(1, this.ClientSize.Width);
            p1.Y = rand.Next(1, this.ClientSize.Height);
            p2.X = rand.Next(1, this.ClientSize.Width);
            p2.Y = rand.Next(1, this.ClientSize.Height);
            p3.X = rand.Next(1, this.ClientSize.Width);
            p3.Y = rand.Next(1, this.ClientSize.Height);

            switch (e.KeyData)
            {
                case Keys.OemQuotes: // эллипс
                    {
                        uint width = (uint)rand.Next(1,Math.Abs(this.ClientSize.Width - p1.X));
                        uint height = (uint)rand.Next(1,Math.Abs(this.ClientSize.Height - p1.Y));
                        DrawEllipse(p1, width, height);
                    }
                    break;
                case Keys.R: // квадрат
                    {
                        uint size = (uint)rand.Next(1, Math.Min(
                            Math.Abs(this.ClientSize.Width - p1.X), 
                            Math.Abs(this.ClientSize.Height - p1.Y)
                            ));

                        DrawSquare(p1, size);
                    }
                    break;
                case Keys.N: // треугольник
                    {
                        DrawTriangle(p1, p2, p3);
                    }
                    break;
                case Keys.Escape:
                    {
                        DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Выход",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
            }
        }

        private void DrawEllipse(Point p1, uint width, uint height)
        {
            Form2 ellipse_form = new Form2();
            ellipse_form.Show();
            ellipse_form.Draw(pen, brush, p1, width, height);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DrawSquare(Point p1, uint size)
        {
            Form3 square_form = new Form3();
            square_form.Show();
            square_form.Draw(pen, brush, p1, size);
        }

        private void DrawTriangle(Point p1, Point p2, Point p3)
        {
            Form4 triangle_form = new Form4();
            triangle_form.Show();
            triangle_form.Draw(pen, brush, p1, p2, p3);
        }
    }
}