#pragma once
#include "Employee.h"
class Manager:public Employee
{
private:
	unsigned int dealsCount;
public:
	Manager(std::string fullname, unsigned int age);
	std::string career() const override;
	std::string getInfo() const override;
	virtual unsigned int getSalary() override { return (Employee::getSalary() * (100 + dealsCount)) / 100; }
};

