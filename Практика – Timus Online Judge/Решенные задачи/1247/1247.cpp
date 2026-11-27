#include <iostream>
#include <vector>
#include <algorithm>

using namespace std; 

// Проверяем является ли заданная последовательность чисел корректной. Суммируем элементы по порядку в обе стороны и
// и проверяем, сумма < i+1 - если условие нарушено хотя бы для одного элемента -> NO

int main()
{
    int s, n;
    cin >> s >> n;

    vector<int> data(s);

    for (int i = 0; i < s; i++) // записываем элементы последовательности в вектор
        cin >> data[i];

    int sum = 0;
    for (int i = 0; i < s; i++) // цикл для вычисления суммы элементов вектора
    {
        sum += data[i];
        if (sum < (i + 1)) // если сумма на i-м шаге меньше, чем i + 1 -> выводим "NO"
        {
            cout << "NO" << endl;
            return 0;
        }
    }

    sum = 0; // обнуляем переменную sum
    for (int i = 0; i < s; i++) // цикл для вычисления суммы элементов вектора в обратном порядке
    {
        sum += data[s - 1 - i];
        if (sum < (i + 1))
        {
            cout << "NO" << endl;
            return 0;
        }
    }

    cout << "YES" << endl; // проверки пройдены успешно

    return 0;
}
