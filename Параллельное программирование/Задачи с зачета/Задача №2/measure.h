#ifndef HELPERS_H
#define HELPERS_H

#include <vector>
#include <omp.h>
#include <math.h>
#include <iostream>
#include <functional>

std::vector<double> measureTime(int n, std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure(const int n, const double t, std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure4(std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure5(std::function<void()> target, std::function<void()> before = []() {});
std::pair<double, double> makeMeasure10(std::function<void()> target, std::function<void()> before = []() {});

std::vector<double> measureTime(int n, std::function<void()> target, std::function<void()> before)
{
    auto measures = std::vector<double>(n, -1);

    for (int i = 0; i < n; i++)
    {
        before();
        double start = omp_get_wtime();
        target();
        double end = omp_get_wtime();
        measures[i] = end - start;
        std::cout << "Measure #" << i + 1 << "/" << n << " completed. Time: " << measures[i] << std::endl;
    }

    return measures;
}

std::pair<double, double> makeMeasure(const int n, const double t, std::function<void()> target, std::function<void()> before)
{
    auto measures = measureTime(n, target, before);

    double mean = 0, sig = 0, eps;
    for (int i = 0; i < n; i++)
        mean += measures[i];
    mean /= n;

    for (int i = 0; i < n; i++)
        sig += std::pow(measures[i] - mean, 2);
    sig /= n;
    eps = std::sqrt(sig) * t;

    return { mean, eps };
}

/**
 * @brief P = 0.95, n = 4
 *
 * @return {mean, eps}
 */
std::pair<double, double> makeMeasure4(std::function<void()> target, std::function<void()> before)
{
    const int n = 4;
    const double t = 3.18;
    return makeMeasure(n, t, target, before);
}

/**
 * @brief P = 0.95, n = 5
 *
 * @return {mean, eps}
 */
std::pair<double, double> makeMeasure5(std::function<void()> target, std::function<void()> before)
{
    const int n = 5;
    const double t = 2.77;
    return makeMeasure(n, t, target, before);
}

/**
 * @brief P = 0.95, n = 10
 *
 * @return {mean, eps}
 */
std::pair<double, double> makeMeasure10(std::function<void()> target, std::function<void()> before)
{
    const int n = 10;
    const double t = 2.26;
    return makeMeasure(n, t, target, before);
}

/**
 * @param result {mean, eps}
 */
void printResult(std::pair<double, double> result)
{
    int eps_pow = 0;
    while (result.second * std::pow(10, eps_pow) > 20)
        eps_pow--;

    while (result.second * std::pow(10, eps_pow) < 2)
        eps_pow++;

    std::cout << "mean: " << result.first << std::endl;
    std::cout << "eps:  " << result.second << std::endl;

    const double mean = std::round(result.first * std::pow(10, eps_pow)) / std::pow(10, eps_pow);
    const double eps = std::ceil(result.second * std::pow(10, eps_pow)) / std::pow(10, eps_pow);
    std::cout << mean << " ± " << eps << std::endl;
}

#endif