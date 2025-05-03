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
6.10. Сформируйте новую строку, отличающуюся от исходной тем, что группы идущих подряд 
одинаковых символов разделены символом ‘*’. Пример: “aabecccdaaa” → “aa*b*e*ccc*d*aaa”.
*/

namespace Task_10
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

        private string TransformString(string inputString) // введенная строка
        {
            string transformedString = "";

            for (int i = 0; i < inputString.Length; i++)
            {
                transformedString += inputString[i];

                // inputString.Length - 1 = нужен для того, чтобы не выйти за границы массива
                if (i < inputString.Length - 1 && inputString[i] != inputString[i + 1]) // если не конец строи и текущий символ не равен следущему
                {
                    transformedString += "*"; // добавляем звездочку
                }
            }

            return transformedString; // возращаем преобразованную строку
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputString = textBox1.Text; // ввод из textBox1
            string transformedString = TransformString(inputString); // преобразование строки
            textBox2.Text = transformedString; // вывов строки в textBox2
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
