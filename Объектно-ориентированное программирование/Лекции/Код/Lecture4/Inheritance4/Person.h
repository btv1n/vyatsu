#pragma once
#include <string>

class Person
{
private:
	std::string fullname;
	unsigned int age;
	std::string passport;
protected:
	inline void setFullname(std::string fullname) { this->fullname = fullname; }
	inline void setAge(unsigned int age) { this->age = age; }

public:
	Person(std::string fullname, unsigned int age);
	Person(Person const& person);
	Person& operator=(Person const& person);
	virtual ~Person() {};
	inline std::string getFullname() const { return fullname; }
	inline unsigned int getAge() const { return age; }

	inline void setDocument(std::string const& passport) { this->passport = passport; }
	// std::string getInfo() const;
	virtual std::string getInfo() const;
	virtual std::string career() const = 0;
};

