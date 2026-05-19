#include <iostream>
#include "Vector.h"

using namespace std;
int main()
{
    Vector a;
    Vector b(3, 6), c(11, 11);
    Vector d = b;
    Vector e = b - d;
    cout << e.getX() << " " << e.getY() << endl;
    Vector f = 10 * e * 10;
    f *= 10;
    cout << f.getX() << " " << f.getY() << endl;
    c *= 20;
    cout << c.getX() << " " << c.getY() << endl;
    //Vector g = 10 * e;
    //cout << g.getX() << " " << g.getY() << endl;

}

