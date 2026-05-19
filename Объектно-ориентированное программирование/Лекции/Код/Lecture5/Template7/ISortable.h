#pragma once

template <class Type>
class ISortable
{
public:	
	 virtual void my_sort(Type* begin, Type* end) = 0;
};