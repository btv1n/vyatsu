#pragma once
#include <string>
#include <windows.h>

class Patient
{
private:
	std::string surname;
	std::string name;
	std::string patronymic;
	std::string policy;
	int dayBirthday;
	int monthBirthday;
	int yearBirthday;

public:
	Patient(std::string surname, std::string name, std::string patronymic);
	Patient(std::string data);
	Patient(std::string fullname, std::string policy);
	Patient(Patient const& patient);
	void setBirthday(SYSTEMTIME birthday);
	void setBirthday(int dayBirthday, int monthBirthday, int yearBirthday);
	void setPolicy(std::string const & policy);
	std::string toCsvString();
	std::string toString();
};

