from fractions import Fraction  # Для работы с рациональными числами.
from math import isqrt  # Для быстрого вычисления целой части квадратного корня
import time

# math.sqrt(20)   # 4.47213595499958
# math.isqrt(20)  # 4

# Коэффициент сохранят 
def remove_zeroes(poly):
    """
    Удаляет ведущие нулевые коэффициенты многочлена
    Коэффициенты хранятся от старшей степени к младшей
    """
    while len(poly) > 1 and poly[0] == 0:
        poly.pop(0)  # Удаляем первый коэффициент
    return poly


def is_zero(poly):
    """Проверяет, является ли многочлен нулевым"""
    remove_zeroes(poly)
    return len(poly) == 1 and poly[0] == 0


def evaluate(poly, x):
    """
    Вычисляет значение многочлена в заданной точке.
    Используется схема Горнера: она последовательно обрабатывает коэффициенты
    от старшей степени к младшей и не требует отдельно считать степени x.
    """

    result = Fraction(0)

    for coefficient in poly:  # Последовательно перебираем коэффициенты от старшей степени к младшей
        result = result * x + coefficient  # Обновляем по схеме Горнера

    return result  # Вычисленное значение многочлена.


def number_to_text(x):
    """
    Преобразует рациональное число в строку для вывода
    Целые числа печатаются без знаменателя, а дробные - числитель/знаменатель
    """
    if x.denominator == 1:  # Является ли дробь целым числом.
        return str(x.numerator)  # Возвращаем только числитель, если знаменатель равен единице

    return str(x.numerator) + "/" + str(x.denominator)  # Возвращаем дробь в виде строки


def print_polynomial(poly):
    """
    Печатает многочлен
    Например, список [1, -6, 11, -6] будет напечатан как x^3 - 6*x^2 + 11*x - 6
    """
    remove_zeroes(poly)

    degree = len(poly) - 1  # Вычисляем степень многочлена по количеству коэффициентов
    text = ""

    for i in range(len(poly)):  # Перебираем индексы всех коэффициентов многочлена
        coefficient = poly[i]
        current_degree = degree - i  # Вычисляем степень, соответствующую текущему коэффициенту

        if coefficient == 0:
            continue

        if text != "":
            if coefficient > 0:
                text += " + "
            else:
                text += " - "
                coefficient = -coefficient
        else:
            if coefficient < 0:
                text += "-"
                coefficient = -coefficient # Делаем коэффициент положительным
        
        if current_degree == 0:
            text += number_to_text(coefficient) # Добавляем в строку только коэффициент без переменной x

        elif current_degree == 1:
            if coefficient == 1:
                text += "x"
            else:
                text += number_to_text(coefficient) + "*x"

        else:
            if coefficient == 1:
                text += "x^" + str(current_degree)
            else: # Степени выше первой с коэффициентом, отличным от 1
                text += number_to_text(coefficient) + "*x^" + str(current_degree) # Добавляем коэффициент, x и степень

    if text == "":
        text = "0"

    print(text)


def divisors(n):
    """
    Возвращает все целые положительные и отрицательные делители числа
    Функция используется для перебора возможных рациональных корней
    """
    n = abs(int(n))  # Преобразуем число к целому и берём модуль, потому что делители ищутся для положительного значения

    if n == 0:
        return [0]

    result = set()

    for i in range(1, isqrt(n) + 1):  # Перебираем возможные делители от 1 до квадратного корня из n
        if n % i == 0:  # Проверяем, делится ли n на i без остатка
            result.add(i)  # Добавляем положительный делитель i
            result.add(-i)  # Добавляем отрицательный делитель -i
            result.add(n // i)  # Добавляем парный положительный делитель n / i
            result.add(-(n // i))  # Добавляем парный отрицательный делитель -(n / i)

    return sorted(result)


def divide_polynomials(a, b):
    """
    Делит многочлен a на многочлен b столбиком
    Возвращает пару: частное и остаток. Коэффициенты обоих многочленов
    рассматриваются как рациональные числа и идут от старшей степени к младшей
    """
    # Создаём копию
    a = [Fraction(x) for x in a]
    b = [Fraction(x) for x in b]

    # Удаляем ведущие нули
    remove_zeroes(a)
    remove_zeroes(b)

    if is_zero(b):
        raise ValueError("Деление на нулевой многочлен")

    quotient = []  # Создаём список для коэффициентов частного

    while len(a) >= len(b) and not is_zero(a): # Продолжаем деление, пока степень делимого не меньше степени делителя
        coefficient = a[0] / b[0] # Находим старший коэффициент частного
        degree_diff = len(a) - len(b) # Находим разницу степеней делимого и делителя
        quotient.append(coefficient) # Добавляем найденный коэффициент в частное

        subtractor = [coefficient * x for x in b] # Умножаем делитель на найденный коэффициент

        # Коэффициенты идут от старшей степени к младшей.
        # Поэтому нули для сдвига по степеням добавляются в конец списка.
        subtractor += [Fraction(0)] * degree_diff  # Дописываем нули, чтобы степени совпали

        for i in range(len(subtractor)):  # Перебираем все коэффициенты
            a[i] -= subtractor[i]  # Вычитаем соответствующий коэффициент из текущего делимого

        remove_zeroes(a)  # Удаляем ведущие нули, появившиеся после вычитания

    if len(quotient) == 0:
        quotient = [Fraction(0)]

    if len(a) == 0:  # Не стал ли остаток пустым списком
        a = [Fraction(0)]  # Заменяем пустой остаток на нулевой многочлен

    return quotient, a  # Возвращаем частное и остаток от деления


def rational_roots(poly):
    """
    Ищет рациональные корни многочлена среди делителей свободного члена.
    Каждый вариант подставляется в многочлен, и корнем считается только тот кандидат, при котором значение равно нулю.
    """
    constant = poly[-1] # Берём свободный член многочлена

    if constant == 0: # Проверяем, равен ли константа нулю
        return [Fraction(0)] # Если свободный член равен нулю, то x = 0 является корнем

    roots = [] # Рациональные корни

    for d in divisors(constant):  # Перебираем все целые делители
        x = Fraction(d)

        if evaluate(poly, x) == 0:  # Подставляем в многочлен и проверяем, равен ли результат нулю
            roots.append(x)  # Добавляем в список корней, если он действительно является корнем

    return roots


def kronecker_factorization(poly):
    """
    Выполняет упрощённое разложение многочлена на множители.
    Функция ищет рациональные корни. Для каждого найденного корня r выделяется
    линейный множитель x - r, после чего многочлен делится на этот множитель.
    """
    remove_zeroes(poly) 

    if len(poly) <= 2:  # Проверяем, является ли многочлен константой или линейным многочленом
        return [poly]  # Такие многочлены считаются неразложенными множителями

    roots = rational_roots(poly)  # Ищем рациональные корни исходного многочлена

    if len(roots) == 0:
        return [poly]  # Если корней нет, возвращаем исходный многочлен как один множитель

    factors = []
    current = poly[:]

    while True:
        roots = rational_roots(current)

        if len(roots) == 0: 
            break

        root = roots[0]  # Берём первый найденный рациональный корень.
        factor = [Fraction(1), -root]  # Создаём множитель x - root

        quotient, remainder = divide_polynomials(current, factor)  # Делим текущий многочлен на найденный линейный множитель

        if not is_zero(remainder):
            break  # Если остаток ненулевой, разложение дальше не продолжаем

        factors.append(factor)
        current = quotient # Продолжаем разложение уже для полученного частного

    if len(current) > 1: # Проверяем, остался ли после делений ненулевой многочлен
        factors.append(current)  # Добавляем оставшуюся часть как последний множитель

    return factors


def read_polynomial():
    """
    Считывает коэффициенты многочлена из пользовательского ввода.
    Пользователь вводит коэффициенты через пробел от старшей степени к младшей.
    """
    print("Введите коэффициенты:")
    print("Пример: 1 -6 11 -6. Результат: x^3 - 6*x^2 + 11*x - 6")

    parts = input("-> ").split()

    return [Fraction(x) for x in parts]



poly = read_polynomial()

start = time.perf_counter()
factors = kronecker_factorization(poly)
finish = time.perf_counter()

print("\nИсходный многочлен:")
print_polynomial(poly)

print("\nРазложение:")
for i, factor in enumerate(factors, start=1):
    print("Множитель", i, ":")
    print_polynomial(factor)

print("\nВремя выполнения:")
print(finish - start, "секунд")