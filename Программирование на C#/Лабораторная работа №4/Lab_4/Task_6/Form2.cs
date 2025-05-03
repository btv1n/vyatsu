using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_6
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
        public void DrawEllipse(Pen pen, SolidBrush brush, Point p1, uint width, uint height)
        {
            graph.DrawEllipse(pen, p1.X, p1.Y, width, height);
            graph.FillEllipse(brush, p1.X, p1.Y, width, height);
        }
        public void DrawSquare(Pen pen, SolidBrush brush, Point p1, uint size)
        {
            graph.DrawRectangle(pen, p1.X, p1.Y, size, size);
            graph.FillRectangle(brush, p1.X, p1.Y, size, size);
        }
        public void DrawTriangle(Pen pen, SolidBrush brush, Point p1, Point p2, Point p3)
        {
            Point[] points = { p1, p2, p3 };
            graph.DrawPolygon(pen, points);
            graph.FillPolygon(brush, points);
        }
    }
}
