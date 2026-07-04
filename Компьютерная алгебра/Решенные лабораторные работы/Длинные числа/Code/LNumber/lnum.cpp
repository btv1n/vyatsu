#include "lnum.h"
#include <sstream>

LNum::LNum(std::string s)
{
    Node2<char> *ptr = nullptr;
    last = nullptr;

    if (s.size() != 0 && s[0] == '-')
    {
        s = s.substr(1);
        positive = false;
    }
    else
        positive = true;

    for (size_t i = 0; i < s.size(); i++)
    {
        char c = s[i];
        if (c > '9' || c < '0')
            throw std::invalid_argument("invalid format");
        if (last == nullptr)
        {
            last = new Node2<char>(c - '0');
            ptr = last;
        }
        else
            ptr = ptr->add_before(c - '0');
    }
    first = ptr;
}

LNum::LNum(const LNum &other)
{
    Node2<char> *ptr = nullptr;
    first = nullptr;

    for (auto other_ptr = other.first; other_ptr != nullptr; other_ptr = other_ptr->next)
    {
        if (ptr == nullptr)
        {
            ptr = new Node2<char>(other_ptr->data);
            first = ptr;
        }
        else
            ptr = ptr->add_after(other_ptr->data);
    }

    positive = other.positive;
    last = ptr;
}

LNum::LNum()
{
    first = nullptr;
    last = nullptr;
    positive = true;
}

LNum &LNum::operator=(const LNum &other)
{
    if (this->first == other.first)
        return *this;
    delete first;

    Node2<char> *ptr = nullptr;
    first = nullptr;

    for (auto other_ptr = other.first; other_ptr != nullptr; other_ptr = other_ptr->next)
    {
        if (ptr == nullptr)
        {
            ptr = new Node2<char>(other_ptr->data);
            first = ptr;
        }
        else
            ptr = ptr->add_after(other_ptr->data);
    }

    positive = other.positive;
    last = ptr;

    return *this;
}

void LNum::add(char n)
{
    if (n > 9)
        throw std::invalid_argument("invalid char");

    if (last != nullptr)
    {
        Node2<char> *t = new Node2<char>(n);
        last->next = t;
        t->prev = last;
        last = t;
    }
    else
    {
        first = new Node2<char>(n);
        last = first;
    }
}

void LNum::pow(const LNum &n)
{
    if (n == LNum("0"))
    {
        *this = LNum("1");
    }
    else if (n % LNum("2") == LNum("1"))
    {
        LNum p1(*this);
        p1.pow(n - LNum("1"));
        *this = (*this) * p1;
    }
    else
    {
        this->pow(n / LNum("2"));
        *this = (*this) * (*this);
    }
}

void LNum::pow(const unsigned int &n)
{
    if (n == 0)
    {
        *this = LNum("1");
    }
    else if (n % 2 == 1)
    {
        LNum p1(*this);
        p1.pow(n - 1);
        *this = (*this) * p1;
    }
    else
    {
        this->pow(n / 2);
        *this = (*this) * (*this);
    }
}

bool LNum::abs_less(const LNum &other) const
{
    auto rptr = this->first;
    auto lptr = other.first;

    bool result = false;
    while (rptr != nullptr && lptr != nullptr)
    {
        if (result)
            result = rptr->data <= lptr->data;
        else
            result = rptr->data < lptr->data;
        rptr = rptr->next;
        lptr = lptr->next;
    }

    if (!(rptr == nullptr && lptr == nullptr))
        result = rptr == nullptr;
    return result;
}

bool LNum::abs_equal(const LNum &other) const
{
    auto rptr = this->first;
    auto lptr = other.first;

    while (rptr != nullptr && lptr != nullptr && rptr->data == lptr->data)
    {
        rptr = rptr->next;
        lptr = lptr->next;
    }

    if (rptr != nullptr || lptr != nullptr)
        return false;
    return true;
}

char LNum::pop_last()
{
    Node2<char> *t = last;
    char data = t->data;
    last = t->prev;
    last->next = nullptr;
    delete t;
    return data;
}

std::string LNum::to_string() const
{
    std::stringstream ss;
    ss << (*this);
    return ss.str();
}

void LNum::add_begin(char n)
{
    if (n > 9)
        throw std::invalid_argument("invalid char");

    if (first != nullptr)
    {
        Node2<char> *t = new Node2<char>(n);
        first->prev = t;
        t->next = first;
        first = t;
    }
    else
    {
        first = new Node2<char>(n);
        last = first;
    }
}

std::ostream &operator<<(std::ostream &os, const LNum &num)
{
    if (!num.positive)
        os << "-";
    for (Node2<char> *ptr = num.last; ptr != nullptr; ptr = ptr->prev)
        os << (char)(ptr->data + '0');

    return os;
}

LNum operator+(const LNum &rhs, const LNum &lhs)
{
    bool lpos = lhs.positive, rpos = rhs.positive;
    LNum result;

    if (rhs.positive != lhs.positive)
    {
        if (rhs.abs_equal(lhs))
            return LNum("0");
        else if (rpos && rhs.abs_less(lhs))
        {
            rpos = false;
            lpos = true;
            result.positive = false;
        }
        else if (lpos && lhs.abs_less(rhs))
        {
            rpos = true;
            lpos = false;
            result.positive = false;
        }
    }
    else
    {
        lpos = true;
        rpos = true;
        result.positive = rhs.positive;
    }

    int shift = 0;

    auto rptr = rhs.first;
    auto lptr = lhs.first;

    while (rptr != nullptr && lptr != nullptr)
    {
        int sum_result = shift;
        if (rpos)
            sum_result += rptr->data;
        else
            sum_result -= rptr->data;

        if (lpos)
            sum_result += lptr->data;
        else
            sum_result -= lptr->data;

        shift = sum_result < 0 ? -1 : sum_result / 10;

        result.add((sum_result % 10) < 0 ? 10 + sum_result % 10 : sum_result % 10);

        rptr = rptr->next;
        lptr = lptr->next;
    }

    while (rptr != nullptr)
    {
        int sum_result = rptr->data + shift;
        shift = sum_result / 10;
        result.add(sum_result % 10);
        rptr = rptr->next;
    }

    while (lptr != nullptr)
    {
        int sum_result = lptr->data + shift;
        shift = sum_result / 10;
        if (shift)
            sum_result -= 10;
        result.add(sum_result);
        lptr = lptr->next;
    }

    if (shift != 0)
        result.add(shift);

    while (result.last && result.last->data == 0 && result.last != result.first)
        result.pop_last();

    return result;
}

LNum operator-(const LNum &rhs, const LNum &lhs)
{
    LNum nlhs(lhs);
    nlhs.positive = !lhs.positive;
    return rhs + nlhs;
}

LNum operator*(const LNum &rhs, const LNum &lhs)
{
    LNum result("");

    int i = 0;
    for (auto rptr = rhs.first; rptr != nullptr; rptr = rptr->next)
    {
        LNum mul_result = lhs * (int)rptr->data;
        for (int j = 0; j < i; j++)
            mul_result.add_begin(0);
        mul_result.positive = true;
        result = result + mul_result;
        i++;
    }

    result.positive = rhs.positive == lhs.positive;
    return result;
}

LNum operator/(const LNum &rhs, const LNum &lhs)
{
    // Деление столбиком, t - остаток, result - результат
    LNum t, result;
    auto rptr = rhs.last;

    while (rptr != nullptr)
    {
        while (rptr != nullptr && t.first == nullptr && rptr->data == 0)
        {
            result.add_begin(0);
            rptr = rptr->prev;
        }
        int shifts = 0;
        while (rptr != nullptr && t.abs_less(lhs))
        {
            t.add_begin(rptr->data);
            rptr = rptr->prev;
            shifts++;
        }

        while (shifts > 1 && result.first != nullptr)
        {
            result.add_begin(0);
            shifts--;
        }

        if (t.first != nullptr)
        {
            int d = 0;

            while (!t.abs_less(lhs * (d + 1)))
                d++;

            result.add_begin(d);
            LNum dt = lhs * d;
            dt.positive = true;
            if (t == dt)
                t = LNum();
            else
                t = t - dt;
        }
    }

    result.positive = lhs.positive == rhs.positive;
    if (!result.positive)
        return result - LNum("1");
    return result;
}

LNum operator%(const LNum &rhs, const LNum &lhs)
{
    const LNum d = rhs / lhs;
    const LNum mul = lhs * d;

    if (rhs.positive && lhs.positive)
        return rhs - mul;
    if (!rhs.positive && lhs.positive)
        return mul + rhs;
    if (rhs.positive && !lhs.positive)
        return rhs - mul;
    return rhs + mul;
}

LNum operator*(const int &rhs, const LNum &lhs)
{
    LNum result("");

    result.positive = rhs < 0 == !lhs.positive;
    int arhs = abs(rhs);
    int shift = 0;
    for (auto ptr = lhs.first; ptr != nullptr; ptr = ptr->next)
    {
        int mul_result = arhs * ptr->data + shift;
        shift = mul_result / 10;
        result.add(mul_result % 10);
    }
    while (shift != 0)
    {
        result.add(shift % 10);
        shift /= 10;
    }

    return result;
}

LNum operator*(const LNum &rhs, const int &lhs)
{
    return lhs * rhs;
}

// 123 < 124
bool operator<(const LNum &rhs, const LNum &lhs)
{
    if (rhs.positive != lhs.positive)
        return lhs.positive;
    if (rhs == lhs)
        return false;

    auto rptr = rhs.first;
    auto lptr = lhs.first;
    bool result = false;
    while (rptr != nullptr && lptr != nullptr)
    {
        if (result)
            result = rptr->data <= lptr->data;
        else
            result = rptr->data < lptr->data;
        rptr = rptr->next;
        lptr = lptr->next;
    }

    if (!(rptr == nullptr && lptr == nullptr))
        result = rptr == nullptr;
    return rhs.positive ? result : !result;
}

bool operator>(const LNum &rhs, const LNum &lhs)
{
    if (rhs.positive != lhs.positive)
        return rhs.positive;
    if (rhs == lhs)
        return false;

    auto rptr = rhs.first;
    auto lptr = lhs.first;
    bool result = false;
    while (rptr != nullptr && lptr != nullptr)
    {
        if (result)
            result = rptr->data >= lptr->data;
        else
            result = rptr->data > lptr->data;
        rptr = rptr->next;
        lptr = lptr->next;
    }

    if (!(rptr == nullptr && lptr == nullptr))
        result = lptr == nullptr;
    return rhs.positive ? result : !result;
}

bool operator<=(const LNum &rhs, const LNum &lhs)
{
    return !(rhs > lhs);
}

bool operator>=(const LNum &rhs, const LNum &lhs)
{
    return !(rhs < lhs);
}

bool operator==(const LNum &rhs, const LNum &lhs)
{
    if (rhs.positive != lhs.positive)
        return false;

    auto rptr = rhs.first;
    auto lptr = lhs.first;
    while (rptr != nullptr && lptr != nullptr)
    {
        if (rptr->data != lptr->data)
            return false;
        rptr = rptr->next;
        lptr = lptr->next;
    }

    return rptr == lptr;
}

bool operator==(const LNum &rhs, int lhs)
{
    return lhs == rhs;
}

bool operator==(int rhs, const LNum &lhs)
{
    if (rhs > 0 != lhs.positive && rhs != 0)
        return false;

    int rptr = 10;
    rhs = abs(rhs);

    auto lptr = lhs.first;
    while ((rptr / 10 <= rhs || rhs == 0) && lptr != nullptr)
    {
        if (((rhs % rptr) / (rptr / 10)) != lptr->data)
            return false;
        rptr *= 10;
        lptr = lptr->next;
    }

    return (rptr / 10 > rhs || rhs == 0) && lptr == nullptr;
}

LNum fact(int n)
{
    LNum result("1");
    for (int i = 2; i <= n; i++)
        result = i * result;

    return result;
}
