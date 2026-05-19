#pragma once
#include "Point.h"

struct Segment
{
	Point first;
	Point last;

	Segment();
	Segment(Point p1, Point p2);
	Segment(Point p); // explicit
	~Segment() {};

	double distance();
	Point middle();
	Point middle() const;
};
