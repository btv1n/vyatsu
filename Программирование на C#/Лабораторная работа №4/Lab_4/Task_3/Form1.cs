using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

/*
4.3.Напишите приложение для определения идеального веса по следующей формуле:
Вес = k × (50 + 0,75 × (h – 150) + 0,25 × (a – 20)),
где
k – коэффициент (для мужчин k = 1, для женщин k = 0,9);
h – рост в см (корректными данными считаем 120<h<200);
a – возраст в годах (корректными данными считаем 15<a<80).
Для ввода k использовать RadioButton, остальные данные вводить в TextBox. 
При вводе некорректных данных вывести соответствующее сообщение.
*/

namespace Task_3
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double weight = 0;

            // Определим коэфициент k. radioButton1 = для мужчин ; radioButton2 = для женщин
            double k = 0;
            if (radioButton1.Checked) k = 1;
            else if (radioButton2.Checked) k = 0.9;

            // textBox1 = рост в см; textBox2 = возраст
            double h, a;
            bool success = double.TryParse(textBox1.Text, out h);
            if ((success) && (h > 120) && (h < 200))
            {
                success = double.TryParse(textBox2.Text, out a);
                if ((success) && (a > 15) && (a < 80))
                {
                    // если все проверки успешно пройдены вычисляем идеальный вес
                    weight = k * (50 + 0.75 * (h - 150) + 0.25 * (a - 20));
                    textBox3.Text = weight.ToString();
                }
                else
                {
                    MessageBox.Show("Неверный тип входных данных - возраст");
                    textBox3.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Неверный тип входных данных - рост");
                textBox3.Text = "";
            }
        }
    }
}
