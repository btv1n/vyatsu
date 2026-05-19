#include <iostream>
#include"Employee.h"
#include "TemporaryWorker.h"
#include <vector>


using namespace std;
int main()
{
    Employee e("Petrov P.P.", 21);
    e.setDocument(123);
    e.setDocument("012345");
    cout << e.getInfo() << endl;

    TemporaryWorker t("Petrov P.P.", 21);
    t.setDocument(8);
    t.setDocument("188392");
    cout << t.getInfo() << endl;


    Person* ptr_e = new Employee(e); // указатель на базовый класс проинициализирован адресом существующего объекта
    cout << ptr_e->getInfo() << endl;

    Person* ptr_t = &t; // указатель на базовый класс проинициализирован адресом существующего объекта
    cout << ptr_t->getInfo() << endl;

   
    delete ptr_e;
    //delete ptr_t;
}

