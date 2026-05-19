#pragma once
#include "Point.h"

class Segment
{
private:
	Point first;
	Point last;
public:
	Segment();
	Segment(Point, Point);
	double distance() const;
};

