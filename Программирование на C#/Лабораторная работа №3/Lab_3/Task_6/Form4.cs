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
    public partial class Form4 : Form
    {
        Graphics graph;

        public Form4()
        {
            InitializeComponent();
            graph = CreateGraphics();
        }

        public void Draw(Pen pen, SolidBrush brush, Point p1, Point p2, Point p3)
        {
            Point[] points = { p1, p2, p3 };
            graph.DrawPolygon(pen, points);
            graph.FillPolygon(brush, points);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}