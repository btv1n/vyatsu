#pragma once
#include <string>
#include "Person.h"
class Employee:public Person
{
private:
	unsigned int workPass;
public:
	Employee(std::string fullname, unsigned int age);
	inline Employee(Employee const& e) : Person(e), workPass(e.workPass) {};

	inline void setDocument(unsigned int const& workPass) { this->workPass = workPass; }
	using Person::setDocument;
};

