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
4.4.	Модифицируйте упражнение 3.2 «Клавиатурный тренажер» из лабораторной работы № 3 таким образом, 
чтобы пользователь мог задать уровень сложности тренажера (максимальное время отображения очередного 
символа строки задания на форме) – простой, средний или сложный. Каждый раз выдается по 10 символов. 
После окончания тестирования должно выводиться затраченное время, количество допущенных ошибок и 
количество пропущенных букв.
*/


namespace Task_4
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Выбор сложности
            int time = 0;
            if (radioButton1.Checked) time = 3; // Простая сложность = 3 секунды ожидания
            else if (radioButton2.Checked) time = 2; // Средняя сложность = 2 секунды ожидания
            else if (radioButton3.Checked) time = 1; // Сложная сложность = 1 секунда ожидания


            // Показать игровую форму
            Form2 playForm = new Form2(time);
            playForm.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
