#pragma once
#include <iostream>
template<typename T, typename Enable = void>
class Matrix;

template<class T>
class Matrix<T, typename std::enable_if<std::is_same<T, int>::value ||std::is_same<T, double>::value>::type>
{
	T** arr;
	size_t column, row;
public:
	Matrix(size_t column, size_t row);
	void print();
};


template<class T>
inline Matrix<T, typename std::enable_if<std::is_same<T, int>::value || std::is_same<T, double>::value>::type>::Matrix(size_t column, size_t row)
{
	this->column = column;
	this->row = row;
	arr = new T * [row];
	for (size_t i = 0; i < row; i++)
		arr[i] = new T[column];
	for (size_t i = 0; i < row; i++)
	{
		for (size_t j = 0; j < column; j++)
			arr[i][j] = 0;
	}
}

template<class T>
inline void Matrix<T, typename std::enable_if<std::is_same<T, int>::value || std::is_same<T, double>::value>::type>::print()
{
	for (size_t i = 0; i < row; i++)
	{
		for (size_t j = 0; j < column; j++)
			std::cout<<arr[i][j]<<" ";
		std::cout << "\n";
	}
	std::cout << "\n";
}

