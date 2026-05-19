#include <iostream>
#include "Vector.h"

using namespace std;
int main()
{
    Vector a;
    Vector b(3, 6), c(11, 11);
    Vector d = b;
    Vector e = b + d;
    cout << e.getX() << " " << e.getY() << endl;
    Vector f = e * 10;
    cout << f.getX() << " " << f.getY() << endl;
    Vector g = 20 * e;
    cout << g.getX() << " " << g.getY() << endl;
    Vector h = -e;
    cout << h.getX() << " " << h.getY() << endl;
    cout << e.getX() << " " << e.getY() << endl;

}

