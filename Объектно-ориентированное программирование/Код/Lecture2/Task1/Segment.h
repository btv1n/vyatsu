#pragma once
#include "Point.h"

struct Segment
{
	Point first;
	Point last;
};

double distance(Segment);
