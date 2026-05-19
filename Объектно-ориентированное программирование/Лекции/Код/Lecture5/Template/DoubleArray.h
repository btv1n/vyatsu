#pragma once
class DoubleArray
{
private:
	double* array;
	size_t size;
public:
	DoubleArray(int* array, size_t size);
	DoubleArray(size_t size, double value);
	DoubleArray(DoubleArray const &array);
	DoubleArray& operator =(DoubleArray const& a);

	size_t getSize() const;
	double& operator [](const size_t);
	const double& operator[](const size_t)const;
};

