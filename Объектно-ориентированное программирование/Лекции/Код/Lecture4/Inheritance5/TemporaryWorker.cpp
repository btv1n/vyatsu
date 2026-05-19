#include "TemporaryWorker.h"

TemporaryWorker::TemporaryWorker(std::string fullname, unsigned int age):
Person(fullname, age), contractDuration(0)
{ }

TemporaryWorker& TemporaryWorker::operator=(TemporaryWorker const& temporaryWorker)
{
	if (this != &temporaryWorker)
	{
		contractDuration = temporaryWorker.contractDuration;
	}
	return *this;
}

std::string TemporaryWorker::getInfo() const
{
	return "TemporaryWorker " + Person::getInfo() + " " + std::to_string(contractDuration);
}


