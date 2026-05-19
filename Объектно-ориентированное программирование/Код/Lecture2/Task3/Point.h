#pragma once


class Point
{
private:
	double x;
	double y;
	mutable int count;

public:
	Point();
	Point(double x, double y);
	Point(Point const & p);
	~Point();

	double distance(Point const & p) const;
	Point middle(Point const& p) const;
	double getX() const;
	inline double getY() const { return y; };
	inline void setX(double const& x) { this->x = x; }
};

