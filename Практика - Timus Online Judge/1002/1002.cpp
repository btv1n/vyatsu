#include <iostream>
#include <string>
#include <map> // контейнер map
#include <vector>
#include <queue>
#include <stack>

using namespace std;

string num, word;
int n;
map <char, char> alphabet;
vector <int> par;
vector <int> d;
vector <string> dict;
vector <vector <int> > A;
queue <int> q;
stack <int> r;


// Инициализация алфавита
void init_alphabet(void)
{
	alphabet['a'] = '2';
	alphabet['b'] = '2';
	alphabet['c'] = '2';
	alphabet['d'] = '3';
	alphabet['e'] = '3';
	alphabet['f'] = '3';
	alphabet['g'] = '4';
	alphabet['h'] = '4';
	alphabet['k'] = '5';
	alphabet['l'] = '5';
	alphabet['m'] = '6';
	alphabet['n'] = '6';
	alphabet['p'] = '7';
	alphabet['r'] = '7';
	alphabet['s'] = '7';
	alphabet['t'] = '8';
	alphabet['u'] = '8';
	alphabet['v'] = '8';
	alphabet['w'] = '9';
	alphabet['x'] = '9';
	alphabet['y'] = '9';
	alphabet['o'] = '0';
	alphabet['q'] = '0';
	alphabet['z'] = '0';
	alphabet['i'] = '1';
	alphabet['j'] = '1';
}


// Инициализация объектов программы
void init(void)
{
	dict.clear();   // очистка вектора dict
	dict.resize(n); // изменение размера вектора dict

	par.clear();
	par.resize(num.size() + 1);

	d.clear();
	d.resize(num.size() + 1);

	A.clear();
	A.resize(num.size() + 1);
	for (int i = 0; i < A.size(); ++i)
	{
		A[i].resize(num.size() + 1);
	}
}


// Проверка
void test(int k, int n)
{
	for (int i = k; i < k + dict[n].size(); ++i) // цикл от k до k + размер слова
	{
		if (num[i] != alphabet[dict[n][i - k]]) // если символы не совпадают, выход из функции
			return;
	}
	A[k][k + dict[n].size()] = n + 1; // заполнение матрицы A значением
}


// Поиск соответствия кратчайших слов номеру телефона. Алгоритм BFS(Breadth - First Search) — алгоритм поиска в ширину
void BFS(int u)
{
	d[u] = 1;  // инициализация d
	q.push(u); // добавление u в очередь

	while (!q.empty()) // пока очередь не пуста
	{
		u = q.front(); // получение элемента из очереди
		q.pop();       // удаление элемента из очереди

		for (int v = 0; v <= num.size(); ++v) // цикл от 0 до размера строки
		{
			if (!d[v] && A[u][v]) // если v не посещена и есть путь
			{
				q.push(v);       // добавление v в очередь
				d[v] = d[u] + 1; // увеличение расстояния
				par[v] = u;      // установка предыдущего узла
			}
		}
	}
}


// Вывод значения или сообщения об отсутствии решения
void print(void)
{
	int u = num.size();

	if (d[u] != 0)
	{
		while (u != 0)
		{
			r.push(u);
			u = par[u];
		}
		while (u != num.size())
		{
			int v = r.top();
			r.pop();
			cout << dict[A[u][v] - 1] << ' ';
			u = v;
		}
		printf("\n");
	}
	else
		printf("No solution.\n");
}


int main(void)
{
#ifndef ONLINE_JUDGE
	// Открытие файлов для ввода и вывода
	freopen("T1002.in", "r", stdin);
	freopen("T1002.out", "w", stdout);
#endif

	init_alphabet(); // инициализация алфавита


	while (getline(cin, num) && num != "-1") // считывание значений, пока не встречено "-1"
	{
		scanf("%d\n", &n); // считывание значения n
		init();            // инициализация объектов

		for (int i = 1; i <= n; ++i) // цикл по словарю
		{
			getline(cin, word); // считываем строки
			dict[i - 1] = word; // записываем строки в словарь

			for (int j = 0; j < num.size(); ++j) // цикл по символам строки
			{
				if (num[j] == alphabet[word[0]] && j + word.size() - 1 < num.size()) // если символы совпадают и слово помещается
					test(j, i - 1); // проверка слова
			}
		}

		BFS(0);  // поиск кратчайшего слова
		print(); // вывод результата
	}

	return 0;
}