#pragma once
#include <vector>
#include <string>
#include <iostream>

class BigNum
{
private:
	std::vector<int> value;

public:
#pragma region MyRegion


	BigNum();
	BigNum(size_t n);
	BigNum(std::string s);
	BigNum(BigNum const& b);

	size_t length() const;

	BigNum& operator ++();  //prefix
	BigNum operator ++(int); //postfix

	operator std::string() const;
	void operator()(std::string s);
#pragma endregion

	class PartBigNum
	{
	private:
        BigNum const & partvalue;
		int left;
	public:
        PartBigNum(BigNum const &b, int i) :partvalue(b), left(i) {};
        BigNum operator[](int i) const;
	};

    PartBigNum operator[](int i) const;
};

std::istream& operator >>(std::istream& is, BigNum& b);

std::ostream& operator <<(std::ostream& os, BigNum const& b);










/*
#include <cstddef> // size_t
#include <iostream>

struct String {
    String(const char* str = "");
    String(size_t n, char c);
    ~String();

    String(const String& other);
    String& operator=(const String& other);

    void append(const String& other);

    class PreparedString {
    public:
        void write(std::ostream& out) const;

        String operator[](unsigned int to) const;

        PreparedString(const String& source_in, unsigned int from_in);

    private:
        const String& source;
        unsigned int from;
    };

    void write(std::ostream& out) const;

    PreparedString operator[](unsigned int from) const;

    size_t size;
    char* str;
};

void String::PreparedString::write(std::ostream& out) const
{
    out << (this->source.str + this->from) << std::endl;
}

String String::PreparedString::operator[](unsigned int to) const
{
    char* tmp_c_str = new char[to - from + 1];
    for (char* at_src = source.str + from, *at_dst = tmp_c_str, *last = source.str + to; at_src != last; ++at_src, ++at_dst)
        *at_dst = *at_src;
    *(tmp_c_str + to - from) = '\0';
    String tmp_string(tmp_c_str);
    delete[] tmp_c_str;

    return tmp_string;
}

String::PreparedString::PreparedString(const String& source_in, unsigned int from_in) : source(source_in), from(from_in)
{
}

String::PreparedString String::operator[](unsigned int from) const
{
    return String::PreparedString(*this, from);
}

void String::write(std::ostream& out) const
{
    out << this->str << std::endl;
}
*/