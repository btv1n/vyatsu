#include <iostream>
#include "Vector.h"

using namespace std;

int main()
{
    Vector v("4 6 7 8 2 9");
    vector<double> a = (vector<double>)v;
    for (size_t i = 0; i < a.size(); i++)
        cout << a[i] << " ";
    cout << endl;

    v(3, new double[3]{ 1, 2, 3 });
    a = (vector<double>)v;
    for (size_t i = 0; i < a.size(); i++)
        cout << a[i] << " ";
    cout << endl;
    std::string s = (std::string)v;
    cout << s <<endl;
    v[0] = 100;
    for (int i = 0; i < v.getSize(); i++)
        cout << v[i] << " ";
    cout << endl;
}

