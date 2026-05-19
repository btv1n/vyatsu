#include <iostream>
#include "Point.h"
#include "Segment.h"

using namespace std;

int main()
{
    //Point p; // ошибка
    Point p1(2, 6);
    cout << p1.x << " " << p1.y << "\n";
    Point p2(4, 7);
    cout << p2.x << " " << p2.y << "\n";

    double d1 = p1.distance(p2);

    //Segment sg; // ошибка
    //cout << sg.first.x << " " << sg.first.y << "\n";

    Segment s(p1, p2);

    Segment s1(p1, p2);
    Segment s2(p1);
    cout << s2.first.x << " " << s2.first.y;
    cout << s2.last.x << " " << s2.last.y;

    Segment s3 = p2;
    cout << s3.first.x << " " << s3.first.y;
    cout << s3.last.x << " " << s3.last.y;

    double d2 = s.distance();
    cout << d1 << " " << d2 << endl;
}

