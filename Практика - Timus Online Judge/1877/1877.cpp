#include <iostream>

using namespace std;

int main()
{
	int codeLock_1, codeLock_2;

	cin >> codeLock_1 >> codeLock_2;

	if (codeLock_1 % 2 == 0 || codeLock_2 % 2 != 0)
		cout << "yes" << endl;
	else
		cout << "no" << endl;

	return 0;
}
