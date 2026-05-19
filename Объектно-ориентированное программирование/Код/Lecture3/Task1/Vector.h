#pragma once
class Vector
{
private:
	double x;
	double y;

public:
	Vector(double x = 0, double y = 0);
	Vector(Vector const& v);
	~Vector();

	double getX() const;
	double getY() const;

	void setX(double x);
	inline void setY(double y) { this->y = y; }
};

Vector operator-(Vector const& v);
Vector operator+(Vector const& v1, Vector const& v2);
Vector operator*(Vector const& v, double r);
Vector operator*(double r, Vector const& v);
