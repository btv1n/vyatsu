#pragma once
#include <vector>
#include <string>
#include <iostream>

class BigNum
{
private:
	std::vector<int> value;

public:
	BigNum();
	BigNum(size_t n);
	BigNum(std::string s);
	BigNum(BigNum const & b);
	
	size_t length() const;

	BigNum& operator ++();  //prefix
	BigNum operator ++(int); //postfix

	operator std::string() const;
	void operator()(std::string s);
};

std::istream& operator >>(std::istream& is, BigNum& b);

std::ostream& operator <<(std::ostream& os, BigNum const& b);