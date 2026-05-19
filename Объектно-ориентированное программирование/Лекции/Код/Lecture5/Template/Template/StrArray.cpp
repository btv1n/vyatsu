#include "StrArray.h"

StrArray::StrArray(std::string* array, size_t size)
{
	this->size = size;
	this->array = new std::string[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = array[i];
}

StrArray::StrArray(size_t size, std::string value)
{
	this->size = size;
	this->array = new std::string[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = value;
}

StrArray::StrArray(StrArray const &strarray)
{
	size = strarray.size;
	array = new std::string[size];
	for (size_t i = 0; i < size; i++)
		array[i] = strarray.array[i];
}

size_t StrArray::getSize() const
{
	return size;
}

std::string& StrArray::operator[](const size_t i)
{
	return array[i];
}

const std::string& StrArray::operator[](const size_t i) const
{
	return array[i];
}

StrArray& StrArray::operator=(StrArray const& strarray)
{
	if (this != &strarray) {
		delete[] array;
		size = strarray.size;
		array = new std::string[size];
		for (size_t i = 0; i != size; ++i)
			array[i] = strarray.array[i];
	}
	return *this;
}