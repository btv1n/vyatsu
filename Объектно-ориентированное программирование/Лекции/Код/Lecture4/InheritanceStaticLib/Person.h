#pragma once
#include <string>
class Person
{
protected:
	std::string fullname;
	int age;
public:
	Person(std::string fullname, int age);
	
	inline int getAge() const { return age; }
};

