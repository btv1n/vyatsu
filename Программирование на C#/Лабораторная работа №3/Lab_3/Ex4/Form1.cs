using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex4
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Font MyFont;
        SolidBrush MyBrush;
        Random Rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graph = CreateGraphics();
            Rand = new Random();
            MyFont = new Font("Arial", 30, FontStyle.Bold);
            MyBrush = new SolidBrush(Color.Black);
            Text = Text + "Load";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graph.DrawString("Упражнение 3.4", MyFont, MyBrush, 50, 150);
            MyBrush.Color = Color.FromArgb(Rand.Next(256), Rand.Next(256), Rand.Next(256));
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Text = Text + "Shown";
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show(); //ShowDialog() 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Закрытие приложения", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
