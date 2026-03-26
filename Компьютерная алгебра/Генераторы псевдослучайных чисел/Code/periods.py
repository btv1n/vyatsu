import time
import math

### Линейный конгруэнтный метод
class LinearRandom:
    def __init__(self, seed: int, k: int = 13):
        # Mодуль n = 2^32-1, используется для арифметики по модулю 32 битная арифметика
        self.n = 31 #---------- маленький модуль
        
				# x0 - начальное число. Проверяем корректность seed: 0 <= seed <= n - 1
        assert 0 <= seed and seed < self.n # assert - встроенная проверка, если false, то программа завершается
        
        # Проверка взаимной простоты k и n
        assert math.gcd(k, self.n) == 1
        
				# Хранит предыдущее сгенерированное число
        self.prev = seed
        
				# Первообразный корень главного модуля (рекомендации: k = 5, k = 13)
        self.a = 3 #----------
        
        # Для эффективного использования компьютера 
        self.b = 0 

    def next(self):
        # x[n+1] = (a * x[n] + b) mod 2^31-1 (m)
        self.prev = (self.a * self.prev + self.b) % self.n
        return self.prev


### Метод Фибоначчи с запаздыванием
# X[k] - вещественные числа из диапазона [0,1)
class FibRandom:
    def __init__(self, seed: int, a: int = 4, b: int = 1):
        # a, b - индексы запаздывания. Они определяют какие элементы последовательности участвуют в вычислении следующего числа
        assert a != b # иначе генератор не имеет смысла
        self.a, self.b = a, b
        
				# Для запуска алгоритма требуется max{a,b} случайных чисел
        # self.buffer = [0] * (max(a, b) + 1)

        
				# Инициализирующий генератор для заполнения буфера
        # lr = LinearRandom(seed)

        # Заполнение буфера начальными псевдослучайными числами. Деление на lr.n приводит значения к диапазону [0, 1)
        # for i in range(max(a, b) + 1):
        #     self.buffer[i] = lr.next() / lr.n
        
				# Заполнения буфера как в задании
        # self.buffer = [0.1, 0.7, 0.3, 0.9, 0.5] #####
        
        
        self.buffer = [0.1, 0.7, 0.3, 0.9, 0.5] #----------
        
				# Вывод стартового буфера
        print("Стартовый буфер: ", self.buffer)
        print()

    def next(self):
        # Формула: X[k] = X[k-a] - X[k-b]. self.a, self.b - элементы с лагом a и b
        n = self.buffer[self.a] - self.buffer[self.b]
        # n = self.buffer[self.b] - self.buffer[self.a] #####
        # n = round(n, 1) #####

				# Если получилось отрицательное число, прибавляем 1, чтобы попасть в диапазон [0, 1)
        if n < 0:
            n += 1
        
				# Добавление нового значения в конец буфера
        self.buffer.append(n)
        
				# Удаление самого старого элемента
        self.buffer.pop(0)
        
        return n


### Алгоритм Блюма-Блюма-Шуба
class BBSRandom:
    def __init__(self, seed: int, p: int = 2047, q: int = 8191):
        # p и q - простые числа, оба сравнимы с 3 по модулю 4
        assert p % 4 == 3 and q % 4 == 3, "p и q должны быть сравнимы с 3 по модулю 4"

        # Вычисляем целое число Блюма
        self.M = p * q
        self.p = p
        self.q = q
        
        # Выбираем другое случайное целое число x, взаимно просто с M
        assert math.gcd(seed, self.M) == 1, "seed должен быть взаимно прост с M"
        
        # Вычисляем стартовое число генератора: x[0] = x^2 mod M * x[0]
        self.x = (seed * seed) % self.M
        print(f"BBS инициализирован: p={p}, q={q}, M={self.M}, x0={self.x}")
        print()
        
        # Сохраняем начальное значение для возможности сброса
        self.x0 = self.x

    def next(self):
        # На каждом n-м шаге работы генератора вычисляется x[n+1] = x[n]^2 mod M.
        self.x = (self.x * self.x) % self.M
        
        # Результатом n-го шага является один бит числа x[n+1]
        # Обычно берут младший бит (наименее значимый)
        bit = self.x & 1 # & 1 - это побитовая операция, которая берет последний бит

        return bit
        
    def get_state(self):
        """Возвращает текущее состояние"""
        return self.x

    def reset(self):
        """Сбрасывает генератор в начальное состояние"""
        self.x = self.x0

#################################################################################################################

def find_period(generator, get_value_func, limit=10_000_000):
    # Множество для хранения уже сгенерированных значений - для быстрой проверки, встречалось ли число ранее
    seen = set() 
    # Список для хранения последовательности сгенерированных чисел, нужен для подсчета длины последовательности до первого повторения
    seq = []

    # Цикл генерации чисел
    # param limit - ограничивает максимальное количество шагов
    for i in range(limit):
        # param get_value_func - функция, которая извлекает значение из конкретного генератора
        value = get_value_func(generator)

        # Проверяем встречалось ли число ранее
        if value in seen:
            print("Повтор найден:", value)
            # Длина списка seq - это количество уникальных элементов до первого повторения - т.е. период
            return len(seq)

        # Добавление числа в множество и список
        seen.add(value)
        seq.append(value)

    # Если период слишком большой
    return None  

#################################################################################################################



print("=" * 80)
print()

### Линейный конгруэнтный метод
# Некоторые числа содержат < 10 знаков, поскольку начинаются с 0
print("Линейный конгруэнтный метод")
print()

start_time = time.time() # начало замера времени

linear = LinearRandom(seed=2, k=13)
for i in range(10):
    print(f"x{i} = {linear.next()}")

end_time = time.time() # конец замера времени

print()
print(f"Время выполнения когруэнтного метода: {end_time - start_time:.6f} секунд")

print()
print("=" * 80)
print()








### Метод Фибоначчи с запаздыванием
# В методичке k13 = 0
print("Метод Фибоначчи с запаздыванием")
print()

start_time = time.time() # начало замера времени

# fibonacci = FibRandom(2)
# fibonacci = FibRandom(seed = 2, a = 4, b = 1)
# for i in range(10):
#     print(f"x{i} = {fibonacci.next()}")

end_time = time.time() # конец замера времени

print()
print(f"Время выполнения метода фибоначчи: {end_time - start_time:.6f} секунд")

print()
print("=" * 80)
print()








### Алгоритм Блюма-Блюма-Шуба
print("Алгоритм Блюма-Блюма-Шуба")
print()

start_time = time.time() # начало замера времени

bss = BBSRandom(seed=2701, p = 2047, q = 8191)
# bss = BBSRandom(seed = 3, p = 11, q = 19) # Время выполнения метода фибоначчи: 0.003150 секунд
print(f"x0 = {bss.get_state():8d} | бит = —")
for i in range(1, 11):
    bit = bss.next()
    print(f"x{i} = {bss.get_state():8d} | бит = {bit}")

end_time = time.time() # конец замера времени

print()
print(f"Время выполнения метода Блюма-Блюма-Шуба: {end_time - start_time:.6f} секунд")

print()
print("=" * 80)





#################################################################################################################


print("=" * 80)
print("=" * 80)
print("=" * 80)


linear = LinearRandom(seed=1)
period = find_period(linear, lambda g: g.next())
print("Период линейного конгруэнтного метода:", period)
print("-" * 80)

fibonacci = FibRandom(seed=2, a=4, b=1)
period = find_period(fibonacci, lambda g: g.next())
# period = find_period(fibonacci, lambda g: round(g.next(), 10))
print("Период метода Фибоначчи:", period)
print("-" * 80)

bss = BBSRandom(seed=2701, p=2047, q=8191)
period = find_period(bss, lambda g: (g.next(), g.get_state()))
# period = find_period(bss, lambda g: (g.next(), g.get_state())[1])
print("Период BBS:", period)
print("-" * 80)