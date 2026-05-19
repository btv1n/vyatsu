#pragma once
#include <vector>
#include  <string>

class Point
{
private:
	size_t size;
	double * coordinates;
public:
	Point(size_t n);
	Point(Point const & p);
	Point(std::string const & s);

	~Point();

	double distance(Point const& p) const;
	Point middle(Point const& p) const;

	Point& operator =(Point const& p);

	void swap(Point &p);
	double get(size_t i) const;
	double& get(size_t i);
	size_t getSize() const;
};

