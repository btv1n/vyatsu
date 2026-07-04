#include <vector>
#include <math.h>
#include <iostream>
#include <chrono>
#include <omp.h> // библиотека для параллельных вычислений
#include <functional> // для работы с функциями std::function<void()>



std::vector<double> measureTime(int n, std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure(const int n, const double t, std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure4(std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure5(std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure10(std::function<void()> target, std::function<void()> before = []() {});


// Измеряет время выполнения функции несколько раз
std::vector<double> measureTime(int n, std::function<void()> target, std::function<void()> before)
{
    // Массив для хранения всех замеров
    auto measures = std::vector<double>(n);

    // Переменные времени начала и конца одного измерения
    std::chrono::time_point<std::chrono::high_resolution_clock> start, end;

    for (int i = 0; i < n; i++)
    {
        // Подготавливаем состояние перед замером
        before();

        // Фиксируем начало и конец выполнения
        start = std::chrono::high_resolution_clock::now();
        target();
        end = std::chrono::high_resolution_clock::now();

        // Переводим длительность в секунды
        std::chrono::duration<double> time_span = end - start;
        measures[i] = time_span.count();

        // Печатаем прогресс
        std::cout << "Measurement #" << i + 1 << "/" << n << " completed. Time: " << measures[i] << std::endl;
    }

    return measures;
}

// Считает среднее время и погрешность
std::pair<double, double> makeMeasure(const int n, const double t, std::function<void()> target, std::function<void()> before)
{
    // Получаем набор времени запусков для n
    auto measures = measureTime(n, target, before);

    // mean - среднее, sig - дисперсия, eps - итоговая погрешность
    double mean = 0, sig = 0, eps;

    // Среднее время выполнения
    for (int i = 0; i < n; i++)
        mean += measures[i];
    mean /= n;

    // Дисперсия относительно среднего
    for (int i = 0; i < n; i++)
        sig += std::pow(measures[i] - mean, 2);
    sig /= n;

    // Оценка погрешности - коэффициент Стьюдента
    eps = std::sqrt(sig) * t;

    // Среднее время, погрешность
    return { mean, eps };
}


std::pair<double, double> makeMeasure4(std::function<void()> target, std::function<void()> before)
{
    const int n = 4;
    const double t = 3.18;
    return makeMeasure(n, t, target, before);
}

std::pair<double, double> makeMeasure5(std::function<void()> target, std::function<void()> before)
{
    // Количество измерений
    const int n = 5;

    // Коэффицент Стьюдента
    const double t = 2.77;

    return makeMeasure(n, t, target, before);
}

std::pair<double, double> makeMeasure10(std::function<void()> target, std::function<void()> before)
{
    const int n = 10;
    const double t = 2.26;
    return makeMeasure(n, t, target, before);
}

// Выводит результат
void printResult(std::pair<double, double> result)
{
    // eps_pow задает количество знаков для округления
    int eps_pow = 0;

    // Подбираем eps_pow в диапазоне [2; 20]
    while (result.second * std::pow(10, eps_pow) > 20)
        eps_pow--;

    while (result.second * std::pow(10, eps_pow) < 2)
        eps_pow++;

    // Значения без округления
    std::cout << "Average: " << result.first << std::endl;
    std::cout << "Eps:     " << result.second << std::endl;

    // mean округляем по стандартному правилу, eps округляем вверх
    const double mean = std::round(result.first * std::pow(10, eps_pow)) / std::pow(10, eps_pow);
    const double eps = std::ceil(result.second * std::pow(10, eps_pow)) / std::pow(10, eps_pow);

    std::cout << mean << " +/- " << eps << std::endl;
}