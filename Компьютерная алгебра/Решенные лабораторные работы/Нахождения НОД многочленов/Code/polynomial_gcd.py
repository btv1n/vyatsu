from fractions import Fraction  # Дроби
import sys
import time


# Yдаляет лишние нули в начале списка
def remove_zeroes(poly):
    while len(poly) > 1 and poly[0] == 0:  # Пока первый коэффициент равен нулю
        poly.pop(0)  # Удаляем первый ноль
    return poly


# Проверяет, равен ли многочлен нулю.
def is_zero(poly):  
    remove_zeroes(poly)
    if len(poly) == 1 and poly[0] == 0:  # Если остался только один ноль
        return True  # Это нулевой многочлен
    return False  # Иначе многочлен не нулевой


# Функция находит остаток от деления a на b
def divide_polynomials(a, b):
    a = a[:]  # Копию первого многочлена
    b = b[:]

    remove_zeroes(a)
    remove_zeroes(b)
    
    # Проверяем, не делим ли на нулевой многочлен
    if is_zero(b): 
        print("На нулевой многочлен делить нельзя")
        return [Fraction(0)]  # Нулевой остаток

    # Пока степень a не меньше степени b
    while len(a) >= len(b) and not is_zero(a):  
        coefficient = a[0] / b[0]  # Коэффициент для вычитания.

        for i in range(len(b)):
            a[i] = a[i] - coefficient * b[i]  # Вычитаем часть многочлена b из a

        remove_zeroes(a)  # Убираем нули, которые появились после вычитания

    return a # Остаток от деления


# Делает старший коэффициент равным 1. Монический вид -> [2,4,6] -> [1,2,3]
def make_monic(poly):
    poly = poly[:]
    remove_zeroes(poly)

    if is_zero(poly):  # Проверяем, не равен ли многочлен нулю.
        return [Fraction(0)]

    first = poly[0]  # Берем первый коэффициент

    for i in range(len(poly)):
        poly[i] = poly[i] / first  # Делим каждый коэффициент на первый

    return poly  # Возвращаем нормальный вид многочлена


# Находит НОД двух многочленов
def gcd_polynomials(a, b):
    # Делает копии многочленов
    a = a[:]
    b = b[:]

    remove_zeroes(a)
    remove_zeroes(b)
    
    # Пока второй многочлен не стал нулем
    while not is_zero(b):  
        r = divide_polynomials(a, b)  # Находим остаток от деления
        a = b  # Второй многочлен становится первым
        b = r  # Остаток становится вторым многочленом

    return make_monic(a)  # Возвращаем НОД со старшим коэффициентом 1


# Переводит число в текст
def number_to_text(x):  
    if x.denominator == 1:  # Если у дроби знаменатель равен 1
        return str(x.numerator)  # Пишем только числитель
    return str(x.numerator) + "/" + str(x.denominator)  # Иначе пишем дробь


# Печатает многочлен
def print_polynomial(poly):
    remove_zeroes(poly)
    degree = len(poly) - 1  # Находим степень многочлена
    text = ""

    for i in range(len(poly)):
        coefficient = poly[i]  # Берем коэффициент
        current_degree = degree - i  # Считаем степень

        if coefficient == 0:
            continue

        # Если в строке уже есть часть многочлена
        if text != "":  
            if coefficient > 0: # Если коэффициент положительный
                text = text + " + "
            else:  # Если коэффициент отрицательный
                text = text + " - "
                coefficient = -coefficient  # Делаем коэффициент положительным
        else:  # Если строка пока пустая.
            if coefficient < 0:  # Если первый коэффициент отрицательный
                text = "-"  # Добавляем минус в начало
                coefficient = -coefficient  # Делаем коэффициент положительным
                

        if current_degree == 0:  
            text = text + number_to_text(coefficient)  # Добавляем только число
        elif current_degree == 1:
            if coefficient == 1:
                text = text + "x"  # Пишем только x
            else:  # Если коэффициент не равен 1
                text = text + number_to_text(coefficient) + "*x"  # Пишем число и x
        else:  # Если степень больше 1
            if coefficient == 1:  # Если коэффициент равен 1
                text = text + "x^" + str(current_degree)  # Пишем x в степени
            else:  # Если коэффициент не равен 1
                text = text + number_to_text(coefficient) + "*x^" + str(current_degree)  # Пишем x в степени с коэффициентом

    if text == "":  # Если строка так и осталась пустой
        text = "0"  # Значит многочлен равен нулю

    print(text)

# Переводит строку в дробь: "3", "-1/2", "0.5"
def parse_fraction(text):
    text = text.strip()  # Убираем пробелы в начале и в конце строки
    if not text:
        raise ValueError("пустой коэффициент")

    if "/" in text:  # Если в строке есть дробь
        parts = text.split("/")  # Делим строку на числитель и знаменатель
        if len(parts) != 2:  # Дробь должна содержать ровно одну "/"
            raise ValueError("неверный формат дроби: " + text)
        return Fraction(int(parts[0].strip()), int(parts[1].strip()))  # Создаем дробь

    if "." in text:  # Если в строке есть десятичная точка
        return Fraction(text)  # Преобразуем десятичное число в дробь

    return Fraction(int(text))  # Иначе это целое число


# Читает многочлен из консоли
def read_polynomial(name):
    print("Введите коэффициенты многочлена", name + ":")
    print("От старшей степени к младшей, через пробел.")
    print("Пример: 1 -3 2  ->  x^2 - 3x + 2")

    while True:
        line = input("-> ").strip()  # Убираем пробелы по краям
        if not line: # Ничего не введено
            print("Введите хотя бы один коэффициент.")
            continue

        # Анализ введенной строки
        try:
            parts = line.split()  # Разбивает строку на отдельные коэффициенты
            poly = [parse_fraction(part) for part in parts]  # Преобразуем каждый коэффициент в дробь
            remove_zeroes(poly)  # Удаляем лишние нули в начале списка
            if is_zero(poly):
                print("Многочлен не может быть нулевым. Повторите ввод.")
                continue
            return poly  # Возвращаем многочлен
        except ValueError as error:  # Если коэффициент записан неверно
            print("Ошибка:", error)
            print("Используйте целые числа, дроби (1/2) или десятичные (0.5).")

##############################################################
'''
# Например: x^3 - 2*x^2 - x + 2 записывается как [1, -2, -1, 2]
first_polynomial = [  # Создаем первый многочлен
    Fraction(1),  # Коэффициент при x^3
    Fraction(-2),  # Коэффициент при x^2
    Fraction(-1),  # Коэффициент при x
    Fraction(2),  # Свободный член
]  # Заканчиваем список первого многочлена

second_polynomial = [  # Создаем второй многочлен
    Fraction(1),  # Коэффициент при x^2
    Fraction(-3),  # Коэффициент при x
    Fraction(2),  # Свободный член
]  # Заканчиваем список второго многочлена
'''

##############################################################
# Коэффициенты записаны от старшей степени к младшей

# F(x) = x^2 - 3x + 2
# G(x) = x^2 - 4x + 3
# НОД(F, G) = x - 1

# F(x) = x^2 - 3x + 2
first_polynomial = [
    Fraction(1),
    Fraction(-3),
    Fraction(2)
]

# G(x) = x^2 - 4x + 3
second_polynomial = [
    Fraction(1),  # Коэффициент при x^2
    Fraction(-4),  # Коэффициент при x
    Fraction(3) # Свободный член
]

##############################################################

# F(x) = x^3 - x
# G(x) = x^4 - 1
# НОД(F, G) = x^2 - 1

# F(x) = x^3 - x
first_polynomial = [
    Fraction(8),
    Fraction(0),
    Fraction(-8),
    Fraction(0)
]

# G(x) = x^4 - 1
second_polynomial = [
    Fraction(16),
    Fraction(0),
    Fraction(0),
    Fraction(0),
    Fraction(-16)
]
##############################################################

# Ввод многочленов
# first_polynomial = read_polynomial("F(x)")
# second_polynomial = read_polynomial("G(x)")

start_time = time.perf_counter()
answer = gcd_polynomials(first_polynomial, second_polynomial)  # Находим НОД
finish_time = time.perf_counter()

print("Первый многочлен:")
print_polynomial(first_polynomial)

print("Второй многочлен:")
print_polynomial(second_polynomial)

print("НОД многочленов:")
print_polynomial(answer)

print("Время выполнения:")
print(finish_time - start_time, "секунд")