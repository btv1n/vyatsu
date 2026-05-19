#pragma once
#include <string>
#include <iostream>
#include "Person.h"

class Employee:public Person 
{
private:
	unsigned int workPass;
	unsigned int salary;
public:
	Employee(std::string fullname, unsigned int age);
	inline Employee(Employee const& e) : Person(e), workPass(e.workPass), salary(e.salary) {};
	Employee& operator=(Employee const& employee);

	inline void setAge(unsigned int age) { Person::setAge(age); }

	inline void setDocument(unsigned int const& workPass) { this->workPass = workPass; }
	inline void setSalary(unsigned int const& salary) { this->salary = salary; }
	using Person::setDocument;
	std::string getInfo() const override;
	inline std::string career() const override { return "Employee"; }
	virtual unsigned int getSalary() { return salary; }
};
