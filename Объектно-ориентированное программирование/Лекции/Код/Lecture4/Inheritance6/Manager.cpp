#include "Manager.h"

Manager::Manager(std::string fullname, unsigned int age):Employee(fullname, age), dealsCount(0)
{ }

std::string Manager::career() const
{
	return "Manager";
}

std::string Manager::getInfo() const
{
	return "Manager " + Employee::getInfo() + " " + std::to_string(dealsCount);
}
