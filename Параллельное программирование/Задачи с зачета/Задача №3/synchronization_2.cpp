#include <iostream>
#include <vector>

const int N = 500000;

using namespace std;

int main()
{
    srand(0);
    int *arr = new int[N];
    int *result = new int[N];
    for (int i = 0; i < N; i++)
    {
        arr[i] = rand() % 100;
        result[i] = 0;
    }

    for (int i = 1; i / 2 < N; i *= 2) {
        for (int j = 0; j < N; j++)
            if (j < i)
                result[j] = arr[j];
            else
                result[j] = arr[j] + arr[j - i];
        std::swap(result, arr);
    }
}