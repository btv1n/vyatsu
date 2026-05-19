#include <iostream>
#include <cmath>
#include "Point.h"
#include "Segment.h"

using namespace std;

int main()
{
    Point p1;
    p1.x = 2;
    p1.y = 6;
    cout << p1.x << " " << p1.y << "\n";
    Point p2;
    p2.x = 4;
    p2.y = 7;
    cout << p2.x << " " << p2.y << "\n";
    double d1 = distance(p1, p2);
    Segment s;
    s.first = p1;
    s.last = p2;
    double d2 = distance(s);
    cout << d1 << " " << d2 << endl;
}



