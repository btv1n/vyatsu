#include <iostream>
#include <cstdio>

using namespace std;

// scanf_s -> scanf

int main()
{
    int N; // кол-во цифр
    int p; // число
    double sum = 0;

    scanf_s("%d", &N);
    for (int i = 0; i < N; i++)
    {
        scanf_s("%d", &p);
        sum += (double)p;
    }

    sum = sum / (double)N;
    printf("%.6lf", sum); // выводим 6 знаков после запятой
    return 0;
}
