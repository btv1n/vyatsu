from sieve_of_eratosthenes import generate  # решето Эратосфена
from prime_tests import all_divisors, wilson, LTT  # тесты простоты
from math import sqrt

n = 100  # верхняя граница
primes = generate(n)  # простые числа, найденные решетом
primes_all_divisors = []
primes_wilson = []
primes_LTT = []

# Функция проверки числа на простоту
# def all_divisors(n: int) -> bool:  
#     if n < 2:  # Числа меньше 2 не являются простыми
#         return False

#     for d in range(2, int(sqrt(n)) + 1):  # Перебираем делители от 2 до корня из n
#         if n % d == 0:
#             return False

#     return True  # Если делителей нет, число простое


for i in range(1, n + 1):
    if all_divisors(i):  # проверяем число пробными делениями
        primes_all_divisors.append(i)  # добавляем простое число
    if wilson(i):  # проверяем число критерием Вильсона
        primes_wilson.append(i) 
    if LTT(i):  # проверяем простоту числа Мерсенна 2^i - 1
        primes_LTT.append(i)  # добавляем подходящий показатель i
        
# 1. Реализовать решето Эратосфена
print("Решето Эратосфена\n", primes) # вывод результата решета
print()

# 2. Реализовать метод пробных делений и тест на основе критерия Вильсона.
print("Все делители\n", primes_all_divisors)  # вывод метода пробных делений
print()
print("Критерий Вильсона\n", primes_wilson)  # вывод критерия Вильсона
print()

# 5. Тест Лукаса-Лемера
print("Тест Лукаса-Лемера\n", primes_LTT)  # вывод показателей чисел Мерсенна
# p = 13, M_p = 2^13 - 1 = 8191
# перебираем найденные показатели p
for p in primes_LTT:
    print(f"p = {p}, M_p = 2^{p} - 1 = {2 ** p - 1}")  # выводим число Мерсенна
    # print(f"Простое: {'да' if all_divisors(p) else 'нет'}")