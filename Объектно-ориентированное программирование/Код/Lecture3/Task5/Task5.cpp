#include <iostream>
#include "BigNum.h"
int main()
{    
    BigNum a("123456789");
    //BigNum::PartBigNum d = a[2];
    //BigNum f = d[5];
    BigNum b = a[2][5];
    std::cout << (std::string)b;
}

