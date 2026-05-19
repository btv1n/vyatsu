#include "pch.h"
#include "Person.h"
#include <exception>

Person::Person(std::string fullname, int age)
	: fullname (fullname)
{
	if (age <= 0)
		throw std::exception("age is incorrect");
	this->age = age;
}
