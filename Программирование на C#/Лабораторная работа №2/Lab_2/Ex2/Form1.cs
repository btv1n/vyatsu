using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex2
{
    public partial class Form1 : Form
    {
        Random Rand;

        public Form1()
        {
            InitializeComponent();
            //Инициализация генератора случайных чисел
            Rand = new Random();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Определение трех целых случайных чисел [0..255]
                int a = Rand.Next(256);
                int b = Rand.Next(256);
                int c = Rand.Next(256);
                BackColor = Color.FromArgb(a, b, c);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
