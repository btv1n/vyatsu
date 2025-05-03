using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
4.5.Напишите программу нахождения корней квадратного уравнения a × x2 + b × x + c = 0. 
Осуществите проверку корректности ввода. Учитите, что a  может быть равно нулю.
*/

namespace Task_5
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

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, c;

            // textBox1 = a ; textBox2 = b ; textBox3 = c

            // проверка заполнения полей
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            // проверка на корректность ввода
            if (!double.TryParse(textBox1.Text, out a) ||
                !double.TryParse(textBox2.Text, out b) ||
                !double.TryParse(textBox3.Text, out c))
            {
                MessageBox.Show("Ошибка ввода!");
                return;
            }

            // считаем дискриминант (a*x^2 + b*x + c = 0)
            double discriminant = b * b - 4 * a * c;

            // выводим корни уравнения
            if (discriminant < 0)
            {
                MessageBox.Show("Корни отсутствуют!");
                //resultLabel.Text = "";
            }
            else if (discriminant == 0)
            {
                double x = -b / (2 * a);
                MessageBox.Show($"x = {x.ToString()}");
            }
            else
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                MessageBox.Show($"x1 = {x1.ToString()}, x2 = {x2.ToString()}");
                
            }
        }
    }
}