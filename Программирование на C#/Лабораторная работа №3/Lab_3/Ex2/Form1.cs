using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Ex2
{
    public partial class MainForm : Form
    {
        // Определение
        string TargetString = " абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        int CurrentIndex; // Для определения номера текущего символа в строке задания
        const int MaxCount = 10;
        int count = 0;
        Graphics Graph;
        Font MyFont;
        Random Rand;
        DateTime start;

        // Инициализация
        public MainForm()
        {
            InitializeComponent();

            Graph = CreateGraphics();
            MyFont = new Font("Arial", 32);
            Rand = new Random();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyFont.Dispose();
            Graph.Dispose();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar == 13) && (count == 0))
            {   //если нажат Enter и символы еще не выводились
                start = DateTime.Now;   //запоминаем время начала
                                        //определяем номер символа в строке
                CurrentIndex = Rand.Next(TargetString.Length);
                //отображаем этот символ на форме
                Graph.DrawString(TargetString.Substring(CurrentIndex, 1),
                MyFont, Brushes.Black, 160, 75);
                //увеличиваем количество выведенных символов
                count = 1;
                //изменяем заголовок
                Text = "Нажми правильную клавишу!";
            }
            else if ((count > 0) && (e.KeyChar == TargetString[CurrentIndex]))
            {   //если проверка началась и введен правильный символ
                if (count == MaxCount) //если проверка закончилась
                {
                    //определяем количество секунд с начала проверки
                    int time = DateTime.Now.Subtract(start).Seconds;
                    //выводим сообщение
                    MessageBox.Show("Время выполнения = " +
                    time.ToString() + " секунд");
                    Close();    //закрываем форму
                }
                else    //введен не последний символ
                {
                    //очищаем форму цветом формы
                    Graph.Clear(BackColor);
                    //определяем номер символа в строке
                    CurrentIndex = Rand.Next(TargetString.Length);
                    //отображаем этот символ на форме
                    Graph.DrawString(TargetString[CurrentIndex].ToString(),
                    MyFont, Brushes.Black, 160, 75);
                    //увеличиваем количество выведенных символов count++;
                }
            }
            //если введен неверный символ,
            //воспроизводим звуковой сигнал
            else System.Media.SystemSounds.Hand.Play();
        }
    }
}
