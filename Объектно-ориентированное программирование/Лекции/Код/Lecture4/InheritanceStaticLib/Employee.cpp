#include "pch.h"
#include "Employee.h"

Employee::Employee(std::string fullname, int age, int id)
	:Person(fullname, age), id(id)
{
}
