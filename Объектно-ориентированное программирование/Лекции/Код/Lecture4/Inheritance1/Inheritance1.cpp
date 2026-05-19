#include <iostream>
#include "Employee.h"
#include "Person.h"

using namespace std;

int main()
{
    Employee e("Ivanov I.I.", 23, 2812);
    cout << e.getFullname() << " " << e.getAge() << " " << e.getWorkPass();

    Person & p1 = e; // Employee &  -> Person &
    Person* p2 = &e; // Employee *  -> Person *
    Employee e1 = e;
}
