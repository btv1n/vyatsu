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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Text = Text + "Load";
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            Text = Text + "Shown";
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            Text = Text + "Resize";
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            Text = Text + "Activated";
        }

        private void Form2_Deactivate(object sender, EventArgs e)
        {
            Text = Text + "Deactivate";
        }
    }
}
