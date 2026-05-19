#pragma once
#include <string>
#include <vector>

/// <summary>
/// N-мерный вектор над полем действительных чисел
/// </summary>
class Vector
{
private:
	/// <summary>
	/// размерность пространства
	/// </summary>
	size_t size;
	double* coordinates;

public:
	/// <summary>
	/// создание нулевого вектора n-мерного пространства
	/// </summary>
	/// <param name="n">размерность</param>
	Vector(size_t n);
	
	/// <summary>
	/// создание вектора из элементов строки	
	/// </summary>
	/// <param name="s">строка содежит числа через пробел</param>
	Vector(std::string s);

	Vector(size_t size, double* coordinates);
	Vector(Vector const& v);
	size_t getSize() const;

	Vector& operator =(Vector const& p);
	double& operator [](const size_t);
	const double operator[](const size_t)const;
	/// <summary>
	/// явное преобразоваие
	/// </summary>
	explicit operator std::vector<double>() const;
	/// <summary>
	/// неявное преобразоваие к строке
	/// </summary>
	/// <remark>координаты записаны через пробел</remark>
	operator std::string() const;
	/// <summary>
	/// переопределение вектора новым размером и новыми значениями
	/// </summary>
	/// <param name="n">новый размер ветора</param>
	/// <param name="b">массив новых значений вектора</param>
	void operator ()(size_t n, double* b);
};

