#include <iostream>
#include <string>
#include <algorithm>
#include "LNumber/lnum.h"

using std::cin;
using std::cout;
using std::endl;
using std::string;

int main(int argn, char **argv)
{
    // (9060173 * 9028091) - (1450140 * 2732940) + 560099730
    cout << "result: " << (LNum("9060173") * LNum("9028091")) - (LNum("1450140") * LNum("2732940")) + LNum("560099730")
         << endl;
    return 0;
}
