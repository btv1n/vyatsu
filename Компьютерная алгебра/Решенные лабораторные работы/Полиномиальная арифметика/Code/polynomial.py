# Полиномиальная арифметика

# Класс для работы с многочленами

# 2) описание класса работы с многочленами от одной переменной, компьютерная реализация основных алгоритмов полиномиальной арифметики; 
class Polynomial: 
    def __init__(self, coefficients): 
        self.coefficients = coefficients  # Список коэффициентов многочлена
        self.remove_zeros()  # Удаляем лишние нули в конце списка коэффициентов


    # Метод для удаления лишних нулей в конце многочлена
    def remove_zeros(self):  
        while len(self.coefficients) > 1 and self.coefficients[-1] == 0: 
            self.coefficients.pop()  # Удаляем последний


    # Метод для преобразования многочлена в строку
    def __str__(self): 
        result = ""

        for i in range(len(self.coefficients)):
            coefficient = self.coefficients[i]

            if coefficient == 0:  # Если коэффициент равен нулю
                continue  # Пропускаем

            if i == 0:  # Если степень x равна 0
                part = str(coefficient)  # Записываем только коэффициент
            elif i == 1:  # Если степень x равна 1
                part = str(coefficient) + "x"
            else:  # Если степень x больше 1
                part = str(coefficient) + "x^" + str(i)

            if result == "":
                result = part  # Записываем первую часть многочлена без плюса
            else:  # Если результате уже что-то есть
                result = result + " + " + part  # Добавляем плюс и записываем следующее слагаемое

        if result == "":  # Если все коэффициенты были равны нулю
            result = "0"  # многочлен равен нулю

        return result


    # Метод для сложения двух многочленов
    def add(self, other):  
        max_len = max(len(self.coefficients), len(other.coefficients))  # Находим максимальную длину списков коэффициентов

        result = []

        for i in range(max_len):  # Перебираем все степени
            if i < len(self.coefficients):  # Если у первого многочлена есть коэффициент при этой степени
                a = self.coefficients[i]  # Берем этот коэффициент
            else:  # Если коэффициента нет
                a = 0  # Считаем его равным нулю

            if i < len(other.coefficients):  # Если у второго многочлена есть коэффициент при этой степени
                b = other.coefficients[i]  # Берем этот коэффициент
            else:  # Если коэффициента нет
                b = 0  # Считаем его равным нулю

            result.append(a + b)  # Складываем коэффициенты одинаковых степеней

        return Polynomial(result)


    # Метод для вычитания двух многочленов
    def subtract(self, other):  
        max_len = max(len(self.coefficients), len(other.coefficients))  # Находим максимальную длину списков коэффициентов

        result = []

        for i in range(max_len):  # Перебираем все степени от 0 до максимальной
            if i < len(self.coefficients):  # Если у первого многочлена есть коэффициент при этой степени
                a = self.coefficients[i]  # Берем этот коэффициент
            else:  # Если коэффициента нет
                a = 0  # Считаем его равным нулю

            if i < len(other.coefficients):  # Если у второго многочлена есть коэффициент при этой степени
                b = other.coefficients[i]  # Берем этот коэффициент
            else:  # Если коэффициента нет
                b = 0  # Считаем его равным нулю

            result.append(a - b)  # Вычитаем коэффициенты одинаковых степеней

        return Polynomial(result)


    # Метод для умножения двух многочленов
    def multiply(self, other):  
        result_len = len(self.coefficients) + len(other.coefficients) - 1  # Вычисляем длину списка коэффициентов результата
        result = [0] * result_len

        for i in range(len(self.coefficients)):  # Перебираем коэффициенты первого многочлена
            for j in range(len(other.coefficients)):  # Перебираем коэффициенты второго многочлена
                result[i + j] = result[i + j] + self.coefficients[i] * other.coefficients[j]  # Добавляем произведение к нужной степени

        return Polynomial(result)


    # Метод для простого вычисления значения многочлена
    def value_simple(self, x):  
        result = 0

        for i in range(len(self.coefficients)):  # Перебираем все коэффициенты многочлена
            result = result + self.coefficients[i] * power_binary(x, i)

        return result

    # 4) реализация алгоритма нахождения значения многочленов (схема Горнера). 
    # Метод вычисления значения многочлена по схеме Горнера
    '''
		Начинает со старшего коэффициента.
		Постепенно идет к младшему коэффициенту.
		На каждом шаге умножает текущий результат на x.
		Затем добавляет следующий коэффициент.
		Возвращает итоговое значение.
		'''
    def value_horner(self, x): 
        result = 0  # Начальное значение результата равно нулю

        for i in range(len(self.coefficients) - 1, -1, -1):  # Идем от старшего коэффициента к младшему
            result = result * x + self.coefficients[i]  # Выполняем шаг схемы Горнера

        return result  # Возвращаем найденное значение многочлена


# 3) решение задачи эффективного вычисления xn (бинарный метод, метод множителей, «степенное дерево») 
# Функция простого возведения числа x в степень n
def power_simple(x, n):  
    result = 1 

    for i in range(n):  # Повторяем умножение n раз
        result = result * x 

    return result


# Функция бинарного возведения в степень
def power_binary(x, n):
    result = 1 
    base = x  # Текущее основание степени
    degree = n  # Текущую степень

    while degree > 0:  # Пока степень больше нуля
        if degree % 2 == 1:  # Если степень нечетная
            result = result * base  # Умножаем результат на текущее основание

        base = base * base
        degree = degree // 2

    return result # х в степени n


# Функция возведения в степень методом множителей
def power_factors(x, n):  
    if n == 0:  # Если степень равна нулю
        return 1

    if n == 1:
        return x  # Число в первой степени равно самому себе

    for d in range(2, n):  # Перебираем возможные делители степени n
        if n % d == 0:  # Если d является делителем n
            k = n // d  # Находим второй множитель степени

            small_power = power_factors(x, d)  # Вычисляем x в степени d
            return power_factors(small_power, k)  # Возводим полученный результат в степень k

    return x * power_factors(x, n - 1)  # Если n простое, вычисляем x * x^(n-1)


# Функция возведения в степень с использованием степенного дерева
def power_tree(x, n):  
    if n == 0:
        return 1

    powers = [1]  # Список степеней

    powers.append(x)  # Добавляем x^1

    current = 2  # Начинаем вычислять степени с x^2

    while current <= n:  # Пока не достигли нужной степени n
        a = current // 2  # Берем первую часть степени
        b = current - a  # Берем вторую часть степени

        value = powers[a] * powers[b]  #  Преобразуем x^current в x^a * x^b
        powers.append(value)  # Добавляем найденную степень

        current = current + 1  # Переходим к следующей степени

    return powers[n]  # x в степени n


# 2 + 3x + 4x^2
p1 = Polynomial([2, 3, 4])

# 1 + 5x
p2 = Polynomial([1, 5]) 


print("Первый многочлен:")
print("p1 =", p1)
print()


print("Второй многочлен:")
print("p2 =", p2)
print()

print("Сложение многочленов:")
print("p1 + p2 =", p1.add(p2))
print()

print("Вычитание многочленов:")
print("p1 - p2 =", p1.subtract(p2))
print()

print("Умножение многочленов:")
print("p1 * p2 =", p1.multiply(p2))
print()


# Задаем значения x и n (степень)
x = 2
n = 10
# x = int(input("Введите x: "))
# n = int(input("Введите n: "))


print("Вычисление значения многочлена p1 при x =", x)
print("Простой способ:", p1.value_simple(x))
print("Схема Горнера:", p1.value_horner(x))
print()


print("Вычисление x^n")
print("x =", x)
print("n =", n)
print()

print("Простой способ:", power_simple(x, n))
print("Бинарный метод:", power_binary(x, n))
print("Метод множителей:", power_factors(x, n))
print("Степенное дерево:", power_tree(x, n))







'''
1) рассмотрение способов представления многочленов в памяти компьютера;

Способ 1: с помощью массива
Принцип: индекс элемента в массиве - это его степень, а число - это коэффициент
Пример: poly = [5, 0, 3] -> 3x^2 + 0x^1 + 5*x^1

Способ 2: с помощью словаря
Принцип: Ключ словаря - это степень, а значение по ключу - это коэффициент
Пример: poly = {0: 5, 100: 3} -> 3x^100 + 5x^0


В коде использован способ 1: p1 = Polynomial([2, 3, 4])
'''