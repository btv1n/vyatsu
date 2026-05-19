#pragma once
#include <string>
#include "Person.h"

class Employee :public Person
{
private:
	unsigned int workPass;
public:
	Employee(std::string fullname, unsigned int age);
	inline Employee(Employee const& e) : Person(e), workPass(e.workPass) {};
	Employee& operator=(Employee const& employee);
	virtual ~Employee() {};
	inline void setDocument(unsigned int const& workPass) { this->workPass = workPass; }
	using Person::setDocument;
	std::string getInfo() const override;
	inline std::string career() const override { return "Employee"; }
};
