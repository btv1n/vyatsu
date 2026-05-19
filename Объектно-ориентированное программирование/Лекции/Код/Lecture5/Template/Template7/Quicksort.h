#pragma once
#include "ISortable.h"
#include <algorithm>

template <class Type>
class Quicksort :public ISortable<Type>
{
public:
	void my_sort(Type* begin, Type* end) override;	
};

template<class Type>
inline void Quicksort<Type>::my_sort(Type* begin, Type* end)
{
	std::sort(begin, end);
}