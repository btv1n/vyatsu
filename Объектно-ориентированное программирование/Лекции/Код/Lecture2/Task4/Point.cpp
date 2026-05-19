#include "Point.h"
#include <sstream>
#include <cmath>
#include <algorithm>

Point::Point(size_t n)
{
	size = n;
	coordinates = new double[n] {0};
}

Point::Point(Point const & p):Point(p.size)
{
	for (size_t i = 0; i < size; i++)
		coordinates[i] = p.coordinates[i];
}

Point::Point(std::string const& s)
{
	std::stringstream ss(s);
	std::vector<double> arr;
	while (!ss.eof()) 
	{
		std::string temp;
		ss >> temp;
		arr.push_back(stod(temp));
	}
	size = arr.size();
	coordinates = new double[size];
	for (size_t i = 0; i < size; i++)
		coordinates[i] = arr[i];
}

Point::~Point()
{
	size = 0;
	delete [] coordinates;
}

double Point::distance(Point const& p) const
{
	double s = 0.0;
	for (size_t i = 0; i < size; i++)
		s += pow((coordinates[i] - p.coordinates[i]), 2);
	return sqrt(s);
}

Point Point::middle(Point const& p) const
{
	Point newp(p.size);
	for (size_t i = 0; i < size; i++)
		newp.coordinates[i] = (coordinates[i] + p.coordinates[i]) / 2;
	return newp;
}

/*Point& ñoperator =(Point const& p)
{
	if (this != &p) {
		delete [] coordinates;
		size = p.size;
		coordinates = new double[size];
		for (size_t i = 0; i != size; ++i)
			coordinates[i] = p.coordinates[i];
	}
	return *this;
}*/



Point& Point::operator =(Point const& p) {
	if (this != &p)
		Point(p).swap(*this);
	return *this;
}

/*
void Point::swap(Point& p)
{
	std::swap(size, p.size);
	
	std::swap(coordinates, p.coordinates);
}
*/

void Point::swap(Point& p)
{
	size_t const t1 = size;
	size = p.size;
	p.size = t1;

	double* const t2 = coordinates;
	coordinates = p.coordinates;
	p.coordinates = t2;
}

double Point::get(size_t i) const {
	return coordinates[i];
}
double& Point::get(size_t i) {
	return coordinates[i];
}

size_t Point::getSize() const
{
	return this->size;
}
