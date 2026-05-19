#include <iostream>
#include "Matrix.h"

//https://ru.stackoverflow.com/questions/494672/%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D1%8B-%D0%B2-c-%D0%9E%D0%B3%D1%80%D0%B0%D0%BD%D0%B8%D1%87%D0%B5%D0%BD%D0%B8%D0%B5-%D1%82%D0%B8%D0%BF%D0%B0
//https://ru.stackoverflow.com/questions/632000/%D0%9A%D0%B0%D0%BA-%D0%B7%D0%B0%D0%B4%D0%B0%D1%82%D1%8C-%D0%BE%D0%B3%D1%80%D0%B0%D0%BD%D0%B8%D1%87%D0%B5%D0%BD%D0%B8%D1%8F-%D0%BD%D0%B0-%D0%BF%D0%B0%D1%80%D0%B0%D0%BC%D0%B5%D1%82%D1%80%D1%8B-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%BD%D1%8B%D1%85-%D0%BA%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2
//

int main()
{
    Matrix<int> m(2, 3);
    m.print();
    Matrix<double> m2(4, 3);
    m2.print();
    //Matrix<unsigned int> m3(2, 3);

}


