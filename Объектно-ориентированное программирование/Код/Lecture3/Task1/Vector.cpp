#include "Vector.h"

Vector::Vector(double x, double y):x(x), y(y)
{ }

Vector::Vector(Vector const& v):x(v.x), y(v.y)
{ }

Vector::~Vector()
{ }

double Vector::getX() const
{
	return x;
}

double Vector::getY() const
{
	return y;
}

void Vector::setX(double x)
{
	this->x = x;
}

Vector operator-(Vector const& v)
{
	return Vector(-v.getX(), -v.getY());
}

Vector operator+(Vector const& v1, Vector const& v2)
{
	return Vector(v1.getX() + v2.getX(), v1.getY() + v2.getY());
}

Vector operator*(Vector const& v, double r)
{
	return Vector(v.getX() * r, v.getY() * r);
}

Vector operator*(double r, Vector const& v)
{
	return v*r;
}
