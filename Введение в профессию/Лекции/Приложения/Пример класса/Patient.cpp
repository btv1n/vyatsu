#include "Patient.h"
#include <sstream>
#include <regex>
using namespace std;

Patient::Patient(string surname, string name, string patronymic)
{
	this->surname = surname;
	this->name = name;
	this->patronymic = patronymic;
	dayBirthday = 1;
	monthBirthday = 1;
	yearBirthday = 1901;
	policy = "";
}

Patient::Patient(std::string data)
{
	regex r1(R"(\")");
	smatch m1;
	data = regex_replace(data, r1, "");

	regex r(R"(;)");
	smatch m;
	vector<string> v;
	while (regex_search(data, m, r))
	{
		v.push_back(m.prefix());
		data = m.suffix();
	}
	v.push_back(data);
	surname = v[0];
	name = v[1];
	patronymic = v[2];
	policy = v[3];
	dayBirthday = stoi(v[4]);
	monthBirthday = stoi(v[5]);
	yearBirthday = stoi(v[6]);

}

Patient::Patient(std::string fullname, std::string policy)
{
	stringstream ss(fullname);
	string surname, name, patronymic;
	ss >> surname >> name >> patronymic;
	this->surname = surname;
	this->name = name;
	this->patronymic = patronymic;
	this->policy = policy;
}

Patient::Patient(Patient const& patient)
{
	surname = patient.surname;
	name = patient.name;
	patronymic = patient.patronymic;
	policy = patient.policy;
	dayBirthday = patient.dayBirthday;
	monthBirthday = patient.monthBirthday;
	yearBirthday = patient.yearBirthday;
}

void Patient::setBirthday(SYSTEMTIME birthday)
{
	dayBirthday = birthday.wDay;
	monthBirthday = birthday.wMonth;
	yearBirthday = birthday.wYear;
}

void Patient::setBirthday(int dayBirthday, int monthBirthday, int yearBirthday)
{
	this->dayBirthday = dayBirthday;
	this->monthBirthday = monthBirthday;
	this->yearBirthday = yearBirthday;
}

void Patient::setPolicy(std::string const& policy)
{
	this->policy = policy;
}

std::string Patient::toCsvString()
{
	return "\""+surname + "\";\"" + name + "\";\"" + patronymic + "\";\"" + policy + "\";" + std::to_string(dayBirthday)+
		+";" + std::to_string(monthBirthday) + ";" + std::to_string(yearBirthday)+"\n";
}

std::string Patient::toString()
{
	return surname + " " + name + " " + patronymic + " " + policy + " " + std::to_string(dayBirthday) +
		+" " + std::to_string(monthBirthday) + " " + std::to_string(yearBirthday);

}
