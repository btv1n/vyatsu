#include <iostream>
#include <vector>
#include <algorithm>
#include <map>
#include <set>

using namespace std;


/*
Условие задачи:

Алгоритмические стратегии
Реализовать задачу о рюкзаке жадным алгоритмом (нахождение оптимального решения не гарантируется).
В этой версии задачи о рюкзаке мы можем разбивать объекты на части (дробная задача о рюкзаке).
*/


/* 
Условие, когда нельзя делить предметы:

		if (arr[i].weight <= W) // если элемент полностью влезает в рюкзак, добавляем его целиком
		{
			W -= arr[i].weight;
			result_value += arr[i].value;
		}
		else
		{
			continue;
		}
*/


/*
Функция sort в данном коде используется для сортировки массива элементов Item по соотношению значение/вес.
в функции Knapsack вызывается функция sort, которая принимает в качестве параметров указатели на начало и 
конец массива arr, а также указатель на функцию Comparison. Эта функция сортирует элементы массива arr в порядке 
убывания соотношения значение/вес.
*/


// Структура элемента, в которой хранится вес и значение элемента
struct Item
{
	int value, weight;

	Item(int value, int weight)
	{
		this->value = value;
		this->weight = weight;
	}
};

// Функция сравнения для сортировки элементов по соотношению значение/вес
bool Comparison(struct Item a, struct Item b)
{
	double number_1 = (double)a.value / (double)a.weight;
	double number_2 = (double)b.value / (double)b.weight;
	return number_1 > number_2; // true | false
}

// Жадный алгоритм
double Knapsack(int W, struct Item arr[], int N)
{
	// Сортировка элементов по соотношению
	sort(arr, arr + N, Comparison); // сортируем весь массив (от и до), используя сравнение

	double result_value = 0.0;

	// Перебор всех элементов
	for (int i = 0; i < N; i++)
	{
		if (arr[i].weight <= W) // если элемент полностью влезает в рюкзак, добавляем его целиком
		{
			W -= arr[i].weight;
			result_value += arr[i].value;
		}
		else // если элемент не влезает в рюкзак целиком, то добавляем его дробную часть
		{
			// continue; // если нельзя разбивать предметы на части
			result_value += arr[i].value * ((double)W / (double)arr[i].weight);
			break;
		}
	}

	return result_value;
}

int main()
{
	setlocale(LC_ALL, "RUS");

	int W = 5; // вместимость рюкзака

	// объекты = значение/вес
	Item arr[] =
	{
		//{ 12, 3},
		{ 20, 1 }, // 20 / 1 = 20
		{ 38, 2 }, // 38 / 2 = 19
		{ 10, 3 },
		{ 29, 4 }
	};

	int N = sizeof(arr) / sizeof(arr[0]); // кол-во элементов в массиве

	std::cout << "Максимально возможное значение: " << Knapsack(W, arr, N);

	return 0;
}