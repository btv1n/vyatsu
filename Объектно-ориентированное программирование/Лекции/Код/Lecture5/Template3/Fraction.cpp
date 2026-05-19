#include "Fraction.h"


void Fraction::reduction()
{
	unsigned int n = gcd();
	numerator /= n;
	denominator /= n;
}

unsigned int Fraction::gcd() const
{
	unsigned int a = abs(numerator);
	unsigned int b = denominator;

	while (a > 0 && b > 0)
		if (a > b)
			a %= b;
		else
			b %= a;
	return a + b;
}

Fraction::Fraction(int numerator, unsigned int denominator)
{
	if (denominator != 0)
	{
		this->numerator = numerator;
		this->denominator = denominator;
		reduction();
	}
	else
		throw std::exception("division by zero");
}

Fraction::Fraction(Fraction const& f)
{
	numerator = f.numerator;
	denominator = f.denominator;
}

Fraction& Fraction::operator=(Fraction const& f)
{
	if (this != &f)
	{
		numerator = f.numerator;
		denominator = f.denominator;
	}
	return *this;
}

Fraction Fraction::operator+(Fraction const& f) const
{
	return Fraction(numerator * f.denominator + denominator * f.numerator, denominator * f.denominator);
}

Fraction Fraction::operator-(Fraction const& f) const
{
	return Fraction(numerator * f.denominator - denominator * f.numerator, denominator * f.denominator);
}

Fraction Fraction::operator*(Fraction const& f) const
{
	return Fraction(numerator * f.numerator, denominator * f.denominator);
}

Fraction Fraction::operator/(Fraction const& f) const
{
	return Fraction(numerator * f.denominator, denominator * f.numerator);
}

Fraction Fraction::operator*(int const& value) const
{
	return Fraction(numerator * value, denominator);
}

Fraction Fraction::exponentiering(int const& exponent) const
{
	if (numerator == 0) return Fraction(0, 1);
	else
	{
		if (exponent == 0) return Fraction(1, 1);
		else
			if (exponent > 0)
			{
				Fraction f(1, 1);
				for (int i = 0; i < exponent; i++)
					f *= (*this);
				return f;
			}
			else
			{
				Fraction f(denominator, numerator);
				for (int i = 0; i < exponent; i++)
					f *= (*this);
				return f;
			}
	}
}

Fraction Fraction::operator-() const
{
	return Fraction((-1) * numerator, denominator);
}

Fraction::operator double() const
{
	return (1.0 * numerator) / denominator;
}

Fraction::operator std::string() const
{
	return std::to_string(numerator) + "/" + std::to_string(denominator);
}

bool Fraction::operator==(Fraction const& f)
{
	return (numerator == f.numerator) && (denominator * f.denominator);
}

bool Fraction::operator!=(Fraction const& f)
{
	return !(*this == f);
}

bool Fraction::operator<(Fraction const& f)
{
	return numerator * f.denominator < denominator* f.numerator;
}

bool Fraction::operator>(Fraction const& f)
{
	return !((*this < f) || (*this == f));
}

bool Fraction::operator>=(Fraction const& f)
{
	return !(*this < f);
}

Fraction& Fraction::operator+=(Fraction const& v)
{
	Fraction temp = *this + v;
	numerator = temp.numerator;
	denominator = temp.denominator;
	return *this;
}

Fraction& Fraction::operator-=(Fraction const& v)
{
	Fraction temp = *this - v;
	numerator = temp.numerator;
	denominator = temp.denominator;
	return *this;
}

Fraction& Fraction::operator*=(Fraction const& v)
{
	numerator *= v.numerator;
	denominator *= v.denominator;
	return *this;
}

Fraction& Fraction::operator/=(Fraction const& v)
{
	numerator *= v.denominator;
	denominator *= v.numerator;
	return *this;
}

bool Fraction::operator<=(Fraction const& f)
{
	return !(*this > f);
}

Fraction operator*(int const& value, Fraction const& f)
{
	return Fraction(f) * value;
}

std::istream& operator>>(std::istream& in, Fraction& b)
{
	in >> b.numerator >> b.denominator;
	return in;
}

std::ostream& operator<<(std::ostream& out, Fraction const& b)
{
	out << std::to_string(b.numerator) + "/" + std::to_string(b.denominator);
	return out;
}

