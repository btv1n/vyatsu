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
4.8.Напишите приложение «Вопрос работодателя», которое выполняет следующие действия:
–	первоначально форма имеет следующий вид (рис. 4.8);
–	кнопка «Да» имеет подсказку «Нажми сюда». При нажатии на кнопку «Да» приложение 
закрывается, выводя сообщение «Мы так и думали!»;
–	при попытке нажатия на кнопку «Нет» эта кнопка перемещается по форме в произвольном 
направлении, не уходя за границы формы (рис. 4.9).
Указания к решению: для отображения всплывающей подсказки воспользуйтесь компонентом 
ToolTip и свойством ToolTip on toolTip1 у соответствующего компонента Button.
*/


namespace Task_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button1, "Нажми сюда");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мы так и думали!");
            this.Close();

            //Form2 window = new Form2();
            //window.Show();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            // Генерация случайных координат для кнопки
            int newX = random.Next(0, ClientSize.Width - button2.Width);
            int newY = random.Next(0, ClientSize.Height - button2.Height);

            // Перемещение кнопки на новые координаты
            button2.Location = new Point(newX, newY);
        }
    }
}
