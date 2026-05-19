#include "Point.h"
#include <cmath>

Point::Point():x(0),y(0)
{ }

Point::Point(double x, double y)
{
	this->x = x;
	this->y = y;
}

double Point::distance(Point p)
{
	return sqrt(pow(x - p.x, 2) + pow(y - p.y, 2));
}

Point Point::middle(Point p)
{
	p.x = 0;
	p.y = 0;
	return Point((x+p.x)/2, (y + p.y) / 2);
}

Point Point::middle(Point const & p) const
{
	//p.x = 0;
	//x = 0;
	//p.y = 0;
	return Point((x + p.x) / 2, (y + p.y) / 2);
}
