#include <iostream>
#include <thread>
#include <vector>
#include <mutex>
#include <random>

using namespace std;

const size_t N = 1000;
mutex mtx;

void writer(vector<int> &arr)
{
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<int> dist(0, 4000);
    uniform_int_distribution<int> sleep_dist(500, 1500);

    while (true)
    {
        this_thread::sleep_for(chrono::milliseconds(sleep_dist(gen)));
        lock_guard lock(mtx);
        for (size_t i = 0; i < arr.size(); i++)
            arr[i] = dist(gen);
    }
}

void printer(vector<int> &arr)
{
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<size_t> index_dist(0, arr.size() - 1 - 3);
    uniform_int_distribution<int> sleep_dist(500, 1500);

    while (true)
    {
        this_thread::sleep_for(chrono::milliseconds(sleep_dist(gen)));
        size_t start = index_dist(gen);
        lock_guard lock(mtx);
        for (size_t i = 0; i < 3; i++)
            cout << arr[start + i] << " ";
        cout << endl;
    }
}

int main()
{
    vector<int> arr(N, 1);

    thread f(writer, ref(arr));
    thread p(printer, ref(arr));

    f.join();
    p.join();
    return 0;
}