#include "Point.h"
#include <cmath>

Point::Point() :x(0), y(0) {}

Point::Point(double x, double y): x(x), y(y) {}

Point::Point(Point const& p)
{
	x = p.x;
	y = p.y;
}

double Point::distance(Point const & p) const
{
	count++;
	return sqrt(pow((x - p.x), 2) + pow((y - p.y), 2));
}

Point Point::middle(Point const& p) const
{
	count++;
	return Point((x + p.x) / 2, (y + p.y) / 2);
}

double Point::getX() const
{
	return x;
}