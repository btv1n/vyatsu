#include <iostream>
#include <omp.h>
#include <random>
#include "measure.h"

int main(int argc, char **argv)
{
    if (argc != 3)
    {
        std::cout << "Usage: " << argv[0] << " N threads" << std::endl;
        return 1;
    }

    const long long N = std::stoll(argv[1]);
    if (N <= 0)
    {
        std::cerr << "N must be > 0" << std::endl;
        return 1;
    }

    const int nthreads = std::stoi(argv[2]);
    if (nthreads <= 0)
    {
        std::cerr << "nthreads must be > 0" << std::endl;
        return 1;
    }

    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> dist(0, 4000);

    int *arr = new int[N];
    for (int i = 0; i < N; i++)
        arr[i] = dist(gen);

    auto parallelTarget = [arr, N, nthreads]()
    {
        int m = arr[0];
        #pragma omp parallel num_threads(nthreads) reduction(min : m)
        {
            #pragma omp for schedule(auto)
            for (int i = 0; i < N; i++)
                if (arr[i] % 2 == 1)
                    m = std::min(arr[i], m);
        }
    };

    auto syncTarget = [arr, N]()
    {
        int m = arr[0];
        for (int i = 0; i < N; i++)
            if (arr[i] % 2 == 1)
                m = std::min(arr[i], m);
    };

    std::cout << "Параллельная версия\n";
    auto parallelResult = makeMeasure10(parallelTarget);
    printResult(parallelResult);

    std::cout << "\nПоследовательная версия\n";
    auto syncResult = makeMeasure10(syncTarget);
    printResult(syncResult);
    
    double speedup = syncResult.first / parallelResult.first;
    std::cout << "\nУскорение: " << speedup << "\n";

    delete[] arr;
    return 0;
}