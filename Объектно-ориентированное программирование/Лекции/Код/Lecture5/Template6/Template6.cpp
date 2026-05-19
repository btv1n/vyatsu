#include <iostream>
#include "Array.h"

using namespace std;
int main()
{
    Array<int> a(10, 10);
    Array<double>b(a);
    for (size_t i = 0; i < b.getSize(); i++)
        cout << b[i] << " ";
    cout << endl;
    Array<double> c(10, 1.2);
    a = c;
    for (size_t i = 0; i < b.getSize(); i++)
        cout << a[i] << " ";
    cout << endl;
}
