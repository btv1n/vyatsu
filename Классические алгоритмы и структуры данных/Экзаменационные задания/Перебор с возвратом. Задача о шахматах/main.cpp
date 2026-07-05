#include <iostream>
#include <vector>
#include "math.h"

using namespace std;

/*
Условие задачи:

Перебор с возвратом
По заданным числам n и k определить все способы, которыми можно расставить k слонов
на шахматной доске размера n×n так, чтобы никакие два из них не били друг друга.
*/


bool check(int n, int row, int col, int** board)
{
    // Проверка на атаку по левой верхней диагонали
    for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
    {
        if (board[i][j] == 1)
        {
            return false;
        }
    }

    // Проверка на атаку по левой нижней диагонали
    for (int i = row, j = col; i < n && j >= 0; i++, j--)
    {
        if (board[i][j] == 1)
        {
            return false;
        }
    }

    // Проверка на атаку по правой верхней диагонали
    for (int i = row, j = col; i >= 0 && j < n; i--, j++)
    {
        if (board[i][j] == 1)
        {
            return false;
        }
    }

    // Проверка на атаку по правой нижней диагонали
    for (int i = row, j = col; i < n && j < n; i++, j++)
    {
        if (board[i][j] == 1)
        {
            return false;
        }
    }

    return true;
}

void solve(int n, int k, int row, int col, int count, int& countPosition, int** board)
{
    // Если разместили k слонов, выводим доску
    if (count == k)
    {
        countPosition += 1; // Увеличиваем счетчик расстановок на единицу

        //// Выводит доску с расстановкой слонов
        //for (int i = 0; i < n; i++)
        //{
        //    for (int j = 0; j < n; j++)
        //    {
        //        cout << board[i][j] << " ";
        //    }
        //    cout << endl;
        //}
        //cout << endl;

        return;
    }

    // Перебираем все возможные позиции для слона
    for (int i = row; i < n; i++) // Цикл по строкам пока не достигнем последней строки
    {
        for (int j = col; j < n; j++) // Цикл по столбцам пока не достигнем последнего столбца
        {
            if (check(n, i, j, board)) // Проверяем можно ли поставить слона в это поле т.е. не находится ли поле под боем других слонов
            {
                board[i][j] = 1; // Размещаем слона

                solve(n, k, i, j, count + 1, countPosition, board); // Рекурсивно вызываем функцию

                board[i][j] = 0; // Убираем слона (возврат)
            }
        }
    }
}

int main()
{
    setlocale(LC_ALL, "RUS");

    int countPosition = 0; // Счетчик количества всех возможных расстановок
    int n, k;

    cout << "Введите сначала n, а потом k: ";
    cin >> n >> k;

    // Создание двумерного поля
    int** board = new int* [n];
    for (int i = 0; i < n; i++)
    {
        board[i] = new int[n];
    }

    // Заполнение всех клеток поля нулями
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            board[i][j] = 0;
        }
    }

    // row, col - определяют позицию начала перебора ; count - счетчик поставленных фигур
    int row = 0, col = 0, count = 0;

    solve(n, k, row, col, count, countPosition, board); // Вызов функции для размещения слонов

    cout << "Количество возможных расстановок: " << countPosition;


    // Очистка памяти, удаление массива
    for (int i = 0; i < n; i++)
    {
        delete[] board[i];
    }
    delete[] board;

    return 0;
}