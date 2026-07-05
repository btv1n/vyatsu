#include <iostream>
#include <regex>
#include <string>
#include <vector>

using namespace std;

bool isFileName(string s);

bool hasFileName(string s);




string spaceDelete(string s);

string deleteFirstSpace(string s);

string deleteLastSpace(string s);

string changePoint(string s);

vector<string> getSentence(string text);

int main()
{

// Задача поиска ходя бы одного

    string input = "ABC:1;->   PQR:2;;;   XYZ:3 << < ";
    const regex r(R"((\w+):(\w+);)");
    smatch m;
    if (regex_search(input, m, r))
        cout << "prefix: " << m.prefix() << "\n"
        << "position: " << m.position() << "\n"
        << "str: " << m.str() << "\n"
        << "length: " << m.length() << "\n"
        << "suffix: " << m.suffix() << "\n";

// Задача соответствия шаблону
    input = "ABC:1;";
    smatch m1;
    if (regex_match(input, m1, r))
    {
        for (size_t i = 0; i < m1.size(); ++i)
        {
            ssub_match sub_match = m1[i];
            std::string piece = sub_match.str();
            std::cout << "  submatch " << i << ": " << piece << '\n';
        }
    }

//Задача поиска всех 
 
    std::string log(R"(
        Speed:	366
        Mass:	35
        Speed:	378
        Mass:	32
        Speed:	400
	Mass:	30)");
    std::regex r1(R"(Speed:\t\d*)");
    std::smatch sm;
    while (regex_search(log, sm, r1))
    {
        std::cout << sm.str() << '\n';
        log = sm.suffix();
    }


}

bool isFileName(string s)
{
    const regex r(R"(([\w\-$])+.[a-zA-Z]([\w]){1,3})");
    smatch m;
    return regex_match(s, m, r);
}

bool hasFileName(string s)
{
    const regex r(R"(([\w\-$])+.[a-zA-Z]([\w]){1,3})");
    smatch m;
    return regex_search(s, m, r);
}

string spaceDelete(string s)
{
    regex r(R"((\s)+)");
    string news = regex_replace(s, r, " ");
    return news;
}
