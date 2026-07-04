#include <iostream>
#include <string>
#include <algorithm>
#include "LNumber/lnum.h"

using std::cin;
using std::cout;
using std::string;

int main(int, char **)
{
    bool running = true;
    string s;
    while (running)
    {
        cout << "> ";
        std::getline(cin, s);

        s.erase(std::remove(s.begin(), s.end(), ' '), s.end());

        size_t oper_index = 0;

        for (size_t i = 1; i < s.size(); i++)
        {
            if (
                s[i] == '+' 
                || s[i] == '-' 
                || s[i] == '/' 
                || s[i] == '%' 
                || s[i] == '^' 
                || s[i] == '*' 
                || s[i] == '<' 
                || s[i] == '>' 
                || s[i] == '='
            )
            {
                oper_index = i;
                break;
            }
        }

        try
        {
            if (oper_index == 0 || oper_index == s.size() - 1)
                throw std::invalid_argument("invalid format");

            LNum lhs(s.substr(0, oper_index));
            LNum rhs(s.substr(oper_index + 1));
            LNum result;

            switch (s[oper_index])
            {
            case '+':
                result = lhs + rhs;
                break;
            case '-':
                result = lhs - rhs;
                break;
            case '/':
                result = lhs / rhs;
                break;
            case '%':
                result = lhs % rhs;
                break;
            case '^':
                lhs.pow(rhs);
                result = lhs;
                break;
            case '*':
                result = lhs * rhs;
                break;
            case '<':
                cout << (lhs < rhs) << "\n\n";
                break;
            case '>':
                cout << (lhs > rhs) << "\n\n";
                break;
            case '=':
                cout << (lhs == rhs) << "\n\n";
                break;
            default:
                throw std::invalid_argument("invalid operation");
                break;
            }
            if (result.first)
                cout << "= " << result << "\n\n";
        }
        catch (const std::exception &e)
        {
            std::cerr << e.what() << "\n\n";
        }
    }

    return 0;
}
