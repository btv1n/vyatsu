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
6.11.	Удалите из строки последовательности символов, расположенные между круглыми 
скобками вместе со скобками (считается, что скобки в строке расставлены без ошибок).
*/

/*
        private string RemoveSubstringBetweenBrackets(string input)
        {
            int startIndex = input.IndexOf('('); // Находим первую открывающую скобку
            int endIndex = input.IndexOf(')'); // Находим первую закрывающую скобку

            // Если нет скобок или закрывающая скобка расположена перед открывающей, 
            // значит нет смысла удалять подстроку
            if (startIndex == -1 || endIndex == -1 || endIndex < startIndex)
            {
                return input;
            }

            // Удаляем подстроку, включая скобки
            return input.Remove(startIndex, endIndex - startIndex + 1);
        }
*/

/*
        static string RemoveTextInBrackets(string input)
        {
            int startIndex = input.IndexOf('('); // Находим первую открывающую скобку
            int endIndex = input.IndexOf(')'); // Находим первую закрывающую скобку

            while (endIndex > startIndex) // пока не достигли конца строки (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
            {
                input = input.Remove(startIndex, endIndex - startIndex + 1); // Удаляем скобки и их содержимое
                startIndex = input.IndexOf('('); // Находим следущую открывающую скобку
                endIndex = input.IndexOf(')'); // Находим следущую закрывающую скобку
            }
            return input;
        }
*/

namespace Task_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string RemoveTextInBrackets(string inputString)
        {
            int openBracketIndex = -1;
            int closeBracketIndex = -1;

            // Поиск открывающей и закрывающей скобок
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == '(')
                {
                    openBracketIndex = i;
                }
                else if (inputString[i] == ')' && openBracketIndex != -1)
                {
                    closeBracketIndex = i;
                    break;
                }
            }

            // Если найдены открывающая и закрывающая скобки
            if (openBracketIndex != -1 && closeBracketIndex != -1)
            {
                // Удаляем подстроку между скобками
                inputString = inputString.Remove(openBracketIndex, closeBracketIndex - openBracketIndex + 1);

                // Рекурсивно вызываем метод для обработки оставшейся строки
                return RemoveTextInBrackets(inputString);
            }
            else
            {
                // Если скобки больше не найдены, возвращаем полученную строку
                return inputString;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string output = RemoveTextInBrackets(input);
            textBox2.Text = output;
        }
    }
}
