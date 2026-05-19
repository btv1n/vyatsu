#include "..\Task3\Segment.h"
#include "Segment.h"
#include <cmath>

Segment::Segment():first(Point()), last(Point())
{ }

Segment::Segment(Point p1, Point p2):first(p1), last(p2)
{ }

Segment::Segment(Point p):first(Point(0,0)), last(p)
{ }

double Segment::distance()
{
	return sqrt(pow(first.x - last.x, 2) + pow(first.y - last.y, 2));
}

Point Segment::middle()
{
	first.x = 0;
	first.y = 0;
	return Point((first.x + last.x)/2, (first.x + last.x) / 2);
}

Point Segment::middle() const
{
	//first.x = 0;
	//first.y = 0;
	return Point((first.x + last.x) / 2, (first.x + last.x) / 2);
}
