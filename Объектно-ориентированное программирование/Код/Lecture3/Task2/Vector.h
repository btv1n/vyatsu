#pragma once
class Vector
{
private:
	double x;
	double y;

public:
	Vector();
	Vector(double x, double y);
	Vector(Vector const& v);
	~Vector();

	double getX() const;
	double getY() const;
	inline friend Vector operator*(double const& d, Vector& b) { return b * d; }


	Vector operator -() const;
	Vector operator -(Vector const& p) const;
	Vector& operator *=(double d);
	Vector operator *(double d) const;
	Vector& operator +=(Vector const & v);
};


Vector operator +(Vector const& v1, Vector const& v2);
Vector operator +(Vector v1, Vector const& v2);

//Vector operator *(Vector v1, double d);