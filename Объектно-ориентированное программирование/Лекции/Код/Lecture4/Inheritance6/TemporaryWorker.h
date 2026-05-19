#pragma once
#include "Person.h"
class TemporaryWorker: public Person
{
private:
	unsigned int contractDuration;
public:
	TemporaryWorker(std::string fullname, unsigned int age);
	inline TemporaryWorker(TemporaryWorker const& e) : Person(e), contractDuration(e.contractDuration) {};
	TemporaryWorker& operator=(TemporaryWorker const& temporaryWorker);

	inline void setDocument(unsigned int const& contractDuration) { this->contractDuration = contractDuration; }
	using Person::setDocument;
	std::string getInfo() const;
	inline std::string career() const {return "TemporaryWorker";	}
};

