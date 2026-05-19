
#include <iostream>
using namespace std;

class Device {
public: 
    Device() { cout << "Device constructor called" << endl; }
      void turn_on() { cout << "Device is on." << endl; }
      ~Device(){ cout << "Device destructor called" << endl; }
};

class Computer : virtual public Device {
public: 
    Computer() { cout << "Computer constructor called" << endl; }
    ~Computer() { cout << "Computer destructor called" << endl; }
};

class Monitor : virtual public Device {
public: 
    Monitor() { cout << "Monitor constructor called" << endl; }
    ~Monitor() { cout << "Monitor destructor called" << endl; }
};

class Laptop : public Computer, public Monitor {};


int main()
{
    Laptop l;
    //l.turn_on();
}

