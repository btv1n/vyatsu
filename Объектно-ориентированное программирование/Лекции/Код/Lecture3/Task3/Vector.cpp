#include "Vector.h"
#include <sstream>
#include <exception>

Vector::Vector(size_t n):size(n), coordinates(new double[n])
{ }

Vector::Vector(std::string s)
{
	std::istringstream ss(s);
	std::vector<double> v;
	while (!ss.eof())
	{
		std::string temp;
		ss >> temp;
		v.push_back(std::stod(temp));
	}
	size = v.size();
	coordinates = new double[size];
	for (size_t i = 0; i < size; i++)
		coordinates[i] = v[i];
}

Vector::Vector(size_t size, double* coordinates):Vector(size)
{
	for (size_t i = 0; i < size; i++)
		this->coordinates[i] = coordinates[i];
}

Vector::Vector(Vector const& v):Vector(v.size, v.coordinates)
{ }

size_t Vector::getSize() const
{
	return size;
}

Vector& Vector::operator =(Vector const& p)
{
	if (this != &p) {
		delete[] coordinates;
		size = p.size;
		coordinates = new double[size];
		for (size_t i = 0; i != size; ++i)
			coordinates[i] = p.coordinates[i];
	}
	return *this;
}

double& Vector::operator [](const size_t i) 
{
	return coordinates[i];
}

const double Vector::operator[](const size_t i)const
{
	if (i >= 0 && i < size)
		return coordinates[i];
	else
		throw std::out_of_range("Index out of range");
}

Vector::operator std::string() const
{
	std::string s = "";
	for (size_t i = 0; i < size - 1; i++)
		s += std::to_string(coordinates[i]) + " ";
	s += std::to_string(coordinates[size - 1]);
	return s;
}

Vector::operator std::vector<double>() const
{
	std::vector<double> v;
	for (size_t i = 0; i < size; i++)
		v.push_back(coordinates[i]);
	return v;
}

void Vector::operator()(size_t n, double* b)
{
	Vector temp(n, b);
	*this = temp;
}