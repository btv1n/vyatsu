#include <iostream>
#include <string>
#include <map> // ��������� map
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


// ������������� ��������
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


// ������������� �������� ���������
void init(void)
{
	dict.clear();   // ������� ������� dict
	dict.resize(n); // ��������� ������� ������� dict

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


// ��������
void test(int k, int n)
{
	for (int i = k; i < k + dict[n].size(); ++i) // ���� �� k �� k + ������ �����
	{
		if (num[i] != alphabet[dict[n][i - k]]) // ���� ������� �� ���������, ����� �� �������
			return;
	}
	A[k][k + dict[n].size()] = n + 1; // ���������� ������� A ���������
}


// ����� ������������ ���������� ���� ������ ��������. �������� BFS(Breadth - First Search) � �������� ������ � ������
void BFS(int u)
{
	d[u] = 1;  // ������������� d
	q.push(u); // ���������� u � �������

	while (!q.empty()) // ���� ������� �� �����
	{
		u = q.front(); // ��������� �������� �� �������
		q.pop();       // �������� �������� �� �������

		for (int v = 0; v <= num.size(); ++v) // ���� �� 0 �� ������� ������
		{
			if (!d[v] && A[u][v]) // ���� v �� �������� � ���� ����
			{
				q.push(v);       // ���������� v � �������
				d[v] = d[u] + 1; // ���������� ����������
				par[v] = u;      // ��������� ����������� ����
			}
		}
	}
}


// ����� �������� ��� ��������� �� ���������� �������
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
	// �������� ������ ��� ����� � ������
	freopen("T1002.in", "r", stdin);
	freopen("T1002.out", "w", stdout);
#endif

	init_alphabet(); // ������������� ��������


	while (getline(cin, num) && num != "-1") // ���������� ��������, ���� �� ��������� "-1"
	{
		scanf("%d\n", &n); // ���������� �������� n
		init();            // ������������� ��������

		for (int i = 1; i <= n; ++i) // ���� �� �������
		{
			getline(cin, word); // ��������� ������
			dict[i - 1] = word; // ���������� ������ � �������

			for (int j = 0; j < num.size(); ++j) // ���� �� �������� ������
			{
				if (num[j] == alphabet[word[0]] && j + word.size() - 1 < num.size()) // ���� ������� ��������� � ����� ����������
					test(j, i - 1); // �������� �����
			}
		}

		BFS(0);  // ����� ����������� �����
		print(); // ����� ����������
	}

	return 0;
}