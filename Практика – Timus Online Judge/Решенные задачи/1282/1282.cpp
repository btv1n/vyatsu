#include <iostream>
#include <vector>

using namespace std;

// Структура данных
struct Node
{
    vector<size_t> subnodes;
    int value = -1;
};


// Объявление функции solve, которая рекурсивно обходит граф g, начиная с узла x, с учетом параметра m.
// Рекурсивно проходим по дереву, начиная с узла x, и находим оптимальное значение value с учетом переменной m. 
// Узлы дерева представлены структурой Node, которая содержит значение узла и список подузлов subnodes.
int solve(vector<Node>& g, size_t x, int m)
{
    if (g[x].subnodes.empty()) // если у текущего узла нет подузлов 
        return g[x].value; // значение узла

    // если у текущего узла есть подузлы - рекурсивно обрабатываем их
    int value = -m;
    for (const size_t v : g[x].subnodes) 
    {
        const int t = solve(g, v, -m);
        value = m < 0 ? min(value, t) : max(value, t);
    }
    return value;
}


int main()
{
    cin.tie(nullptr)->sync_with_stdio(false); // ускорение ввода-вывода

    size_t n;
    cin >> n;

    vector<Node> g(n); // создание вектора g размера n для хранения узлов графа
    for (size_t i = 1; i < n; ++i) // цикл для заполнения графа g и его узлов subnodes и value.
    {
        char t;
        cin >> t;

        unsigned p;
        cin >> p;

        if (t == 'L')
            cin >> g[i].value;

        g[p - 1].subnodes.push_back(i);
    }

    constexpr const char* s[3] = { "-1", "0", "+1" }; // constexpr позволяет оптимизировать выполнение программы
    cout << s[solve(g, 0, 1) + 1] << '\n'; // вывод на экран значения из массива s в зависимости от результата solve.

    return 0;
}
