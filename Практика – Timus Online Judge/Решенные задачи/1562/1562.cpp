#define _USE_MATH_DEFINES // Для включения математических констант (M_PI - число ПИ)

#include <iostream>
#include <math.h> // <cmath>

using namespace std;

// Формулы для вычисления размера части ананаса
double pieceSize(double x1, double x2, double bh, double ah) 
{
    return M_PI * pow(ah, 2) * ((x2 - x1) + 1 / (3.0 * pow(bh, 2)) * (pow(x1, 3) - pow(x2, 3)));
}

int main() 
{
    double a, b;
    int n;

    // a - ширина ананаса (см)
    // b - высота ананаса (см)
    // n - кол-во кусков
    cin >> a >> b >> n;

    // Вычисляем шаг и начальное значение x
    double dx = b / n; 
    double x = -b / 2 + dx;

    // Выводит вес каждого куска в граммах с точностью до микрограмма в порядке их отрезания
    while (x <= b / 2 + 0.000001) // идем до середины ширины, поскольку дальше результаты симметричны
    { 
        //cout << pieceSize(x - dx, x, b / 2, a / 2) << endl;
        printf_s("%.6f\n", pieceSize(x - dx, x, b / 2, a / 2)); // printf - visual ; printf_s - site
        x += dx; 
    }

    return 0;
}
