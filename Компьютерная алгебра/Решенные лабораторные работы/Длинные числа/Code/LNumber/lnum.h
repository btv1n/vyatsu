#pragma once
#include "../node/node2.h"
#include <string>

struct LNum
{
    Node2<char> *first;
    Node2<char> *last;
    bool positive;
    explicit LNum(std::string s);
    LNum(const LNum &other);
    LNum();
    LNum &operator=(const LNum &other);

    void add_begin(char n);
    void add(char n);

    void pow(const LNum &n);
    void pow(const unsigned int &n);

    inline ~LNum() { delete first; }

    bool abs_less(const LNum &other) const;
    bool abs_equal(const LNum &other) const;

    char pop_last();

    std::string to_string() const;
};

std::ostream &operator<<(std::ostream &os, const LNum &num);

LNum operator+(const LNum &rhs, const LNum &lhs);
LNum operator-(const LNum &rhs, const LNum &lhs);

LNum operator*(const LNum &rhs, const LNum &lhs);

LNum operator/(const LNum &rhs, const LNum &lhs);
LNum operator%(const LNum &rhs, const LNum &lhs);

LNum operator*(const int &rhs, const LNum &lhs);
LNum operator*(const LNum &rhs, const int &lhs);

bool operator<(const LNum &rhs, const LNum &lhs);
bool operator>(const LNum &rhs, const LNum &lhs);
bool operator<=(const LNum &rhs, const LNum &lhs);
bool operator>=(const LNum &rhs, const LNum &lhs);
bool operator==(const LNum &rhs, const LNum &lhs);
bool operator==(const LNum &rhs, int lhs);
bool operator==(int rhs, const LNum &lhs);

LNum fact(int n);
