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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;



// Недоделан проект

// На кнопках выравнивания текста стоит "CheckOnClick" поэтому они при нажатии/активации подсвечиваются синим цветом, рамка синяя


namespace TextEditor
{
    public partial class MainForm : Form
    {
        private bool isDocumentModified = false; // флаг для отслеживания изменений в документе

        public MainForm()
        {
            InitializeComponent();
            ToolStrip.ImageList = imageList1;
            CenterToolStripButton.ImageIndex = 0;
            LeftToolStripButton.ImageIndex = 1;
            RightToolStripButton.ImageIndex = 2;
        }

        // Открытие файла
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            // Устанавливаем фильтр
            openFileDialog1.Filter = "RTF files|*.rtf";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Загружаем текст из файла
                TextEdit.LoadFile(openFileDialog1.FileName);

                // Отображаем полное имя файла в заголовке формы
                Text = "Text Editor   " + openFileDialog1.FileName;
            }

        }


        // Сохранение файла
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            // Путь для сохранения текстовых документов
            string filePath = "C:\\";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Текстовый файл|*.txt";
            saveFileDialog1.Title = "Сохранить как текстовый файл";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Сохраняем текст из RichTextBox в выбранный файл
                File.WriteAllText(saveFileDialog1.FileName, TextEdit.Text);
            }

            if (string.IsNullOrEmpty(filePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Text File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    SaveDocument(filePath);
                }
            }
            else
            {
                SaveDocument(filePath);
            }
        }
        private void SaveDocument(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write("123");
            writer.Close();
            MessageBox.Show("Документ успешно сохранен!");
        }


        // Создание нового файла
        private void createToolStripButton_Click(object sender, EventArgs e)
        {
            if (isDocumentModified)
            {
                DialogResult result = MessageBox.Show("Документ был изменен. Сохранить изменения?", "Сохранение документа", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveDocument_(); // сохраняем документ
                    TextEdit.Clear();
                }
                else if (result == DialogResult.No)
                {
                    TextEdit.Clear();
                }
                else if (result == DialogResult.Cancel)
                {
                    return; // отменяем создание нового документа
                }
            }

            
            // здесь добавляем логику создания нового текстового документа
        }
        private void SaveDocument_()
        {
            // здесь добавляем логику сохранения документа
            isDocumentModified = false; // сбрасываем флаг изменений после успешного сохранения
        }

        // Вызов окна помощи
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создатель программы.");
        }

        private void SizeTSComboBox_Click(object sender, EventArgs e)
        {

        }

        // Выделение жирным текста
        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            if (BoldToolStripButton.Checked) //добавляем полужирный стиль начертания
                TextEdit.SelectionFont =
                new Font(TextEdit.SelectionFont,
                TextEdit.SelectionFont.Style | FontStyle.Bold);
            else    //отменяем полужирный стиль начертания
                TextEdit.SelectionFont =
                new Font(TextEdit.SelectionFont,
                TextEdit.SelectionFont.Style & ~FontStyle.Bold);
        }

        private void TextEdit_SelectionChanged(object sender, EventArgs e)
        {
            if (TextEdit.SelectionFont == null) return;
            BoldToolStripButton.Checked = TextEdit.SelectionFont.Bold;
        }

        // Изменение размера текста
        private void SizeTSComboBox_DropDownClosed(object sender, EventArgs e)
        {
            TextEdit.SelectionFont =
            new Font(TextEdit.SelectionFont.FontFamily, Convert.ToInt16(SizeTSComboBox.SelectedItem.ToString()));
            TextEdit.Focus();
        }

        // Выравнивание по центру
        private void CenterToolStripButton_Click(object sender, EventArgs e)
        {
            if (TextEdit.SelectionAlignment == HorizontalAlignment.Center)
                TextEdit.SelectionAlignment = HorizontalAlignment.Left;
            else TextEdit.SelectionAlignment = HorizontalAlignment.Center;
        }

        // Вырвнивание по правому краю
        private void RightToolStripButton_Click(object sender, EventArgs e)
        {
            if (TextEdit.SelectionAlignment == HorizontalAlignment.Right)
                TextEdit.SelectionAlignment = HorizontalAlignment.Left;
            else TextEdit.SelectionAlignment = HorizontalAlignment.Right;
        }

        // Выравнивание по левому краю
        private void LeftToolStripButton_Click(object sender, EventArgs e)
        {
            TextEdit.SelectionAlignment = HorizontalAlignment.Left;

            //if (TextEdit.SelectionAlignment == HorizontalAlignment.Left)
            //    TextEdit.SelectionAlignment = HorizontalAlignment.Left;
            //else TextEdit.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void TextEdit_TextChanged(object sender, EventArgs e)
        {
            isDocumentModified = true; // устанавливаем флаг изменений при внесении изменений в текстовое поле (textBox1 - ваше текстовое поле)
        }
    }
}
