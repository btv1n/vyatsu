#include <algorithm>
#include <iostream>

template <typename Type>
void my_sort(Type* begin, Type* end);

template <typename Type, typename Size = size_t>
void my_sort(Type* begin, Size size);

using namespace std;
int main()
{
    int size = 5;
    int* a = new int[size]{2,6,1,16,9};
    for_each(a, a + size, [](int x) {cout << x << " "; });
    cout << endl;
    my_sort(a, a + size);
    for_each(a, a + size,[](int x) {cout << x << " "; });
    cout << endl;

    double* b = new double[size] {2.1, 6.4, 1.1, 1.6, 4.9};
    for_each(b, b + size, [](double x) {cout << x << " "; });
    cout << endl;
    my_sort(b, size);
    for_each(b, b + size, [](double x) {cout << x << " "; });
    cout << endl;
}

template<typename Type>
void my_sort(Type* begin, Type* end)
{
    sort(begin, end);
}

template<typename Type, typename Size>
void my_sort(Type* begin, Size size)
{
    sort(begin, begin + size);
}
