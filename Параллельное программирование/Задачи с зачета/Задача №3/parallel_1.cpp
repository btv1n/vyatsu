#include <iostream>
#include <mpi.h>
#include <vector>
#include <cstdlib>

const int N = 500000;

// Версия с использованием блокирующих операций
int main(int argc, char **argv)
{
    MPI_Init(&argc, &argv);

    int proc_rank, proc_num;
    MPI_Comm_rank(MPI_COMM_WORLD, &proc_rank);
    MPI_Comm_size(MPI_COMM_WORLD, &proc_num);
    int slave_number = proc_num - 1;
    long long chunk = N / slave_number;
    int remainder = N % slave_number;

    if (proc_rank == 0)
    {
        // master-процесс

        // Генерируем массив
        srand(0);
        std::vector<int> arr(N);
        for (int i = 0; i < N; i++)
            arr[i] = rand() % 100;
        auto start = MPI_Wtime();

        for (int i = 0, start = 0; i < slave_number; i++)
        {
            int count = chunk + (i < remainder ? 1 : 0);
            MPI_Send(&count, 1, MPI_INT, i + 1, 0, MPI_COMM_WORLD);
            MPI_Send(&start, 1, MPI_INT, i + 1, 0, MPI_COMM_WORLD);
            MPI_Send(arr.data(), start + count + 1, MPI_INT, i + 1, 0, MPI_COMM_WORLD);
            start += count;
        }

        std::cout << "Массивы разосланы: " << MPI_Wtime() - start << std::endl;
        std::vector<int> partial(N / slave_number + 1);
        for (int i = 0, start = 0; i < slave_number; i++)
        {
            int count = chunk + (i < remainder ? 1 : 0);
            MPI_Recv(partial.data(), count, MPI_INT, i + 1, MPI_ANY_TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            for (int j = 0; j < count; j++)
                arr[start + j] = partial[j];
            start += count;
        }


        std::cout << "Выполнено: " << MPI_Wtime() - start << std::endl;
    }
    else
    {
        int start, count;
        MPI_Recv(&count, 1, MPI_INT, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        MPI_Recv(&start, 1, MPI_INT, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);

        int local_size = start + count + 1;
        int *arr = new int[local_size];
        MPI_Recv(arr, local_size, MPI_INT, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        
        
        int *result = new int[count];
        for (int i = 0; i < count; i++) {
            result[i] = 0;
            int end = start + i;
            for (int j = start; j <= end; j++)
                result[i] += arr[j];
        }

        MPI_Send(result, count, MPI_INT, 0, 0, MPI_COMM_WORLD);
    }

    MPI_Finalize();
    return 0;
}