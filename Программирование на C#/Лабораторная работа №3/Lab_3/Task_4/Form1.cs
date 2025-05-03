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
 * 3.4.	Напишите приложение, которое в заголовке формы выводит ее размеры и координаты на экране, 
 * а по центру формы независимо от ее размеров изображает круг радиусом 30 пикселей. 
 * Минимальный размер формы – 150×150.
*/

namespace Task_3._4
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        SolidBrush Brush;
        const int radius = 30;
        int x0, y0;

        public Form1()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Brush = new SolidBrush(Color.Black);
            this.Text = "Width:" + this.Width.ToString() + 
                        " Height: " + this.Height.ToString() + 
                        " X: " + this.DesktopLocation.X + 
                        " Y: " + this.DesktopLocation.Y;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            x0 = this.ClientSize.Width / 2; 
            y0 = this.ClientSize.Height / 2;
            Graph.FillEllipse(Brush, x0 - radius, y0 - radius, radius * 2, radius * 2);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            this.Text = this.Width.ToString() + ' ' + this.Height.ToString();

            x0 = this.ClientSize.Width / 2; 
            y0 = this.ClientSize.Height / 2;

            Graph.FillEllipse(Brush, x0 - radius, y0 - radius, radius * 2, radius * 2);

            this.Text = "Width:" + this.Width.ToString() + 
                        " Height: " + this.Height.ToString() + 
                        " X: " + this.DesktopLocation.X + 
                        " Y: " + this.DesktopLocation.Y;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            this.Text = "Width:" + this.Width.ToString() + 
                        " Height: " + this.Height.ToString() + 
                        " X: " + this.DesktopLocation.X + 
                        " Y: " + this.DesktopLocation.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}