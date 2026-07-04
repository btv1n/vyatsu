#include <iostream>
#include <string>
#include <algorithm>
#include "LNumber/lnum.h"

using std::cin;
using std::cout;
using std::endl;
using std::string;

LNum gcd_euclid(const LNum &a, const LNum &b)
{
    if (a == 0 || b == 0)
        return a + b;

    if (a > b)
        return gcd_euclid(a % b, b);
    else
        return gcd_euclid(b % a, a);
}

LNum lcm(const LNum &a, const LNum &b)
{
    return a / gcd_euclid(a, b) * b;
}

int main(int argn, char **argv)
{
    string a, b;

    while (true)
    {
        cout << "a: ";
        cin >> a;
        cout << "b: ";
        cin >> b;

        try
        {
            LNum na(a), nb(b);
            cout << "result: " << lcm(na, nb)
                 << endl
                 << endl;
        }
        catch (const std::exception &e)
        {
            std::cerr << e.what()
                      << endl
                      << endl;
        }
    }

    return 0;
}
