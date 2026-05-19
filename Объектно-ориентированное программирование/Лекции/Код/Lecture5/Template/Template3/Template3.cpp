#include <iostream>
#include "Array.h"
#include "Fraction.h"

using namespace std;

int main()
{
    Array<int> arrint(10, 100);
    for (size_t i = 0; i < arrint.getSize(); i++)
        arrint[i] = i + 1;
    for (size_t i = 0; i < arrint.getSize(); i++)
        cout << arrint[i] << " ";
    cout << endl;

    Array<double, unsigned short> arrdouble(10, 1.1);
    for (size_t i = 0; i < arrdouble.getSize(); i++)
        arrdouble[i] += i;
    for (size_t i = 0; i < arrdouble.getSize(); i++)
        cout << arrdouble[i] << " ";
    cout << endl;

    Array<string, char> arrstr(10, "+");
    for (size_t i = 0; i < arrstr.getSize(); i++)
        arrstr[i] += "!!!";
    for (size_t i = 0; i < arrstr.getSize(); i++)
        cout << arrstr[i] << " ";
    cout << endl;

   /*
    Array<Fraction, char> arrfraction(10, Fraction(0, 1));// у класса Fraction должен тыть конструктор по умолчанию
    for (size_t i = 0; i < arrstr.getSize(); i++)
        arrfraction[i] = Fraction(i, 1);
    for (size_t i = 0; i < arrstr.getSize(); i++)
        cout << arrfraction[i] << " ";
    cout << endl;
    */
    
}
