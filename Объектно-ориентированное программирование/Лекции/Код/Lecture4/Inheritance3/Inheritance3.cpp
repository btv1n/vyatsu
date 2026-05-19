#include <iostream>
#include "Employee.h"

using namespace std;
int main()
{
    Employee e("Petrov P.P.", 21);
    e.setDocument(123);
    e.setDocument("012345");
    cout << e.getInfo() << endl;

    Person *ptr = &e; // указатель на базовый класс проинициализирован адресом существующего объекта
    //ptr->setDocument("878787");
    cout << ptr->getInfo() << endl; 

}
