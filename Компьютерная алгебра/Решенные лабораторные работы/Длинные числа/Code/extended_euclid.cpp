#include <iostream>
#include <string>
#include <algorithm>
#include <tuple>
#include "LNumber/lnum.h"

using std::cin;
using std::cout;
using std::endl;
using std::string;

std::tuple<LNum, LNum, LNum> extended_euclid(const LNum &a, const LNum &b)
{
    if (!a.positive || !b.positive)
        throw std::invalid_argument("arguments must be positive");
    if (a == 0)
        return {LNum(b), LNum("0"), LNum("1")};

    LNum gcd, x, y;
    std::tie(gcd, x, y) = extended_euclid(b % a, a);
    cout << y - (b / a) * x << "*" << a << " + " << x << "*" << b << " = " << gcd << endl;
    // gcd, y - (b // a)*x, x
    return std::make_tuple(gcd, y - (b / a) * x, x);
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
            LNum gcd, x, y;
            std::tie(gcd, x, y) = extended_euclid(na, nb);
            cout << x << "*" << a << " + " << y << "*" << b << " = " << gcd
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
