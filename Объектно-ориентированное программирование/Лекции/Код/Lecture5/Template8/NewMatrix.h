#pragma once
#include <iostream>
template<typename T>
concept operation_type = requires (T x) { x + x; x* x; x - x; };

template<operation_type T>
class NewMatrix
{
	T** arr;
	size_t column, row;
public:
	NewMatrix(size_t column, size_t row);
	void print();
};

template<operation_type T>
inline NewMatrix<T>::NewMatrix(size_t column, size_t row)
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

template<operation_type T>
inline void NewMatrix<T>::print()
{
	for (size_t i = 0; i < row; i++)
	{
		for (size_t j = 0; j < column; j++)
			std::cout << arr[i][j] << " ";
		std::cout << "\n";
	}
	std::cout << "\n";
}
