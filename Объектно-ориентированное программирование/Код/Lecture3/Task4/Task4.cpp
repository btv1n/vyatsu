#include <iostream>
#include "BigNum.h"

using namespace std;

int main()
{
    BigNum a("346"), b("726479"), c("999");
    ++a;
    cout << (string)a << endl;

    BigNum d = b++;
    cout << (string)b << endl;
    cout << (string)d << endl;

    ++c;
    cout << (string)c << endl;

    BigNum e;
    cin >> e;
    cout << e << endl;

    BigNum f;
    f("987");
    cout << f << endl;
}

