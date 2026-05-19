#include "Person.h"

Person::Person(std::string fullname, unsigned int age) :fullname(fullname), age(age) {}

Person::Person(Person const& person) : fullname(person.fullname), age(person.age) {};


Person& Person::operator=(Person const& person)
{
	if (this != &person)
	{
		fullname = person.fullname;
		age = person.age;
		passport = person.passport;
	}
	return *this;
}

std::string Person::getInfo() const
{
	return "Person " + fullname + " " + std::to_string(age) + " " + passport;
}
