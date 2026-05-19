#pragma once
#include <vector>
template <class T>
class Array
{
private:
	T* array;
	size_t size;
public:
	Array(std::vector<T> v);
	Array(T* array, size_t size);
	Array(size_t size, T value);
	Array(Array const &array);
	Array& operator =(Array const& a);

	size_t getSize() const;
	T& operator [](const size_t);
	const T& operator[](const size_t)const;

};

template<class T>
inline Array<T>::Array(std::vector<T> v)
{
}

template<class T>
inline Array<T>::Array(T* array, size_t size)
{
	this->size = size;
	this->array = new T[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = array[i];
}

template<class T>
inline Array<T>::Array(size_t size, T value)
{
	this->size = size;
	this->array = new T[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = value;
}

template<class T>
inline Array<T>::Array(Array const &a)
{
	size = a->size;
	array = new T[size];
	for (size_t i = 0; i < size; i++)
		array[i] = a->array[i];
}

template<class T>
inline Array<T>& Array<T>::operator=(Array const& a)
{
	if (this != &a) {
		delete[] array;
		size = a.size;
		array = new T[size];
		for (size_t i = 0; i != size; ++i)
			array[i] = a.array[i];
	}
	return *this;
}

template<class T>
inline size_t Array<T>::getSize() const
{
	return size;
}

template<class T>
inline T& Array<T>::operator[](const size_t i)
{
	return array[i];
}

template<class T>
inline const T& Array<T>::operator[](const size_t i) const
{
	return array[i];
}
