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

    result[0] = arr[0];
    for (int i = 1; i < N; i++)
        for (int j = 0; j < i + 1; j++)
            result[i] += arr[j];

    /* for (int i = 0; i < N; i++)
        std::cout << result[i] << " ";
    std::cout << std::endl; */
}