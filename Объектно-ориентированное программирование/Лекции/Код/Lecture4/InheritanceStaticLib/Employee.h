#pragma once
#include "Person.h"
class Employee :
    public Person
{
private:
    int id;
public:
    Employee(std::string fullname, int age, int id);
};

