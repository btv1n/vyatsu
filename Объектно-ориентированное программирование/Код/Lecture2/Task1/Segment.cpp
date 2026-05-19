#include "Segment.h"

double distance(Segment s)
{
    return sqrt(pow((s.first.x - s.last.x), 2) + pow((s.first.y - s.last.y), 2));
}
