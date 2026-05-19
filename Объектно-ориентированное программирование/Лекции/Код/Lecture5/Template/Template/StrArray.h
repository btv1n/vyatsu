#pragma once
#include <string>
class StrArray
{
private:
	std::string* array;
	size_t size;
public:
	StrArray(std::string* array, size_t size);
	StrArray(size_t size, std::string value);
	StrArray(StrArray const &array);
	StrArray& operator =(StrArray const& a);

	size_t getSize() const;
	std::string& operator [](const size_t);
	const std::string& operator[](const size_t)const;
};

