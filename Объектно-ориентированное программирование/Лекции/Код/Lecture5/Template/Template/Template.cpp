#include <iostream>
#include "IntArray.h"
#include "DoubleArray.h"
#include "StrArray.h"

using namespace std;

int main()
{
    IntArray arrint(10, 0);
    for (size_t i = 0; i < arrint.getSize(); i++)
        arrint[i] = i + 1;
    for (size_t i = 0; i < arrint.getSize(); i++)
        cout<<arrint[i] <<" ";
    cout << endl;

    DoubleArray arrdouble(10, 1.1);
    for (size_t i = 0; i < arrdouble.getSize(); i++)
        arrdouble[i] += i;
    for (size_t i = 0; i < arrdouble.getSize(); i++)
        cout << arrdouble[i] << " ";
    cout << endl;

    StrArray arrstr(10, "+");
    for (size_t i = 0; i < arrstr.getSize(); i++)
        arrstr[i] += "!!!";
    for (size_t i = 0; i < arrstr.getSize(); i++)
        cout << arrstr[i] << " ";
    cout << endl;
}

