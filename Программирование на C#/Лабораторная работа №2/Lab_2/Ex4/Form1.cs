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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (Cursor == Cursors.Cross) Cursor = Cursors.Hand;
                else Cursor = Cursors.Cross;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
