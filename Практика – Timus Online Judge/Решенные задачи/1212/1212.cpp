#include <algorithm>
#include <iostream>
#include <vector>

// Определение структуры Ship, которая содержит координаты (x, y), длину корабля (k) и тип (t).
struct Ship 
{
    unsigned x;
    unsigned y;
    unsigned k;
    char t;
};

// Функция count принимает вектор позиций кораблей p и целое число k, 
// сортирует вектор, удаляет дубликаты и вычисляет количество возможных 
// расстановок кораблей с учетом расстояния k.
unsigned count(std::vector<unsigned>& p, unsigned k)
{
    std::sort(p.begin(), p.end()); // сортировка элементов от начала до конца
    p.erase(std::unique(p.begin(), p.end()), p.end()); // удаление дубликатов. unique - перемещает в конец ; erase - удаляет

    unsigned q = 0; // для подсчета результатов
    for (size_t i = 1; i < p.size(); ++i) // перебор элементов вектора p
    {
        const unsigned d = p[i] - p[i - 1] - 1; // разница между текущим и предыдущим элементом вектора p
        if (d >= k) // проверка, больше ли эта разница заданного значения
            q += d - k + 1; // добавляем в q
    }
    return q;
}

// Выполняет подсчет возможных расстановок кораблей вдоль строк и столбцов поля
unsigned solve(unsigned n, unsigned m, const std::vector<Ship>& s, unsigned k)
{
    unsigned v = 0;

    for (unsigned y = 1; y <= n; ++y)  // цикл по всем строкам игрового поля
    {
        std::vector<unsigned> p = { 0, m + 1 };
        for (const Ship& q : s) // для каждого корабля
        {
            switch (q.t) {
            case 'H': // горизонтальный
                if (q.y >= y - 1 && q.y <= y + 1) // дипазон координаты у
                {
                    p.push_back(q.x - 1);
                    for (unsigned i = 0; i <= q.k; ++i)
                        p.push_back(q.x + i);
                }
                break;
            case 'V': // вертикальный
                if (q.y <= y + 1 && q.y + q.k >= y) {
                    p.push_back(q.x - 1);
                    p.push_back(q.x);
                    p.push_back(q.x + 1);
                }
                break;
            }
        }

        v += count(p, k); // считаем количество кораблей в векторе p
    }

    for (unsigned x = 1; x <= m; ++x) // цикл по всем столбцам игрового поля
    {
        std::vector<unsigned> p = { 0, n + 1 };
        for (const Ship& q : s)  // для каждого корабля
        {
            switch (q.t) {
            case 'H': // горизонтальный корабль
                if (q.x <= x + 1 && q.x + q.k >= x) // координата находится в пределах
                {
                    p.push_back(q.y - 1);
                    p.push_back(q.y);
                    p.push_back(q.y + 1);
                }
                break;
            case 'V': // вертикальный корабль
                if (q.x >= x - 1 && q.x <= x + 1) {
                    p.push_back(q.y - 1);
                    for (unsigned i = 0; i <= q.k; ++i)
                        p.push_back(q.y + i);
                }
                break;
            }
        }

        v += count(p, k); // считаем количество кораблей в векторе p
    }

    return k == 1 ? v / 2 : v; // если значение равно 1 -Ю v/2
}

int main()
{
    std::cin.tie(nullptr)->sync_with_stdio(false);

    unsigned n, m, q;
    std::cin >> n >> m >> q; // Считывает значения n (количество строк поля), m (количество столбцов поля) и q (количество кораблей).

    std::vector<Ship> s(q);
    for (unsigned i = 0; i < q; ++i)
        std::cin >> s[i].x >> s[i].y >> s[i].k >> s[i].t;

    unsigned k;
    std::cin >> k; // Считывает значение k(расстояние между кораблями).

    std::cout << solve(n, m, s, k) << '\n';

    return 0;
}
