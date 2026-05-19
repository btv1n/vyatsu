#include "Employee.h"

Employee::Employee(std::string fullname, unsigned int age, unsigned int workPass):
	Person(fullname, age), workPass(workPass)
{ }
