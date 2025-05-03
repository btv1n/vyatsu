using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/* Изначальная расстановка кусочков
2  6  11 4
5  8  1  7
13 3  15 9
12 10 14 16
*/

namespace Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Применение свойства AllowDrop
            for (int i = 1; i <= 16; i++)
                ((PictureBox)(Controls.Find("picture" + i, true)[0])).AllowDrop = true;

            ShufflePictureBoxes(); // перемешивает картинки на поле

            MessageBox.Show("Если вы хотите увидеть как выглядит собранная картинка, нажмите F1");
        }

        private void picture1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop((PictureBox)sender, DragDropEffects.Move); // инициализируем процесс перетаскивания
        }

        private void picture1_DragEnter(object sender, DragEventArgs e) // событие возникает, когда курсор мыши попадает в пределы объекта-приемника
        {
            e.Effect = DragDropEffects.Move;
        }

        private void picture1_DragDrop(object sender, DragEventArgs e) // событие завершения процесса перетаскивания
        {
            PictureBox receiver = (PictureBox)sender; // объект приемник
            PictureBox source = (PictureBox)e.Data.GetData((typeof(PictureBox))); // объект источник
            Image Temp = receiver.Image; // Temp вспомогательная переменная для хранения изображения
            
            // Меняем картинки местами
            receiver.Image = source.Image;
            source.Image = Temp;


            //////// Сообщение о собранной картинке ////////
            //MessageBox.Show(Field.GetColumn(picture1).ToString());
            //MessageBox.Show(Field.Controls.IndexOf(picture1).ToString());


            //Point location = picture1.Location;
            //Size size = picture1.Size;
            //TableLayoutPanelCellPosition cellPosition = Field.GetPositionFromControl(picture1);
            //int rowIndex = cellPosition.Row;
            //int columnIndex = cellPosition.Column;




            //MessageBox.Show(picture15.TabIndex.ToString());


            //////// Сообщение о собранной картинке ////////
        }

        private void ShufflePictureBoxes() // метод перестановки частей картинки в произвольном порядке
        {
            Random random = new Random();

            // Список PictureBox из всех PictureBox, которые содержит элемент Field
            List<PictureBox> pictureBoxes = Field.Controls.OfType<PictureBox>().ToList();

            // Список из всех возможных позиций
            List<TableLayoutPanelCellPosition> positions = new List<TableLayoutPanelCellPosition>();

            // Получаем все возможные позиции
            for (int rowIndex = 0; rowIndex < Field.RowCount; rowIndex++) // цикл по строкам
            {
                for (int columnIndex = 0; columnIndex < Field.ColumnCount; columnIndex++) // цикл по столбцам 
                {
                    positions.Add(new TableLayoutPanelCellPosition(columnIndex, rowIndex)); // добавление новой позиции в список
                }
            }

            // Перемешиваем PictureBoxes
            foreach (PictureBox picture in pictureBoxes) // перебирает все pictureBox
            {
                // Выбирает случайную позицию
                int index = random.Next(positions.Count); // генерация случайного числа от 0 до кол-ва доступных позиций
                TableLayoutPanelCellPosition position = positions[index]; // получает позицию по сгенерированному индексу

                // Устанавливает позицию PictureBox и удаляет ее из списка доступных позиций
                Field.SetCellPosition(picture, position);
                positions.RemoveAt(index);
            }
        }


        private void picture1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // метод отображения формы с картинкой
        {
            if (e.KeyCode == Keys.F1) // проверяем, что была нажата клавиша F1
            {
                Form2 FormPicture = new Form2(); // создаем экземпляр новой формы
                FormPicture.ShowDialog(); // отображаем новую форму
            }
        }

        private void picture5_Click(object sender, EventArgs e)
        {

        }
    }
}
