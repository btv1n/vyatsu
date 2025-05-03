using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Task_4
{
    public partial class Form2 : Form
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
        int mistakes = 0, lostSings = 0;
        int time;

        Timer timer = new Timer(); // создание объекта таймера

        // Инициализация
        public Form2(int time)
        {
            InitializeComponent();
            this.time = time;

            Graph = CreateGraphics();
            MyFont = new Font("Arial", 32);
            Rand = new Random();

            // задание интервала таймера в миллисекундах
            if (time == 3)
                timer.Interval = 3000; // 3 секунды ожидания
            else if (time == 2)
                timer.Interval = 2000; // 2 секунды ожидания
            else if (time == 1)
                timer.Interval = 1000; // 1 секунда ожидания

            timer.Tick += timer1_Tick; // Подписываемся на событие Tick таймера
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //timer.Interval = 0; // устанавливает интервал таймера в 0 миллисекунд
            timer.Enabled = false; // отключает таймер
            timer.Enabled = true; // включает таймер
            // чтобы обнулить счетчик

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

                /////////////
                timer.Start(); // запуск таймера при первом символе
            }
            else if (e.KeyChar != TargetString[CurrentIndex])
            {
                mistakes++;
            }
            else if ((count > 0) && (e.KeyChar == TargetString[CurrentIndex]))
            {
                //если проверка началась и введен правильный символ
                if (count == MaxCount) //если проверка закончилась
                {
                    /////////////
                    // если проверка закончилась
                    timer.Stop(); // остановка таймера

                    //определяем количество секунд с начала проверки
                    int time = DateTime.Now.Subtract(start).Seconds;

                    //выводим сообщение
                    MessageBox.Show("Время выполнения = " + time.ToString() + " секунд."
                        + " Кол-во допущенных ошибок = " + mistakes.ToString() + "."
                        + " Кол-во пропущенных букв = " + lostSings.ToString());

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
                    count++;
                }
            }
            //если введен неверный символ,
            //воспроизводим звуковой сигнал
            else System.Media.SystemSounds.Hand.Play();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyFont.Dispose();
            Graph.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // здесь выполняется код при каждом срабатывании таймера
            lostSings++; // увеличиваем счетчик пропущенных символов
            Graph.Clear(BackColor);
            CurrentIndex = Rand.Next(TargetString.Length);
            Graph.DrawString(TargetString[CurrentIndex].ToString(),
                MyFont, Brushes.Black, 160, 75);


            /*
            Для управления таймером используются два свойства:
            –	Enabled – получает или задает признак активности таймера;
            –	Interval – получает или задает время в миллисекундах до вызова события Tick 
            относительно момента, когда событие Tick произошло последний раз.
            Единственное событие компонента Timer – событие Tick, которое происходит по 
            истечении интервала времени, заданного в свойстве Interval таймера, при условии, 
            что таймер активен.
            timer.Dispose();
            timer.Stop(); // останавливает таймер
            timer.Start(); // запускает таймер с начала
            // обнуляем счетчик, чтобы вернуть время ожидания в исходное положение
            */

        }
    }
}
