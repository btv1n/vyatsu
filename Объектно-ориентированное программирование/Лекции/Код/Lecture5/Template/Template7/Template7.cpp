#include <iostream>
#include <algorithm>
#include <ctime>
#include <chrono>
#include "Quicksort.h"

using namespace std;

int main()
{
    size_t size = 5000;
    int* a = new int[size];
    for (size_t i = 0; i < size; i++)
        a[i] = rand() % 100;
    Quicksort<int> qsort;
    // Вычисление времени выполнения алгоритма ctime
    double start = clock();
    qsort.my_sort(a, a + size);

    double finish = clock();
    cout << (finish - start) / CLOCKS_PER_SEC << endl;


    int* b = new int[size];
    for (size_t i = 0; i < size; i++)
        b[i] = rand() % 100;
    // Вычисление времени выполнения алгоритма chrono
    auto begin = std::chrono::steady_clock::now();
    qsort.my_sort(b, b + size);
    auto end = std::chrono::steady_clock::now();

    auto elapsed_ms = std::chrono::duration_cast<std::chrono::milliseconds>(end - begin);
    std::cout << "The time: " << elapsed_ms.count() << " ms\n";
    
    auto elapsed_ns = std::chrono::duration_cast<std::chrono::nanoseconds>(end - begin);
    std::cout << "The time: " << elapsed_ns.count() << " ns\n";


    //for_each(a, a + size, [](int x) {cout << x << " "; });
    //cout << endl;
}

