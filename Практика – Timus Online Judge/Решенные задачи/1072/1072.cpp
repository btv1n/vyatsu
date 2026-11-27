#include <iostream>
#include <string>
#include <vector>
#include <queue>
#include <stack>
#include <cmath>
#include <algorithm>
#include <map>
#include <set>

using namespace std;

/*
scanf_s->scanf

#ifndef ONLINE_JUDGE
	freopen("T1072.in", "r", stdin);
	freopen("T1072.out", "w", stdout);
#endif
*/


// Объявление глобальных переменных 
int n, k, start, end_;
vector <vector <string>> inter;
vector <vector <int>> A;
vector <int> D, P;
queue <int> q;
stack <int> path;


// Функция для преобразования числа в двоичное представление
void trans(string& s, int n, int pos)
{
	for (int i = 128; i >= 1; i /= 2)
	{
		s[pos++] += n / i;
		n %= i;
	}
}


// Функция для выполнения операции побитового "и" над двумя строками
void AND(string& s, string& m)
{
	for (int i = m.size() - 1; i >= 0; --i)
	{
		if (m[i] == '0')
			s[i] = '0';
		else
			return;
	}
}


// Функция для ввода данных
//Функция input для ввода данных.Считываются данные о сетях и создается граф, где сети, имеющие общие узлы, соединены ребром.

void input(void)
{
	for (int i = 0; i < n; ++i)
	{
		scanf_s("%d\n", &k);
		for (int j = 0; j < k; ++j)
		{
			string s = "00000000000000000000000000000000";
			string m = "00000000000000000000000000000000";
			int t;

			scanf_s("%d.", &t);
			trans(s, t, 0);
			scanf_s("%d.", &t);
			trans(s, t, 8);
			scanf_s("%d.", &t);
			trans(s, t, 16);
			scanf_s("%d ", &t);
			trans(s, t, 24);

			scanf_s("%d.", &t);
			trans(m, t, 0);
			scanf_s("%d.", &t);
			trans(m, t, 8);
			scanf_s("%d.", &t);
			trans(m, t, 16);
			scanf_s("%d\n", &t);
			trans(m, t, 24);

			AND(s, m);

			inter[i].push_back(s);

			for (int j = i - 1; j >= 0; --j)
			{
				for (int l = 0; l < inter[j].size(); ++l)
				{
					if (s == inter[j][l])
					{
						A[i].push_back(j);
						A[j].push_back(i);
					}
				}
			}
		}
	}
	scanf_s("%d%d", &start, &end_);
	start--;
	end_--;
}


// Функция BFS для поиска кратчайшего пути в графе с помощью алгоритма обхода в ширину
void BFS(int u)
{
	D[u] = 1;
	q.push(u);
	while (!q.empty())
	{
		u = q.front();
		q.pop();
		for (int v = 0; v < A[u].size(); ++v)
		{
			if (!D[A[u][v]])
			{
				D[A[u][v]] = D[u] + 1;
				q.push(A[u][v]);
				P[A[u][v]] = u;
			}
		}
	}
}


// Функция восстановления пути - восстанавливает путь от начальной точки до конечной
void return_path(void)
{
	int u = end_;
	path.push(u);
	do
	{
		u = P[u];
		path.push(u);
	} while (u != start);
}


int main(void)
{
	scanf_s("%d\n", &n); // считывается количество сетей

	// Инициализация / выделение памяти для векторов
	inter.resize(n);
	A.resize(n);
	D.resize(n);
	P.resize(n);

	input(); // ввод данных

	BFS(start); // запуск алгоритма поиска кратчайшего пути

	// Если путь существует, выводится сообщение "Yes" и путь от стартовой до конечной точки. 
	// Иначе выводится сообщение "No"
	if (D[end_]) // Если до нужной вершины можно дойти
	{
		return_path();
		printf("Yes\n");
		while (!path.empty())
		{
			printf("%d ", path.top() + 1);
			path.pop();
		}
	}
	else // Если до нужной вершины нельзя дойти
	{
		printf("No");
	}

	return 0;
}
