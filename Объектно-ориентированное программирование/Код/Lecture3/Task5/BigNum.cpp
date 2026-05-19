#include "BigNum.h"

BigNum::BigNum()
{
	value.push_back(0);
}

BigNum::BigNum(size_t n)
{
	value.assign(n, 0);
}

BigNum::BigNum(std::string s)
{
	for (size_t i = 0; i < s.length(); i++)
		value.push_back(s[i] - '0');
}

BigNum::BigNum(BigNum const& b)
{
	for (size_t i = 0; i < b.length(); i++)
		value.push_back(b.value[i]);
}

size_t BigNum::length() const
{
	return value.size();
}

BigNum& BigNum::operator ++() { //prefix
	//increment
	int d = (value[value.size() - 1] + 1) / 10;
	value[value.size() - 1] = (value[value.size() - 1] + 1) % 10;
	if (d == 1)
	{
		for (int i = value.size() - 2; i >= 0; i--)
		{
			int temp = (value[i] + d) / 10;
			value[i] = (value[i] + d) % 10;
			d = temp;
		}
		if (d > 0)
			value.insert(value.begin(), d);
	}
	return *this;
}

BigNum BigNum::operator ++(int) { //postfix
	BigNum tmp(*this);
	++(*this);
	return tmp;
}

BigNum::operator std::string() const
{
	std::string s = "";
	for (size_t i = 0; i < value.size(); i++)
		s += std::to_string(value[i]);
	return s;
}

void BigNum::operator()(std::string s)
{
	value.clear();
	for (size_t i = 0; i < s.length(); i++)
		value.push_back(s[i] - '0');
}

std::istream& operator>>(std::istream& is, BigNum& b)
{
	std::string s = "";
	is >> s;
	b(s);
	return is;
}

std::ostream& operator<<(std::ostream& os, BigNum const& b)
{
	BigNum temp(b);
	os << (std::string)temp;
	return os;
}


BigNum::PartBigNum BigNum::operator[](int start) const
{
	return BigNum::PartBigNum(*this, start);
}

BigNum BigNum::PartBigNum::operator[](int j) const
{
	std::string temp = (std::string)partvalue;
	temp.erase(temp.begin(), temp.begin() + left - 1);
	temp.erase(temp.begin()+(j - left) , temp.end());
	return BigNum(temp);
}
