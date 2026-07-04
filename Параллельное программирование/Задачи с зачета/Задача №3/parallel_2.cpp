#include <iostream>
#include <mpi.h>
#include <vector>
#include <cstdlib>

const int per_process = 1024;

// Версия с использованием блокирующих операций
int main(int argc, char **argv)
{
    MPI_Init(&argc, &argv);

    int proc_rank, proc_num;
    MPI_Comm_rank(MPI_COMM_WORLD, &proc_rank);
    MPI_Comm_size(MPI_COMM_WORLD, &proc_num);
    int slave_number = proc_num - 1;
    const int N = slave_number * per_process;

    if (proc_rank == 0)
    {
        // master-процесс

        // Генерируем массив
        srand(0);

        int *arr = new int[N];
        for (int i = 0; i < N; i++)
            arr[i] = rand() % 100;

        for (int i = 0, start = 0; i < slave_number; i++)
            MPI_Send(arr + per_process * i, per_process, MPI_INT, i + 1, 0, MPI_COMM_WORLD);

        for (int i = 0, start = 0; i < slave_number; i++)
        {
            MPI_Recv(arr, per_process, MPI_INT, i + 1, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
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
        int *arr = new int[per_process];
        int *result = new int[per_process];
        int start = (proc_rank - 1) * per_process;
        for (int i = 1; i / 2 < N; i *= 2)
        {
            for (int j = start; j < start + proc_rank; j++)
                if (j < i)
                    result[j] = arr[j];
                else
                    if(j < start || j >= start + per_process) {

                    } else {
                        result[j] = arr[j] + arr[j - i];
                    }
            std::swap(result, arr);
        }
    }

    MPI_Finalize();
    return 0;
}