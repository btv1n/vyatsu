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
    public partial class Form3 : Form
    {
        Graphics graph;

        public Form3()
        {
            InitializeComponent();
            graph = CreateGraphics();
        }
        public void Draw(Pen pen, SolidBrush brush, Point p1, uint size)
        {
            graph.DrawRectangle(pen, p1.X, p1.Y, size, size);
            graph.FillRectangle(brush, p1.X, p1.Y, size, size);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}