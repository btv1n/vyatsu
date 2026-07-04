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
    cout << "result: " << LNum("1230004500007000900000300400000") % LNum("3")
         << endl;
    return 0;
}
