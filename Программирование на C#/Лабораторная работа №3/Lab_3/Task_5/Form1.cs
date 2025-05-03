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
 * 3.5.	Напишите приложение, которое при нажатии на клавиатуре клавиш ‘1’, ‘2’, ‘3’ или ‘4’ 
 * открывает соответственно 1, 2, 3 или 4 новых окна.
*/

namespace Task_3._5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            int amount = 0; // кол-во окон для открытия

            switch (e.KeyData)
            {
                case Keys.D1:
                    {
                        amount = 1;
                    }
                    break;
                case Keys.D2:
                    {
                        amount = 2;
                    }
                    break;
                case Keys.D3:
                    {
                        amount = 3;
                    }
                    break;
                case Keys.D4:
                    {
                        amount = 4;
                    }
                    break;
            }

            for (int i = 0; i < amount; i++)
            {
                Form temp = new Form(); // создается объект, который является окном Windows Forms
                temp.Show(); // отображает созданное окно на экране
            }

            this.Focus(); // восстанавливает фокус текущего окна т.е. указывает на него
        }
    }
}