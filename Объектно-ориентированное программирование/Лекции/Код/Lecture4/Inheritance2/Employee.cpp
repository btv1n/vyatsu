#include "Employee.h"

Employee::Employee(std::string fullname, unsigned int age):
	Person(fullname, age), workPass(0) 
{ }