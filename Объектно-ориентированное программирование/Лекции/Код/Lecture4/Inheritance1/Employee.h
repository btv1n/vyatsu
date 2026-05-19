#pragma once
#include <string>
#include "Person.h"
class Employee:public Person
{
private:
	unsigned int workPass;
public:
	Employee(std::string fullname, unsigned int age, unsigned int workPass);
	Employee(Employee const& e): Person(e), workPass(e.workPass) {};
	unsigned int getWorkPass() const { return workPass; }
};

