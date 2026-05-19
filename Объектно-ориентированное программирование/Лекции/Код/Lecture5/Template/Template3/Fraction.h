#pragma once
#include <string>
#include <fstream>


/// <summary>
///  Класс несократимая дробь. Числитель целое число, знаменатель - натуральное.
/// </summary>

class Fraction
{
private:
	int numerator;
	unsigned int denominator;

#pragma region Helper methods

	/// <summary>
	/// Метод сокращения дроби
	/// </summary>
	void reduction();
	/// <summary>
	/// Поиск НОД числителя и знаменателя
	/// </summary>
	/// <returns>НОД</returns>
	/// <remarks>Для вычисления используют модуль числителя</remarks>
	unsigned int gcd() const;
	//static unsigned int gcd(unsigned int numerator, unsigned int denominator);

#pragma endregion

public:

#pragma region Constructors and assignment operator 

	Fraction(int numerator, unsigned int denominator = 1);
	Fraction(Fraction const&);
	Fraction& operator=(Fraction const&);

#pragma endregion

#pragma region Arithmetic operators
	
	Fraction operator-() const;
	Fraction operator+(Fraction const&) const;
	Fraction operator-(Fraction const&) const;
	Fraction operator*(Fraction const&) const;
	Fraction operator/(Fraction const&) const;
	Fraction operator*(int const&) const; //*
	Fraction exponentiering(int const&) const; // *
	friend Fraction operator*(int const&, Fraction const&); //*

#pragma endregion

#pragma region Type conversion

	operator double() const;
	explicit operator std::string() const;

#pragma endregion

#pragma region Comparison operators

	bool operator==(Fraction const& f);
	bool operator!=(Fraction const& f); 
	bool operator<(Fraction const& f); //*
	bool operator>(Fraction const& f); //*
	bool operator<=(Fraction const& f); //*
	bool operator>=(Fraction const& f); //*

#pragma endregion

#pragma region Assignment operators with operation

	Fraction& operator +=(Fraction  const& v);
	Fraction& operator -=(Fraction  const& v);//*
	Fraction& operator *=(Fraction  const& v);//*
	Fraction& operator /=(Fraction  const& v);//*

#pragma endregion

#pragma region Friend IOStream

	friend std::istream& operator >>(std::istream& in, Fraction& b);
	friend std::ostream& operator <<(std::ostream& out, Fraction const& b);

#pragma endregion

};


