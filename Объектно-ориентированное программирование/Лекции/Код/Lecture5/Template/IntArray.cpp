#include "IntArray.h"

IntArray::IntArray(int* array, size_t size)
{
	this->size = size;
	this->array = new int[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = array[i];
}

IntArray::IntArray(size_t size, int value)
{
	this->size = size;
	this->array = new int[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = value;

}

IntArray::IntArray(IntArray const &intarray)
{
	size = intarray.size;
	array = new int[size];
	for (size_t i = 0; i < size; i++)
		array[i] = intarray.array[i];
}

size_t IntArray::getSize() const
{
	return size;
}

int& IntArray::operator[](const size_t i)
{
	return array[i];
}

const int& IntArray::operator[](const size_t i) const
{
	return array[i];
}

IntArray& IntArray::operator=(IntArray const& intarray)
{
	if (this != &intarray) {
		delete[] array;
		size = intarray.size;
		array = new int[size];
		for (size_t i = 0; i != size; ++i)
			array[i] = intarray.array[i];
	}
	return *this;
}