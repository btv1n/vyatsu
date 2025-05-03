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
(1) +
(2) +
(3) +
(4) +
(5) +
(6) -
(7) -
(8) +
(9) -
*/


/*
4.1.	Модифицируйте приложение из упражнения 4.3 таким образом, чтобы пользователь имел возможность 
задать единицу измерения не только результата, но и исходных данных (рис. 4.6). Осуществите проверку 
корректности ввода. В любой момент работы программы в окно ответа должен выводиться верный ответ, 
или (в случае невозможности вычисления ответа) оно должно оставаться пустым. Добавьте следующие 
меры длины: сантиметр, метр.
*/

namespace Task_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double modifIn, modifOut;

            // Проверяем, какая radioButton выбрана и устанавливаем соответствующий
            // модификатор для входных значений
            if (radioButton1.Checked) modifIn = 0.0254;
            else if (radioButton2.Checked) modifIn = 0.3048;
            else if (radioButton3.Checked) modifIn = 0.9144;
            else if (radioButton4.Checked) modifIn = 0.01;
            else modifIn = 1;
            if (radioButton6.Checked) modifOut = 39.3700787;
            else if (radioButton7.Checked) modifOut = 3.28084;
            else if (radioButton8.Checked) modifOut = 1.09361;
            else if (radioButton9.Checked) modifOut = 100;
            else modifOut = 1;

            double Result;
            // Пытаемся преобразовать текст в textBox1 в тип double и
            // сохраняем результат в переменную Result
            bool Success = double.TryParse(textBox1.Text, out Result);
            if (Success) // Если успешно удалось преобразовать текст в число
            {

                Result *= modifIn * modifOut; // Умножаем результат на модификаторы входных и выходных значений

                textBox2.Text = String.Format("{0:0.00}", Result); // Выводим результат в textBox2 с форматированием до двух знаков после запятой
            }
            else textBox2.Text = ""; // Если преобразование не удалось, очищаем textBox2 (делаем его пустым)
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}