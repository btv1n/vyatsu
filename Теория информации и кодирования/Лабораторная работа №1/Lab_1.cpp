#include <iostream>
#include <vector>
#include <string>
#include <exception>
#include <set>
#include <map>
#include <math.h>
#include <algorithm>

using namespace std;


// Представлен в виде словаря: alphabet = { символ алфавита, индекс }
map<string, int> alphabet;        // символ -> индекс
map<int, string> reverse_alphabet; // индекс -> символ
int WORD_SIZE = 0; // WORD_SIZE = n - длина слова (количество симолов в слове
int BASE = 0;      // BASE = k - количество символов в алфавите

/*
Задание: Дан алфавит из k символов. Рассмотрим множество всех слов длины n над этим алфавитом и лексикографический порядок на этом множестве.
Разработать программу, позволяющую выполнить следующие действия.

1. Задать n и k и ввести символы алфавита.
2. Найти номер введённого пользователем слова.
3. Найти слово по введённому пользователем номеру.
4. Найти расстояние между двумя введёнными пользователем словами.
5. Найти слово w2, которое находится на заданном пользователем расстоянии r от введённого пользователем слова w1.

Программа должна быть написана C++, должна иметь меню выбора действий и «защиту от дурака».
Задания не должны быть выполнены путем прямолинейной генерации всех слов из алфавита, а более эффективно.
*/

/*
A = {0,2,4,5,7}
n = 3
k = 5

1)
0: 000
1: 002
2: 004
3: 005
4: 007
5: 020
6: 022
7: 024
8: 025
Вывод позиции числа по номеру: 025 -> 8

2) Вывод вычисла по введенной позиции: 8 -> 025

3) Найти расстояние между числами:
002
024
-> Расстояние 6

4) Вывод числа, прибавленной позицией
002
6
-> 024
*/


void inputAlphabet(int k);

void printAlphabet(const map<string, int>& alphabet);

bool checkNumber(string input);

int inputChekedNumber();

int inputChekedNumberForBase();

int WordFromNumber(string w);

string convertToBase(int x, int base);

string inputString(int WORD_SIZE);

string NumberFromWord(int n);

void printWordInterval(string w1, string w2);

void printNumberInterval(string w1, int r);



int main()
{
    setlocale(LC_ALL, "RUS");

    int number = 0,
        number1 = 0,
        number2 = 0;

    string input = "",
        word1 = "",
        word2 = "";


    // ===== Ввод =====

    std::cout << "n (WORD_SIZE: длина множества всех слов):\n";
    WORD_SIZE = inputChekedNumber();
    std::cout << endl;

    std::cout << "k (BASE: символов алфавита):\n";
    //std::cout << "Длина алфавита: " << alphabet.size() << std::endl;
    BASE = inputChekedNumberForBase();
    std::cout << endl;

    // Ввод алфавита
    inputAlphabet(BASE);


    // ===== Вывод n, k и алфавита =====

    std::cout << endl;
    std::cout << "n = " << WORD_SIZE << std::endl;
    std::cout << "k = " << BASE << std::endl;
    printAlphabet(alphabet);
    std::cout << endl;


    // ===== Меню программы =====

    std::cout << "1: Найти номер введённого пользователем слова\n"
        << "2: Найти слово по введённому пользователем номеру\n"
        << "3: Найти расстояние между двумя введёнными пользователем словами\n"
        << "4: Найти слово w2, которое находится на заданном пользователем расстоянии r от введённого пользователем слова w1\n"
        << "5: Выход из программы\n"
        << std::endl;

    int start = 0;
    bool flag = 0;

    int maxNumber = pow(BASE, WORD_SIZE) - 1;
    while (!flag)
    {
        //std::cout << "Введите команду: ";
        start = inputChekedNumber();

        switch (start)
        {
        case 1:
            std::cout << "Введите слово: ";
            word1 = inputString(WORD_SIZE);
            if (word1 != "-1")
                std::cout << WordFromNumber(word1) << std::endl;
            std::cout << endl;
            break;
        case 2:
            number = inputChekedNumber();
            // Проверка числа на выход из диапазона
            if (number < 0 || number > maxNumber)
            {
                cout << "Ошибка: выход за пределы множества слов!" << endl;
            }
            else
            {
                cout << NumberFromWord(number) << std::endl;
                std::cout << std::endl;
            }
            break;
        case 3:
            std::cout << "Введите граничные слова: ";
            word1 = inputString(WORD_SIZE);
            word2 = inputString(WORD_SIZE);
            if (word1 != "-1" && word2 != "-1")
            {
                printWordInterval(word1, word2);
                std::cout << endl;
            }
            break;
        case 4:
            std::cout << "Введите граничное слово алфавита: ";
            word1 = inputString(WORD_SIZE);
            std::cout << "Введите расстояние r: ";
            number = inputChekedNumber();
            printNumberInterval(word1, number);
            std::cout << endl;
            break;
        case 5:
            std::cout << "Завершение программы." << endl;
            flag = 1;
            break;
        default:
            std::cout << "Ошибка ввода" << std::endl;
        }
    }


    // ===== Итоговый вывод n, k и алфавита =====

    std::cout << endl;
    std::cout << "n = " << WORD_SIZE << std::endl;
    std::cout << "k = " << BASE << std::endl;
    printAlphabet(alphabet);
}


// Обработчик ввода строки
string inputString(int WORD_SIZE)
{
    string word;
    cin >> word;

    // Проверка длины
    if (word.length() != WORD_SIZE)
    {
        cout << "Ошибка: неверная длина слова!" << endl;
        return "-1";
    }

    // Проверка: только цифры
    if (!all_of(word.begin(), word.end(), ::isdigit))
    {
        cout << "Ошибка: можно вводить только цифры!" << endl;
        return "-1";
    }

    // Проверка: символы есть в алфавите
    for (char c : word)
    {
        string s(1, c);
        if (!alphabet.count(s))
        {
            cout << "Ошибка: символа " << c << " нет в алфавите!" << endl;
            return "-1";
        }
    }

    return word;
}

// Ввод алфавита
void inputAlphabet(int k)
{
    alphabet.clear();
    reverse_alphabet.clear();

    cout << "Введите символы алфавита:" << endl;

    for (int i = 0; i < k; i++)
    {
        string symbol;
        cout << "Символ " << i << ": ";
        cin >> symbol;

        // защита от повторов
        if (alphabet.count(symbol))
        {
            cout << "Такой символ уже есть! Повторите ввод." << endl;
            i--;
            continue;
        }

        // Проверка на число
        if (symbol.length() != 1 || !isdigit(symbol[0]))
        {
            cout << "Ошибка! Введите одну цифру от 0 до 9." << endl;
            i--;
            continue;
        }

        alphabet[symbol] = i;
        reverse_alphabet[i] = symbol;
    }
}

// Вывод алфавита
void printAlphabet(const map<string, int>& alphabet)
{
    cout << "Алфавит:" << endl;
    for (const auto& element : alphabet)
    {
        cout << "Символ: " << element.first
            << " -> Индекс: " << element.second << endl;
    }
}

// Проверка числа: молжно ли строку преобразовать в целое число
bool checkNumber(string input)
{
    try
    {
        int number = std::stoi(input);
        return true;
    }
    catch (const std::invalid_argument& e) // если строка не число
    {
        std::cout << "Неверный формат числа" << std::endl;
        return false;
    }
    catch (const std::exception& e) // прочие возможные ошибки
    {
        std::cout << "Ошибка" << e.what() << std::endl;
        return false;
    }
}

// Ввод проверенного числа: запрашивает число у пользователя, пока ввод не станет корректным
int inputChekedNumber()
{
    int number;
    string input;
    do
    {
        std::cout << "Введите число: ";
        std::cin >> input;
    } while (!checkNumber(input));

    number = stoi(input);

    return abs(number);
}

// Ввод проверенного числа: запрашивает число у пользователя, пока ввод не станет корректным
// Для Base, количество символов в алфавите не может быть больше 9
int inputChekedNumberForBase()
{
    int number;
    string input;
    do
    {
        std::cout << "Введите число: ";
        std::cin >> input;
    } while (!checkNumber(input));

    number = stoi(input);

    if (number > 9)
    {
        std::cout << "Количество символом в алфавите не может превышать 9.\nПрисвоено значение 9";
        number = 9;
    }


    return abs(number);
}

// Конвертет в систему исчисления: переводит число x в систему счисления base
// (number: 5, base: 3) -> "12"
string convertToBase(int x, int base)
{
    string result = "";
    int ost; // переменная для хранения остатка: = x % base

    // Пока число больше 0
    while (x > 0)
    {
        // Получаем остаток от деления на base - последния цифра в новой системе счисления
        ost = x % base;

        // Делим число на base - переходим к следующему разряду
        x /= base;

        // Добавляет цифру в начало строки - в позицию 0
        result.insert(0, to_string(ost));
    }
    return result;
}

// Пункт 1: получение номера по слову
// Перевод слова из системы счисления с основанием base в десятичную систему
int WordFromNumber(string w)
{
    // Если длина не равна WORD_SIZE - ошибка
    if (w.length() != WORD_SIZE)
        return -1;

    // Для хранения итогового номера
    int number = 0;

    for (int i = 0; i < WORD_SIZE; i++)
    {
        // char -> string
        string symbol(1, w[i]);

        // Проверка наличия символа с алфавите
        if (!alphabet.count(symbol))
        {
            cout << "Символа нет в алфавите!" << endl;
            return -1;
        }

        // Формула перевода: 0 *5^2 + 1 *5^1 + 3 *5^0 = 0 + 5 + 3 = 8 (13) BASE = 5
        number = number * BASE + alphabet[symbol];
    }

    return number;
}

// Пункт 2: получение слова по номеру
// Перевод числа n из десятичной системы в систему счисления с основанием BASE,
// а затем замена символов на символы алфавита
string NumberFromWord(int n)
{
    string result = "";

    // Перевод числа
    while (n > 0)
    {
        // Получение остатка от деления
        int remainder = n % BASE;

        // По индексу получаем соответствующий символ алфавита и добавлем его в начало строки
        // потому что остатки с конца числа
        result = reverse_alphabet[remainder] + result;

        // Делим число на основание - переходим к следующему разряду
        n /= BASE;
    }

    // Если длина слова меньше WORD_SIZE, добавляем первый символ алфавита слева
    while (result.length() < WORD_SIZE)
        result = reverse_alphabet[0] + result;

    return result;
}

// Пункт 3: вывод слов в заданном интервале
void printWordInterval(string w1, string w2)
{
    // Получение номеров слов
    int n1 = WordFromNumber(w1);
    int n2 = WordFromNumber(w2);

    // Проверка числа
    if (n1 == -1 || n2 == -1)
    {
        cout << "Ошибка: некорректное слово." << endl;
        return;
    }

    // Если не верный порядок - меняем местами
    if (n1 > n2) swap(n1, n2);

    // Выводит все слова в диапазоне
    for (int N = n1; N <= n2; N++)
    {
        cout << N << ") " << NumberFromWord(N) << endl;
    }

    std::cout << "Расстояние между двумя словами: " << abs(n1 - n2) << std::endl;
}

// Пункт 4: вывод слов по числовому интервалу
void printNumberInterval(string w1, int r)
{
    // Преобразуем слово в номер
    int n1 = WordFromNumber(w1);

    if (n1 == -1)
    {
        cout << "Ошибка: некорректное слово!" << endl;
        return;
    }

    // Прибавляем расстояние
    int newNumber = n1 + r;

    // Проверка выхода за пределы множества всех слов
    int maxNumber = pow(BASE, WORD_SIZE) - 1;

    if (newNumber < 0 || newNumber > maxNumber)
    {
        cout << "Ошибка: выход за пределы множества слов!" << endl;
        return;
    }

    // Переводим обратно в слово
    cout << "w2: " << NumberFromWord(newNumber) << endl;
}