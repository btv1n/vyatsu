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
4.2.Напишите программу для подсчета количества полных дней, оставшихся до конца года, 
если сейчас год Y, месяц M, день D (Y, M и D вводятся пользователем). Кнопка «Подсчитать» 
должна быть расположена в правом нижнем углу формы на расстоянии 10 пикселей от ее края,
даже если размер формы изменяется. Осуществите проверку корректности ввода.
*/

/*
Для фиксации кнопки использовал свойство: Anchor = Bottom, Right
Указал в свойствах отступ 10px относительно окна
*/

namespace Task_2
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
            // извлекает первые два символа из текстового поля maskedTextBox1 и сохраняет их в переменной days.
            string days = maskedTextBox1.Text.Substring(0, 2);


            int dd = 0, mm = 0, yy = 0;
            // попытка преобразовать строку в число и сохранить результат в переменной dd. Если преобразование
            // успешно то true
            bool result = int.TryParse(days, out dd);
            if (result)
            {
                string months = maskedTextBox1.Text.Substring(3, 2);
                result = int.TryParse(months, out mm);
                if (result)
                {
                    string years = maskedTextBox1.Text.Substring(6, 4);
                    result = int.TryParse(years, out yy);
                }
            }

            if (!result) MessageBox.Show("Неверный тип входных данных");
            else
            {
                // проверка корректности заданной даты
                if (mm < 1 || mm > 12 || dd < 0 || dd > DateTime.DaysInMonth(yy, mm))
                    MessageBox.Show("Неверно задана дата");
                else
                {
                    // если дата задана верно, то вычисляется кол-во дней, оставшихся до конца года
                    int resultd = DateTime.DaysInMonth(yy, mm) - dd; // кол-во дней в текущем месяце
                    for (int i = mm + 1; i <= 12; i++) // перебор и прибавление месяцем начиная со следующего после текущего месяца
                    {
                        resultd += DateTime.DaysInMonth(yy, i);
                    }
                    textBox1.Text = resultd.ToString(); //преобразование в строку и вывод в текстовое поле textBox1
                }
            }
        }
    }
}