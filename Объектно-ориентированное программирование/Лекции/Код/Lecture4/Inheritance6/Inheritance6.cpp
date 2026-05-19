#include <iostream>
#include "Manager.h"
#include "Employee.h"

using namespace std;

int main()
{
    //Person p("rrt", 46);
	Manager m("Maslov S.S.", 33);
	m.setDocument(34345);
    m.setDocument("3243");
	m.setSalary(20000);
    m.setAge(22);
	cout << m.getInfo()<<endl;
	Person* p = &m;
    cout << p->getInfo();
}



















/*
class Device {
public:
    Device() {
        cout << "Device constructor called" << endl;
    }
    virtual void turn_on() {
        cout << "Device is on." << endl;
    }
};

class Computer : virtual public Device {
public:
    Computer() {
        cout << "Computer constructor called" << endl;
    }
    void turn_on() {
        cout << "Computer is on." << endl;
    }
};

class Monitor : virtual public Device {
public:
    Monitor() {
        cout << "Monitor constructor called" << endl;
    }
    void turn_on() {
        cout << "Monitor is on." << endl;
    }
};

class Laptop : public Computer, public Monitor {
public:
    void turn_on() {
        cout << "Laptop is on." << endl;
    }

};

int main() {
    Laptop laptop;
    laptop.turn_on();
    //((Computer)laptop).turn_on();
    return 0;
}
*/