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
 * 3.1.	Напишите приложение, выводящее всю возможную информацию о нажатой на клавиатуре 
 * клавише (например, буква это или цифра, заглавная или строчная буква, кириллица 
 * или латинский алфавит и т. д.).
*/

namespace Task_3._1
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Pen Pen;
        Font Font;
        SolidBrush MyBrush;
        int x0, y0;
        string text;
        string isUp, isNum, isLatin;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Graph.Clear(Color.White);

            text = "";
            text += e.KeyCode.ToString() + '\n' +
                   "Код клавиши: " + e.KeyValue + '\n';

            if (e.KeyValue < 65 && !char.IsLetterOrDigit((char)e.KeyCode))
            {
                text += "Служебная" + '\n';
                Graph.DrawString(text, Font, MyBrush, x0, y0);
            }
            // код символа и код 
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F24)
            {
                text += "Функциональная" + '\n';
                Graph.DrawString(text, Font, MyBrush, x0, y0);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            isUp = (Char.IsUpper(e.KeyChar)) ? "заглавная" : "строчная";
            isNum = (Char.IsDigit(e.KeyChar)) ? "Цифра" : "Символ";
            isLatin = "";

            if (e.KeyChar >= 65 && e.KeyChar <= 122) 
                isLatin = "Латиница";
            else if (e.KeyChar >= 1040 && e.KeyChar <= 1103) 
                isLatin = "Кириллица";

            text += "Символ: " + e.KeyChar.ToString() + '\n' +
                    "Код символа: " + (int)e.KeyChar + '\n' +
                    "Регистр: " + isUp + '\n' +
                    isNum + '\n';

            if (isLatin != "") 
                text += isLatin + '\n';

            Graph.DrawString(text, Font, MyBrush, x0, y0);
        }

        public Form1()
        {
            InitializeComponent();

            Graph = CreateGraphics();
            Pen = new Pen(Color.Black);
            MyBrush = new SolidBrush(Color.Black);
            Font = new Font("Arial", 20, FontStyle.Bold);
            x0 = this.ClientSize.Width / 4;
            y0 = this.ClientSize.Height / 4;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}