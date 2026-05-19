#pragma once

template <class T, class Size = size_t>
class Array
{
private:
	T* array;
	Size size;
public:
	Array(T* array, Size size);
	Array(Size size, T value);
	Array(Array const &array);
	Array& operator =(Array const& a);

	Size getSize() const;
	T& operator [](const Size);
	const T& operator[](const Size)const;
	~Array();
};


template <class T, class Size>
inline Array<T, Size>::Array(T* array, Size size)
{
	this->size = size;
	this->array = new T[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = array[i];
}

template <class T, class Size>
inline Array<T, Size>::Array(Size size, T value)
{
	this->size = size;
	this->array = new T[size];
	for (size_t i = 0; i < size; i++)
		this->array[i] = value;
}

template <class T, class Size>
inline Array<T, Size>::Array(Array const &a)
{
	size = a->size;
	array = new T[size];
	for (size_t i = 0; i < size; i++)
		array[i] = a->array[i];
}

template <class T, class Size>
inline Array<T, Size>& Array<T,Size>::operator=(Array const& a)
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

template <class T, class Size>
inline Size Array<T, Size>::getSize() const
{
	return size;
}

template <class T, class Size>
inline T& Array<T, Size>::operator[](const Size i)
{
	return array[i];
}

template <class T, class Size>
inline const T& Array<T, Size>::operator[](const Size i) const
{
	return array[i];
}

template <class T, class Size>
inline Array<T, Size>::~Array()
{
	delete[] array; 

}