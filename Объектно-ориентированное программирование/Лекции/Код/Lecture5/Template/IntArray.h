#pragma once
class IntArray
{
private:
	int* array;
	size_t size;
public:
	IntArray(int* array, size_t size);
	IntArray(size_t size, int value);
	IntArray(IntArray const &array);
	IntArray& operator =(IntArray const& a);

	size_t getSize() const;
	int& operator [](const size_t);
	const int& operator[](const size_t)const;
};

