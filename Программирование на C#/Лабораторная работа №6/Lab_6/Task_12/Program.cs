using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
6.12. Удалите из сообщения все слова, содержащие введенный пользователем символ без учета регистра. 
Выведите полученную строку. После этого выведите те слова предложения, которые встречаются в измененном 
сообщении не менее трех раз (выводить эти слова по убыванию их длин).
*/

namespace Task_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите сообщение:");
            string message = Console.ReadLine();

            Console.WriteLine("Введите символ:");
            // Считываем введенный символ и приводим его к нижнему регистру, сохраняем первый символ в переменной symbol
            char symbol = Console.ReadLine().ToLower()[0];

            // Разбиваем строку message на массив слов, используя пробел как разделитель, и сохраняем в переменной words
            string[] words = message.Split(' ');

            // Создаем пустой список filteredWords для хранения отфильтрованных слов
            List<string> filteredWords = new List<string>();
            // Создаем пустой словарь wordCount для хранения количества вхождений каждого слова
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words) // Проходим по каждому слову в массиве words
            {
                // Проверяем, не содержит ли слово (приведенное к нижнему регистру) символ, если нет, то итерируемся по циклу далее
                if (!word.ToLower().Contains(symbol))
                {
                    filteredWords.Add(word); // Добавлям слово в список отфильтрованный слов
                    if (wordCount.ContainsKey(word)) // Проверяем, содержится ли слово в словаре wordCount
                    {
                        wordCount[word]++; // Если содержится, увеличиваем значение на единицу
                    }
                    else
                    {
                        wordCount[word] = 1; // Если не содержится, добавляем слово в словарь
                    }
                }
            }

            // Соединяем слова из списка filteredWords обратно в строку, разделяя их пробелами, и сохраняем в переменной 
            string filteredMessage = string.Join(" ", filteredWords);

            Console.WriteLine("Измененное сообщение:");
            Console.WriteLine(filteredMessage);


            /*
            (1) Создаем пустой список repeatedWords для хранения слов, встречающихся не менее трех раз
            (2) .Where() = Фильтруем словарь wordCount, оставляя только пары, у которых значение 
            (количество вхождений) больше или равно 3
            (3) OrderByDescending(pair => pair.Key.Length) = Сортируем полученные пары по убыванию длины ключа (слова)
            (4) .Select =  Выбираем только слова из отсортированных пар
            (5) Сохраняем полученные ключи (слова) в списке repeatedWords
            */
            List<string> repeatedWords = wordCount.Where(pair => pair.Value >= 3)
                                                  .OrderByDescending(pair => pair.Key.Length)
                                                  .Select(pair => pair.Key)
                                                  .ToList();
            /*
            List<string> repeatedWords = new List<string>();

            foreach (KeyValuePair<string, int> pair in wordCount)
            {
                if (pair.Value >= 3)
                {
                    repeatedWords.Add(pair.Key);
                }
            }

            repeatedWords.Sort((a, b) => b.Length.CompareTo(a.Length));   
            */



            Console.WriteLine("Слова, встречающиеся не менее трех раз:");
            foreach (string word in repeatedWords) // Проходим по каждому слову в списке repeatedWords и выводим его на экран
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }
    }
}
