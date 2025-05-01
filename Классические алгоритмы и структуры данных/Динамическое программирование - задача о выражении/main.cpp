#include <iostream>
#include <string>
#include <map> // включение библиотеки для работы с ассоциативными контейнерами (словари)

using namespace std;


/*
Условие задачи:

Динамическое программирование
Реализовать классическую задачу о выражении. Дано арифметическое выражение,
операндами которого являются целые положительные числа, а разрешенными операциями «+» и «*».
Требуется расставить скобки так, чтобы результат вычисления выражения был максимальным.
*/


// Проверяет является ли символ оператором (возвращает true / false)
bool isOperator(char op)
{
    return (op == '+' || op == '*');
}

// Рассчитывает значения выражений
map<string, int> getValExp(string expression, map<string, map<string, int>>& data)
{
    // count - возвращает кол-во пар ключ-значение с заданным ключом. Если значение = 0, то значение выражения не было вычислено
    if (data.count(expression)) // проверяет рассчитано ли уже значение выражения
    {
        return data[expression]; // возвращает рассчитанное значение
    }

    map<string, int> res; // создает словарь для хранения значений выражения

    for (int i = 0; i < expression.length(); i++) // итерация по выражению
    {
        if (isOperator(expression[i])) // проверяет является ли символ оператором
        {
            // Рекурсивный вызов функций (Prefix - до операнда ; Suffix - после операнда)
            map<string, int> resPre = getValExp(expression.substr(0, i), data); // получает значение подвыражения до оператора
            map<string, int> resSuf = getValExp(expression.substr(i + 1), data); // получает значение подвыражения после оператора

            for (auto& resPreEntry : resPre) // итерация по значениям подвыражения до оператора
            {
                for (auto& resSufEntry : resSuf) // итерация по значениям подвыражения после оператора
                {
                    if (expression[i] == '+') // сложение
                    {
                        // записывает значение выражения в словарь
                        res["(" + resPreEntry.first + "+" + resSufEntry.first + ")"]
                            = resPreEntry.second + resSufEntry.second;
                    }
                    else if (expression[i] == '*') // умножение
                    {
                        // записывает значение выражения в словарь
                        res["(" + resPreEntry.first + "*" + resSufEntry.first + ")"]
                            = resPreEntry.second * resSufEntry.second;
                    }
                }
            }
        }
    }

    if (res.size() == 0) // если словарь пустой, то в выражении одно число
    {
        res[expression] = stoi(expression); // записывает значение выражения в словарь
    }

    data[expression] = res; // записывает рассчитанноое значения в словарь

    //cout << expression << '\n'; // для наглядности
    return res; // возращает рассчитанные значения выражения
}

void printMaxValueExp(string expression)
{
    map<string, map<string, int>> data; // создает словарь для хранения рассчитанных значений выражений

    getValExp(expression, data); // вычисляет значения выражений

    map<string, int> requiredData = data[expression]; // получает рассчитанные значения заданного выражения

    string maxExpression = ""; // переменная для хранения выражения с максимальным значением
    int maxData = -9999; // переменная для хранения максимального значения

    for (auto& entry : requiredData) // итерируется по рассчитанным значениям выражения
    {
        //cout << "first: " << entry.first << " " << "second: " << entry.second << endl; // для наглядности
        if (entry.second > maxData) // значение больше максимального
        {
            maxExpression = entry.first; // сохраняет выражение с максимальным значением
            maxData = entry.second; // сохраняет максимальное значение
        }
    }

    cout << "Исходное выражение: " << expression << '\n'
        << "Выражение с расставленными скобками: " << maxExpression << '\n'
        << "Максимальное значение выражения: " << maxData << '\n' << '\n';
}

// Удаляет лишние пробелы у выражения
void deleteSpaces(string& expression)
{
    // Удаление пробелов в строке
    for (int i = 0; i < expression.length(); i++)
    {
        if (expression[i] == ' ')
        {
            expression.erase(i, 1); // удаление
            i--; // уменьшаем индекс, чтобы не выйти за границы строки
        }
    }
}

// Проверяет выражения на корректность
void checkExpression(string expression, bool& isTrue)
{
    for (int i = 1; i < expression.length(); i++)
    {
        if (!isdigit(expression[i]) && expression[i] != '+' && expression[i] != '*') // проверяет допустимые символы
        {
            isTrue = false;
            break;
        }
        if ((expression[i] == '+' || expression[i] == '*') && !isdigit(expression[i + 1])) // проверяет двойные операнды
        {
            isTrue = false;
            break;
        }
    }
}

int main()
{
    setlocale(LC_ALL, "RUS");

    bool isTrue = true;
    string expression = "2 + 2 * 2"; // заданное выражение (пример: "1+2*3+4*5")

    cout << "Введите выражение для вычисления: ";
    getline(cin, expression);
    cout << '\n';

    deleteSpaces(expression);

    checkExpression(expression, isTrue);

    if (isTrue)
        printMaxValueExp(expression); // вычисляет и выводит максимальное значение выражения
    else
        cout << "Неверное выражения для вычисления." << '\n';

    return 0;
}