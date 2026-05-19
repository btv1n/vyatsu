#include <iostream>
#include "Point.h"

using namespace std;

void print(Point const&);

int main()
{
    Point p(6);
    print(p);
    Point p1("6 2 5");
    print(p1);
    Point p2 = p1;
    print(p2);
    p.get(1) = 7;
    print(p);
}

void print(Point const& p)
{
    for (int i = 0; i < p.getSize(); i++)
        cout << p.get(i) << " ";
    cout << endl;
}