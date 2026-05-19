#include "Employee.h"

Employee::Employee(std::string fullname, unsigned int age) :
	Person(fullname, age), workPass(0)
{ }

Employee& Employee::operator=(Employee const& employee)
{
	if (this != &employee)
	{
		workPass = workPass;
	}
	return *this;
}

std::string Employee::getInfo() const
{
	return "Employee " + Person::getInfo() + " " + std::to_string(workPass);
}
