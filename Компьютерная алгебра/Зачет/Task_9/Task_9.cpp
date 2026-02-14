#include <iostream>
#include <string>
#include <cstdio>
using namespace std;

struct bigint {
    // Одно деление зранит число макс 9 цифр
    static const long long BASE = 1e9;

    // Максимум 10 блоков по 9 цифр
    static const long long SIZE = 10;

    // Хранит число. digits[0] - младшие 9 цифр ...
    long long digits[SIZE];

    // Коструктор без параментров
    bigint() {
        for (int i = 0; i < SIZE; i++) 
            digits[i] = 0;
    }

    // Конструктор long long
    /*
    * bigint a(1234567890123);
		
    BASE = 1 000 000

    digits[0] = 567890123

    1234567890123 / BASE = 1234
    1234 % BASE = 1234

    digits[1] = 1234
    */
    bigint(long long x) {
        for (int i = 0; i < SIZE; i++) digits[i] = 0;
        int next = 0;
        while (x) {
            digits[next++] = x % BASE;
            x /= BASE;
        }
    }

    bigint operator+(const bigint& other) const {
        bigint result = *this; // *this - A. Копия текущего левого операнда: A+B
        // Сложение блоков
        for (int i = 0; i < SIZE; i++) 
            result.digits[i] += other.digits[i];
        // Перенос разрядов. Если блок >= ВASE - он переполнен, переносим 1 в следуюищй блок.
        for (int i = 0; i < SIZE - 1; i++) {
            if (result.digits[i] >= BASE) {
                result.digits[i] -= BASE; // остаток
                result.digits[i + 1]++; // переносим 1 в старший разряд
            }
        }
        return result;
    }
    /* BASE = 1000
    A = 1999
    B = 5

    A:
    digits[0] = 999
    digits[1] = 1

    B:
    digits[0] = 5
    digits[1] = 0

    СЛОЖЕНИЕ БЛОКОВ
    digits[0] = 999 + 5 = 1004
    digits[1] = 1 + 0 = 1

    ПЕРЕНОС
    digits[0] = 1004 - 1000 = 4
    digits[1] = 1 + 1 = 2

    ВЫВОД
    digits[0] = 4
    digits[1] = 2
    2 * 1000 + 4 = 2004
    */

    bigint operator-(const bigint& other) const {
        bigint result = *this; // копия левого операнда
        // Вычитание блоков
        for (int i = 0; i < SIZE; i++)
            result.digits[i] -= other.digits[i];

        // Обработка взаиствования
        for (int i = 0; i < SIZE - 1; i++) {
            if (result.digits[i] < 0) {
                result.digits[i] += BASE;
                result.digits[i + 1]--; // уменьшение старшего разряда
            }
        }
        return result;
    }
    /*
    BASE = 1000
    A = 1000
    B = 1

    A:
    digits[0] = 0
    digits[1] = 1

    B:
    digits[0] = 1
    digits[1] = 0

    ПОСЛЕ ВЫЧИТАНИЯ
    digits[0] = 0 - 1 = -1
    digits[1] = 1 - 0 = 1

    ПОСЛЕ ЗАИМСТВОВАНИЯ
    digits[0] = -1 + 1000 = 999
    digits[1] = 1 - 1 = 0

    ВЫВОД
    999
    */


    bigint operator*(const bigint& other) const {
        bigint result;

        // Перемножаем каждый блок с каждым
        for (int i = 0; i < SIZE; i++) {
            for (int j = 0; j < SIZE - i; j++) {
                // i-й блок первого числа * j-й блок второго числа
                result.digits[i + j] += digits[i] * other.digits[j]; 
            }
        }
        // Перенос разрядов
        for (int i = 0; i < SIZE - 1; i++) {
            result.digits[i + 1] += result.digits[i] / BASE; // перенос
            result.digits[i] %= BASE; // остаток
        }
        return result;
    }
    /*
    BASE = 100

    123 * 45
    123 = [23, 1]
    45 = [45]

    УМНОЖЕНИЕ
    23*45 = 1035 - result[0]
    1 * 45 = 45 - result[1]

    ПОСЛЕ ПЕРЕНОСА
    result[0] = 35
    result[1] = 55
    */
};

ostream& operator<<(ostream& out, const bigint& num) {
    string result; // строка в которую собирается число
    char buffer[10]; // макс 9 цифр + '\0'
    // С конца массива
    for (int i = bigint::SIZE - 1; i >= 0; i--) {
        sprintf_s(buffer, "%09lld", num.digits[i]); // 123 -> 000000123
        result += buffer;
    }
    int first_idx = result.find_first_not_of('0'); // убирает ведущие нули
    if (first_idx == string::npos) // число полностью из нулей ; string::npos - не найдено, не верная позиция
        out << "0";
    else 
        out << result.substr(first_idx); // выводит без ведущих нулей
    return out;
}

int main() {
    // Создает числа
    bigint a(9060173);
    bigint b(9028091);
    bigint c(1450140);
    bigint d(2732940);
    bigint e(560099730);

    // Вычисляет выражение
    bigint result = a * b - c * d + e;

    // Вывод
    cout << "9060173 * 9028091 - 1450140 * 2732940 + 560099730 = " << result << endl;

    return 0;
}