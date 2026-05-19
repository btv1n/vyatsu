#pragma once
#include <string>

class Person
{
private:
	std::string fullname;
	unsigned int age;

protected:
	inline void setFullname(std::string fullname) { this->fullname = fullname; }
	inline void setAge(unsigned int age) { this->age = age; }

public:
	Person(std::string fullname, unsigned int age);
	Person(Person const & person);
	Person& operator=(Person const& person);
	inline std::string getFullname() const { return fullname; }
	inline unsigned int getAge() const { return age; }
};

