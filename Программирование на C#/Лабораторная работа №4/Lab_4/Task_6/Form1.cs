using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

// Задача не решена. Реализовать: скорость, количество, закрытие предложения при нажатии с предупреждающим сообщением

/*
4.6.	Создайте приложение с заставкой. Приложение должно состоять из двух форм. На первой форме появляется сообщение о 
создателе приложения и о том, что при двойном щелчке по форме появится заставка, а также пользователю предлагается задать некоторые параметры заставки:
–	вид отображаемой фигуры (прямоугольники / эллипсы / прямоуголь¬ни¬ки и эллипсы в произвольном порядке),
–	количество одновременно отображаемых фигур (одна / две),
–	скорость смены картинки на заставке (медленная / средняя / быстрая).
Окно заставки темно синего цвета, без заголовка, занимает весь экран (изучите свойство формы FormBorderStyle, а также используйте свойство WindowState). 
В окне заставки в произвольном месте формы должны появляться фигуры, заданные пользователем в параметрах заставки. Приложение (обе формы) должно закрываться 
при нажатии любой клавиши клавиатуры, предварительно выдав сообщение, уточняющее, точно ли пользователь хочет закрыть приложение.
*/

namespace Task_6
{
    public partial class Form1 : Form
    {
        Graphics graph;
        Pen pen;
        SolidBrush brush;
        Random rand;
        Point p1, p2, p3;

        Form2 picture = new Form2();

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

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            int randomNumber = rand.Next(1, 3);

            // вид отображаемой фигуры
            int form = 0;
            if (radioButton1.Checked) form = 0;
            else if (radioButton2.Checked) form = 1;
            else if (radioButton3.Checked) form = 2;

            // количество одновременно отображаемых фигур
            int amount = 0;
            if (radioButton4.Checked) amount = 1;
            else if (radioButton5.Checked) amount = 2;

            // скорость смены картинки на заставке
            int speed;
            if (radioButton5.Checked) speed = 0;
            else if (radioButton6.Checked) speed = 1;
            else if (radioButton7.Checked) speed = 2;

            //
            pen.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            pen.Width = rand.Next(1, 20);
            brush.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            p1.X = rand.Next(1, this.ClientSize.Width);
            p1.Y = rand.Next(1, this.ClientSize.Height);
            p2.X = rand.Next(1, this.ClientSize.Width);
            p2.Y = rand.Next(1, this.ClientSize.Height);
            p3.X = rand.Next(1, this.ClientSize.Width);
            p3.Y = rand.Next(1, this.ClientSize.Height);
            uint width = (uint)rand.Next(1, Math.Abs(this.ClientSize.Width - p1.X));
            uint height = (uint)rand.Next(1, Math.Abs(this.ClientSize.Height - p1.Y));
            uint size = (uint)rand.Next(1, Math.Min(
                Math.Abs(this.ClientSize.Width - p1.X),
                Math.Abs(this.ClientSize.Height - p1.Y)
                ));
            //

            if (amount == 1)
            {
                if (form == 0)
                {
                    DrawSquare_(p1, size);
                }    
                else if (form == 1)
                {
                    DrawEllipse_(p1, width, height);
                }  
                else if (form == 2)
                {
                    randomNumber = rand.Next(1, 3);

                    if (randomNumber == 1)
                    {
                        // new random
                        p1.X = rand.Next(1, this.ClientSize.Width);
                        p1.Y = rand.Next(1, this.ClientSize.Height);
                        size = (uint)rand.Next(1, Math.Min(
                        Math.Abs(this.ClientSize.Width - p1.X),
                        Math.Abs(this.ClientSize.Height - p1.Y)
                        ));

                        DrawSquare_(p1, size);
                    }
                    else if (randomNumber == 2)
                    {
                        // new random
                        p1.X = rand.Next(1, this.ClientSize.Width);
                        p1.Y = rand.Next(1, this.ClientSize.Height);
                        width = (uint)rand.Next(1, Math.Abs(this.ClientSize.Width - p1.X));
                        height = (uint)rand.Next(1, Math.Abs(this.ClientSize.Height - p1.Y));

                        DrawEllipse_(p1, width, height);
                    }
                }
            }
            else if (amount == 2)
            {
                if (form == 0)
                {
                    DrawSquare_(p1, size);

                    // new random
                    p1.X = rand.Next(1, this.ClientSize.Width);
                    p1.Y = rand.Next(1, this.ClientSize.Height);
                    size = (uint)rand.Next(1, Math.Min(
                    Math.Abs(this.ClientSize.Width - p1.X),
                    Math.Abs(this.ClientSize.Height - p1.Y)
                    ));

                    DrawSquare_(p1, size);
                } 
                else if (form == 1)
                {
                    DrawEllipse_(p1, width, height);

                    // new random
                    p1.X = rand.Next(1, this.ClientSize.Width);
                    p1.Y = rand.Next(1, this.ClientSize.Height);
                    width = (uint)rand.Next(1, Math.Abs(this.ClientSize.Width - p1.X));
                    height = (uint)rand.Next(1, Math.Abs(this.ClientSize.Height - p1.Y));

                    DrawEllipse_(p1, width, height);
                }   
                else if (form == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        randomNumber = rand.Next(1, 3);

                        if (randomNumber == 1)
                        {
                            // new random
                            p1.X = rand.Next(1, this.ClientSize.Width);
                            p1.Y = rand.Next(1, this.ClientSize.Height);
                            size = (uint)rand.Next(1, Math.Min(
                            Math.Abs(this.ClientSize.Width - p1.X),
                            Math.Abs(this.ClientSize.Height - p1.Y)
                            ));

                            DrawSquare_(p1, size);
                        }
                        else  if (randomNumber == 2)
                        {
                            // new random
                            p1.X = rand.Next(1, this.ClientSize.Width);
                            p1.Y = rand.Next(1, this.ClientSize.Height);
                            width = (uint)rand.Next(1, Math.Abs(this.ClientSize.Width - p1.X));
                            height = (uint)rand.Next(1, Math.Abs(this.ClientSize.Height - p1.Y));

                            DrawEllipse_(p1, width, height);
                        }
                    }
                }      
            }



            /*
            switch (e.KeyData)
            {
                case Keys.OemQuotes: // эллипс
                    {


                    }
                    break;
                case Keys.R: // квадрат
                    {

                        DrawSquare(p1, size);
                    }
                    break;
                case Keys.N: // треугольник
                    {

                    }
                    break;
                case Keys.Escape:
                    {
                        DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
            }
            */

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DrawEllipse_(Point p1, uint width, uint height)
        {
            picture.Show();
            picture.DrawEllipse(pen, brush, p1, width, height);
        }
        private void DrawSquare_(Point p1, uint size)
        {
            picture.Show();
            picture.DrawSquare(pen, brush, p1, size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Создатель предложения. " +
                "При двойном щелчке по форме появится заставка. " +
                "Выберите параметры.");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DrawTriangle_(Point p1, Point p2, Point p3)
        {
            picture.Show();
            picture.DrawTriangle(pen, brush, p1, p2, p3);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close(); // закрыть форму
            picture.Close();
        }
    }
}
