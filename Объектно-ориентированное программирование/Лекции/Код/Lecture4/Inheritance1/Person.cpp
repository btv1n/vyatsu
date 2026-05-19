#include "Person.h"

Person::Person(std::string fullname, unsigned int age)
{
	this->fullname = fullname;
	this->age = age;
}

Person::Person(Person const& person)
{
	fullname = person.fullname;
	age = person.age;
}

Person& Person::operator=(Person const& person)
{
	if (this != &person)
	{
		fullname = person.fullname;
		age = person.age;
	}
	return *this;
}
