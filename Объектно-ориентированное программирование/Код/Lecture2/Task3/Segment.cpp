#include "Segment.h"
#include <cmath>
Segment::Segment():first(Point()), last(Point())
{}

Segment::Segment(Point p1, Point p2):first(p1), last(p2)
{}

double Segment::distance() const
{
	return sqrt(pow((first.getX() - last.getX()),2) + pow((first.getY() - last.getY()), 2));
}

