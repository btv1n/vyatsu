#include <iostream>
#include <limits.h>

using namespace std;

int main()
{
    int n, k; // n - количество городов, k - количество городов, в которых уже есть электростанции
    cin >> n >> k;
    const int N = 100;
    int powerstation[N + 1]; // информация о наличии электростанций
    int graph[N + 1][N + 1]; // граф связей между городами
    int vertices[N + 1], processed[N + 1]; // обработанные вершины


    // Предположим, что ни в одном городе нет электростанции
    for (int i = 0; i <= n; i++)
        powerstation[i] = 0, vertices[i] = INT_MAX, processed[i] = 0;


    // Ввод номеров городов с электостанциями
    for (int i = 0, temp = 0; i < k; i++)
    {
        cin >> temp;
        powerstation[temp] = 1;
    }


    // Получение входных данных -> Заполнение таблицы {Cij} связей между городами
    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= n; j++)
        {
            cin >> graph[i][j];
        }

    }


    // Маршруты между электростанциями установлены на ноль, потому что на самом деле они
    // не требуются, поэтому не увеличивают стоимость.
    // Устанавливаются маршруты между электростанциями на ноль
    for (int i = 1; i <= n; i++)
    {
        if (powerstation[i] == 1)
        {
            for (int j = 1; j <= n; j++)
            {
                if (powerstation[j] == 1)
                    graph[i][j] = 0;

                if (i == j)
                    graph[i][j] = INT_MIN;
            }
        }
    }


    // Находится минимальное остовное дерево (MST) с использованием алгоритма Прима.
    // Строит остовное дерево с минимальным весом вершин. Остовное дерево используется для поиска
    // оптимального пути или связи между вершинами.
    vertices[1] = 0;
    for (int i = 1, temp = INT_MAX, iter = 0; i < n; i++)
    {

        // Нахождение вершины с минимальным весом
        for (int j = 1; j <= n; j++)
        {
            if (vertices[j] < temp && processed[j] == 0)
                temp = vertices[j], iter = j;

        }

        processed[iter] = 1;

        // Обновление массива вершин
        for (int j = 1; j <= n; j++)
        {
            if (processed[j] == 0)
                if (graph[iter][j] >= 0 && graph[iter][j] < vertices[j])
                    vertices[j] = graph[iter][j];
        }

        temp = INT_MAX;
    }


    // Находится общая стоимость электрификации всех городов и выводится на экран
    int u = 0;
    for (int i = 1; i <= n; i++)
        u += vertices[i];
    cout << u;


    return 0;
}
