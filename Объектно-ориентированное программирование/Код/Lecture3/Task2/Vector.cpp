#include "Vector.h"

Vector::Vector() :x(0), y(0)
{ }

Vector::Vector(double x, double y) : x(x), y(y)
{ }

Vector::Vector(Vector const& v) : x(v.x), y(v.y)
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

Vector Vector::operator-() const
{
	return Vector(-x, -y);
}

Vector Vector::operator-(Vector const& p) const
{
	return Vector(x - p.x, y - p.y);
}

Vector& Vector::operator*=(double d)
{
	x *= d;
	y *= d;
	return *this;
}

Vector Vector::operator*(double d) const
{
	return Vector(x*d, y*d);
}

Vector operator *(Vector v1, double d)
{
	return v1 *= d;
}

Vector& Vector::operator+=(Vector const& v)
{
	x += v.x;
	y += v.y;
	return *this;
}

Vector operator +(Vector v1, Vector const& v2)
{
	return v1 += v2;
}

Vector operator +(Vector const& v1, Vector const& v2)
{
	Vector v(v1);
	return v += v2;
}