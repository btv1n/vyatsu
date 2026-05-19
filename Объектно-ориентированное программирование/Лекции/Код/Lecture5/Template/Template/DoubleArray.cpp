#include "DoubleArray.h"

DoubleArray::DoubleArray(int* array, size_t size)
{
	this->size = size;
	this->array = new double[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = array[i];
}

DoubleArray::DoubleArray(size_t size, double value)
{
	this->size = size;
	this->array = new double[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = value;
}

DoubleArray::DoubleArray(DoubleArray const &doublearray)
{
	size = doublearray.size;
	array = new double[size];
	for (size_t i = 0; i < size; i++)
		array[i] = doublearray.array[i];
}

size_t DoubleArray::getSize() const
{
	return size;
}

double& DoubleArray::operator[](const size_t i)
{
	return array[i];
}

const double& DoubleArray::operator[](const size_t i) const
{
	return array[i];
}

DoubleArray& DoubleArray::operator=(DoubleArray const& doublearray)
{
	if (this != &doublearray) {
		delete[] array;
		size = doublearray.size;
		array = new double[size];
		for (size_t i = 0; i != size; ++i)
			array[i] = doublearray.array[i];
	}
	return *this;
}
