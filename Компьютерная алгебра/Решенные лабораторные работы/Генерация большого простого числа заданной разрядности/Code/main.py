from rand import BBSRandom  # Блюма-Блюма-Шуба
from time import time 
from math import sqrt, gcd # НОД
from random import randint

# from prime_tests.solovey import solovey_eps, solovey
# from prime_tests.miller import miller_eps, miller
from prime_tests.fermat import fermat_eps, fermat  # тест Ферма

# Функция проверки числа на простоту
def all_divisors(n: int) -> bool:  
    if n < 2:  # Числа меньше 2 не являются простыми
        return False

    for d in range(2, int(sqrt(n)) + 1):  # Перебираем делители от 2 до корня из n
        if n % d == 0:
            return False

    return True  # Если делителей нет, число простое


# Генерирует простое число заданной битовой длины
def generate(bbs: BBSRandom, length: int, prime_test):  
    t = [3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251]  # Малые простые числа для предварительной проверки.

    loop_counter = 0 
    start = time()  # Запоминаем время начала генерации

    # Пока не найдем простое число
    while True:  
        n = make_random_number(bbs, length)  # Генерируем случайное число нужной длины

        while n.bit_length() == length:  # Перебираем числа нужной длины
            loop_counter += 1 
            has_small_divisor = False  # Делится ли на простое число

            for d in t:  # Проверяем делимость на простые числа
                if n != d and n % d == 0:  # Если число составное
                    has_small_divisor = True  # Запоминаем
                    break  # Не проверяем делители дальше

            if has_small_divisor:  # Если найден делитель
                n += 2  # Берем следующее нечетное число
                continue  # Переходим к следующему

            # Проверяем число выбранным тестом простоты
            if prime_test(n):  
                print(f"Время выполнения: {time() - start} секунд\nКоличество итераций: {loop_counter}")
                return n

            n += 2  # Если тест не прошел, проверяем следующее нечетное число


# Создает случайное нечетное число заданной битовой длины
def make_random_number(bbs, length): 
    number = 1  # Устанавливаем младший бит равным 1
    for i in range(1, length - 1):  # Заполняем внутренние биты случайными числами
        bit = bbs.next()  # Получаем следующий случайный бит
        number |= bit << i  # Записываем бит на позицию i
    number |= 1 << (length - 1)  # Устанавливаем старший бит равным 1
    return number


# Создает новый генератор BBS
def make_bbs(): 
    seed = int(time() * 1000)  # Начальное значение из текущего времени
    modulus = 2047 * 8191  # Вычисляем модуль генератора

    # Проверка на взаимную простоту seed и modulus
    while gcd(seed, modulus) != 1: 
        seed += 1

    # Генератор
    return BBSRandom(seed)

# Список битовых длин для тестирования. Пример: 8 -> 11111111 -> 256
# 12*8
# lengths = [8, 12, 16, 18, 20]
lengths = []

def main():
    # Выбор случайных битовых длин для тестирования
    while len(lengths) < 5:
        number = randint(8,20)
        
        if number not in lengths:
            lengths.append(number)

    # Запускаем тест для каждой длины
    for length in lengths:
        print(f"\nТест для длины {length} бит")

        bbs = make_bbs()  # Создаем новый генератор случайных битов, для того чтобы задать новое значение seed

        # Генерируем простые числа
        r = generate(  
            bbs,
            length,  # Битовая длина
            lambda n: fermat(n, 5),  # Используем тест Ферма с 5 проверками
        )

        print(f"Число: {r}")
        print(f"Число в двоичном виде: {bin(r)[2:]}")
        print(f"Длина: {r.bit_length()} бит")
        print(f"Простое: {'да' if all_divisors(r) else 'нет'}")

if __name__ == "__main__":
    main()

















# Вывод:
# В ходе лабораторной работы была реализована программа генерации простого числа заданной битовой длины. 
# Для получения случайных бит используется генератор Блюма-Блюма-Шуба, а проверка простоты выполняется с помощью теста Ферма. 
# Программа выводит время генерации и количество итераций основного цикла.
# Тестирование на числах разной длины показало, что программа корректно формирует числа нужной разрядности и находит простые числа.