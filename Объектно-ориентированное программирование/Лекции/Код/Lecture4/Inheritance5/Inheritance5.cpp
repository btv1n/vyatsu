#include <iostream>
#include"Employee.h"
#include "TemporaryWorker.h"
#include <vector>


Person* generate();

using namespace std;
int main()
{
    Employee e("Petrov P.P.", 21);
    e.setDocument(123);
    e.setDocument("012345");
    e.get();
 
    TemporaryWorker t("Petrov P.P.", 21);
    t.setDocument(8);
    t.setDocument("188392");

    
    vector<Person *> vec;
    vec.push_back(&e);
    vec.push_back(&t);
    vec.push_back(generate());// new Employee("Sidorov S.S.", 1525));
    for (Person * person : vec)
        cout << person->getInfo() << endl;

    Person** arr = new Person * [3];
    arr[0] = &e;
    arr[1] = &t;
    arr[2] = (generate()); // new Employee("Sidorov S.S.", 1525);
    arr[2]-> setDocument("872138");
    for (int i=0; i<3; i++)
        cout << arr[i]->getInfo() << endl;
    delete[] arr;
}

Person* generate()
{
    if (rand() % 2 == 0)
    {
        Employee* e = new Employee("Lukin L.L.", 1525);
        return e;
    }
    else
    {
        TemporaryWorker* t = new TemporaryWorker("Torbeev T.T.", 21);
        return t;
    }
}
