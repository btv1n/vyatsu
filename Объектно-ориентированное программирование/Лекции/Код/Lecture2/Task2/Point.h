#include "..\Task3\Point.h"
#pragma once

struct Point
{
	double x;
	double y;
	Point();

	Point(double x, double y);
	~Point() {}
	inline double Point::distance(Point const& p) const
	{
		return 0.0;
	}
	

	double distance(Point p);
	Point middle(Point p);
	Point middle(Point const & p) const;
};
