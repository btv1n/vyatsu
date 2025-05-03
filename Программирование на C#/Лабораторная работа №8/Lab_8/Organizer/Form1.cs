using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
Задачи:
- Когда файл, который не задан, не найден, нужно полностью очищать содержимое страниц ежедневника
для loadfromfile    
*/

namespace Organizer
{
    public partial class MainForm : Form
    {
        string FileName;
        private Timer timer; //

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // Определение выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            // Добавление записи на текущий ListBox
            CurrentListBox.Items.Add(RecordTextBox.Text);

            // Очистка окна ввода
            RecordTextBox.Text = "";

            // Убираем выделение с текста в listBox
            CurrentListBox.ClearSelected();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            // Определение выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            // Запись в окно редактирования выбранного значения
            RecordTextBox.Text = (string)CurrentListBox.SelectedItem;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FileName = dateTimePicker1.Text + "org";
            LoadFromFile(FileName);


            //////////// Status strip ////////////

            // Отобразит текущую дату
            toolStripStatusLabelData.Text = DateTime.Now.ToString("dd.MM.yyyy");

            // Отобразит текущий день недели
            toolStripStatusLabelDayOfWeek.Text = DateTime.Now.ToString("dddd");

            // Отобразит текущее время
            toolStripStatusLabelTime.Text = DateTime.Now.ToString("HH:mm:ss");

            // Создать таймер, чтобы обновлять дату и время каждую секунду
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 секунда - обновляем каждую секунду
            timer.Tick += Timer_Tick;
            timer.Start();

            //////////// Status strip ////////////
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновляет отображение даты, дня недели, времени каждую секунду
            toolStripStatusLabelData.Text = DateTime.Now.ToString("dd.MM.yyyy");
            toolStripStatusLabelDayOfWeek.Text = DateTime.Now.ToString("dddd");
            toolStripStatusLabelTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void SaveToFile(string FileName)
        {
            try
            {
                // Открываем файл для записи
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    // Перебираем все компоненты ListBox
                    for (int i = 1; i <= 4; i++)
                    {
                        // Задаем текущий компонент ListBox
                        ListBox CurListBox =
                        (ListBox)Controls.Find("listBox" + i, true)[0];
                        // Записываем в файл кол во строк списка
                        sw.WriteLine(CurListBox.Items.Count.ToString());
                        // Записываем в файл все записи из списка
                        for (int j = 0; j < CurListBox.Items.Count; j++)
                            sw.WriteLine(CurListBox.Items[j]);
                        // Очищаем список записей текущего ListBox
                        CurListBox.Items.Clear();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении!");
            }
        }

        private void LoadFromFile(string FileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    // Перебираем все компоненты ListBox
                    for (int i = 1; i <= 4; i++)
                    {
                        // Задаем текущий компонент ListBox
                        ListBox CurListBox =
                        (ListBox)Controls.Find("listBox" + i, true)[0];

                        // Считываем количество строк списка из файла
                        int itemCount = int.Parse(sr.ReadLine());

                        // Считываем и добавляем записи в список
                        for (int j = 0; j < itemCount; j++)
                        {
                            string item = sr.ReadLine();
                            CurListBox.Items.Add(item);
                        }
                    }
                }
            }
            catch (FileNotFoundException) // если файл не найден
            {
                // Если файл не найден, очищаем все компоненты ListBox
                MessageBox.Show("Файл не найден");
                //for (int i = 0; i <= 4; i++)
                //{
                //    ListBox CurListBox =
                //    (ListBox)Controls.Find("listBox" + i, true)[0];
                //    CurListBox.Items.Clear();
                //}
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке!");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Очистка содержимого TextBox
            RecordTextBox.Text = "";

            SaveToFile(FileName);
            FileName = dateTimePicker1.Text + "org";
            LoadFromFile(FileName);
        }

        private void ChangeButton_Click(object sender, EventArgs e) // кнопка изменить
        {
            // Определение имени выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);

            // Нахождение компонента ListBox с заданным именем
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            // Если выбрана запись в списке
            if (CurrentListBox.SelectedIndex != -1)
            {
                // Изменение содержимого выбранной записи на текст из TextBox
                CurrentListBox.Items[CurrentListBox.SelectedIndex] = RecordTextBox.Text;

                // Очистка содержимого TextBox
                RecordTextBox.Text = "";
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Определение имени выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);

            // Нахождение компонента ListBox с заданным именем
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            if (CurrentListBox.SelectedIndex != -1) // проверка, что строка выбрана
            {
                int selectedIndex = CurrentListBox.SelectedIndex;
                CurrentListBox.Items.RemoveAt(selectedIndex); // Удаление выбранной строки

                if (selectedIndex < CurrentListBox.Items.Count) // если еще остались строки после удаления
                {
                    // Установка выбранной строки как следующей после удаления
                    CurrentListBox.SetSelected(selectedIndex, true);
                }
                else if (CurrentListBox.Items.Count > 0) // если это была последняя не единственная строка в списке
                {
                    // Установка последней строки как выбранной после удаления
                    CurrentListBox.SetSelected(CurrentListBox.Items.Count - 1, true);
                }

                // Метод для отображения выбранной строки в окне редактирования
                RecordTextBox.Text = CurrentListBox.Text;
            }
            else
            {
                MessageBox.Show("Удалять нечего.");
            }
        }

        // Очистка содержимого текущей вкладки ежедневника
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Определение имени выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);

            // Нахождение компонента ListBox с заданным именем
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            // Очистка содержимого ListBox
            CurrentListBox.Items.Clear();

            // Очищает поле редактирования
            RecordTextBox.Text = "";
        }

        private void RecordTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Определение выбранного компонента ListBox
                string s = "listBox" + (OrgTabControl.SelectedIndex + 1);
                ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

                // Добавление записи на текущий ListBox
                CurrentListBox.Items.Add(RecordTextBox.Text);

                // Очистка окна ввода
                RecordTextBox.Text = "";

                // Указывает что событие обработано успешно
                e.Handled = true;
            }
        }

        
        private void CloseButton_Click(object sender, EventArgs e) // кнопка удаление
        {
            // Сохраняем данные текущего дня ежедневника в файл
            FileName = dateTimePicker1.Text + "org";
            SaveToFile(FileName);

            // Закрываем предложение
            Application.Exit();
        }

        // При переключении вкладок на OrdTabControls выделенные строки должны появлятся в окне редактирования
        private void OrgTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //определение выбранного компонента ListBox
            string s = "listBox" + (OrgTabControl.SelectedIndex + 1);
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];

            if (CurrentListBox.SelectedIndex != -1)
            {
                RecordTextBox.Text = CurrentListBox.SelectedItem.ToString();
            }
            else
            {
                RecordTextBox.Text = "";
            }
        }

        // Сохранение данных в файл при закрытии предложения любым способом
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) 
        {
            // Сохраняем данные текущего дня ежедневника в файл
            FileName = dateTimePicker1.Text + "org";
            SaveToFile(FileName);
        }
    }
}
