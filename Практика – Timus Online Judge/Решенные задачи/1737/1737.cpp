#include <iostream> 
#include <string> 
#include <queue> // Подключение библиотеки для работы с очередью

using namespace std;

int main()
{
    // Создание очереди строк
    queue<string> result;

    // Кол-во букв
    int n; cin >> n;

    // Добавляем строки в очередь
    result.push("a");
    result.push("b");
    result.push("c");

    // Создание массива символов
    char abc[] = { 'a', 'b', 'c' };

    for (int i = 1; i < n; i++) // от 0 до n-1
    {
        while (true)
        {
            // Получение ссылки на первый элемент очереди
            string& s = result.front(); // string s

            // Если размер строки больше i выходим из цикла
            if (s.size() > i) 
                break;

            // Получение последнего символа строки
            char last = s[s.size() - 1]; 

            if (s.size() > 1) 
            {
                // Получение предпоследнего символа строки
                char lbo = s[s.size() - 2]; 

                for (int k = 0; k < 3; k++)
                {
                    // Получение k-ого символа из массива abc
                    char c = abc[k]; 

                    // Если последний и предпоследний символы не равны символу "с" добавляем новую строку в очередь
                    if (last != c && lbo != c) 
                        result.push(s + c);
                }
            }
            else
            {
                for (int k = 0; k < 3; k++) // Цикл с k от 0 до 2
                {
                    // Получение k-ого символа из массива abc
                    char c = abc[k]; 

                    // Если последний символ не равен символу "c" добавляем новую строку в очередь
                    if (last != c)
                        result.push(s + c);
                }
            }
            // Удаление первого элемента из очереди
            result.pop(); 
        }
        // Если размер очереди умноженный на (i+1) больше 100000 
        if ((i + 1) * result.size() > 100000)
        {
            cout << "TOO LONG";
            return 0;
        }
    }

    // Пока очередь не пуста, выводим первый элемент очереди и удаляем его
    while (!result.empty()) 
    {
        cout << result.front() << endl;
        result.pop();
    }

    return 0;
}
