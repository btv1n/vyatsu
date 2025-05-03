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
 * 3.3.	Напишите такое приложение: при нажатии на клавишу Enter в центре формы появляется 
 * квадрат размера 10×10. Пользователь может управлять его положением с помощью клавиш 
 * управления курсором и изменять его размеры с помощью сочетания клавиш Shift и клавиш 
 * управления курсором. При этом размеры квадрата могут изменяться только в пределах заданного 
 * диапазона (длина стороны может принимать значение из диапазона [1..Min(ширина формы, высота формы) – 10]), 
 * а сам квадрат не может перемещаться за пределы формы. При нажатии на клавишу F1 выводится диалоговое 
 * окно с информацией о создателе программы.
*/

namespace Task_3._3
{
    public partial class Form1 : Form
    {
        Graphics graph;
        SolidBrush brush;
        int window_width, window_height;
        int size;
        int x, y;
        const int step = 10;
        bool started;

        public Form1()
        {
            InitializeComponent();
            graph = CreateGraphics();
            brush = new SolidBrush(Color.Black);
            window_width = this.ClientSize.Width;
            window_height = this.ClientSize.Height;
            size = 10;
            x = window_width / 2 - size / 2;
            y = window_height / 2 - size / 2;
            started = false;
        }

        // Проверяет допустимый размер квадрата
        private bool ValidateSize(int size)
        {
            return size >= 1 && size <= Math.Min(window_height, window_width) - 10;
        }

        // Метод для проверки не выхода за границы окна
        private void StickToBorder()
        {
            if (x < 0)
                x = 0;

            if (x + size > window_width)
                x = window_width - size;

            if (y < 0)
                y = 0;

            if (y + size > window_height)
                y = window_height - size;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Метод для отрисовки квадрата
        private void Draw()
        {
            graph.Clear(Color.White);
            graph.FillRectangle(brush, x, y, size, size);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if (started)
                        {
                            if (e.Shift)
                            {
                                if (ValidateSize(size + 1))
                                    size++;
                            }
                            else
                            {
                                y -= step;
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        if (started)
                        {
                            if (e.Shift)
                            {
                                if (ValidateSize(size - 1))
                                    size--;
                            }
                            else
                            {
                                y += step;
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    {
                        if (started)
                        {
                            if (e.Shift)
                            {
                                if (ValidateSize(size - 1))
                                    size--;
                            }
                            else
                            {
                                x -= step;
                            }
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        if (started)
                        {
                            if (e.Shift)
                            {
                                if (ValidateSize(size + 1))
                                    size++;
                            }
                            else
                            {
                                x += step;
                            }
                        }
                    }
                    break;
                case Keys.F1:
                    {
                        // создание и отображение диалогового окна с информацией о создателе программы
                        MessageBox.Show("Создатель программы.", "Автор", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //e.Handled = true; // предотвращение дальнейшей обработки клавиши F1 (например, открытия справки включенной по умолчанию)
                    }
                    break;
                case Keys.Enter:
                    {
                        started = true;
                    }
                    break;
            }
            if (started)
            {
                StickToBorder();
                Draw();
            }
        }
    }
}