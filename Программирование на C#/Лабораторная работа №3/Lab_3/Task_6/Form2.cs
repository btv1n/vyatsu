using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3._6
{
    public partial class Form2 : Form
    {
        Graphics graph;

        public Form2()
        {
            InitializeComponent();
            graph = CreateGraphics();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void Draw(Pen pen, SolidBrush brush, Point p1, uint width, uint height)
        {
            graph.DrawEllipse(pen, p1.X, p1.Y, width, height);
            graph.FillEllipse(brush, p1.X, p1.Y, width, height);
        }
    }
}