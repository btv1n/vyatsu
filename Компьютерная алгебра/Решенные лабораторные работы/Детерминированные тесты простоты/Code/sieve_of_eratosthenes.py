# Решето Эратосфена
def generate(n: int) -> list[int]:  
    assert n > 2 
    sieve = [False, False] + [True] * (n - 1)  # создаем список проверки простоты [ false, false, true ... ]

    result = []  # список найденных простых чисел

    i = 2  # первое простое число
    while i <= n: 
        for j in range(i * 2, n + 1, i):  # перебираем кратные числа
            sieve[j] = False  # помечаем кратное как составное
        result.append(i)
        i += 1  # переходим к следующему числу
        while i <= n and not sieve[i]:  # пропускаем составные числа
            i += 1  # следующее простое число
    return result  # возвращаем список простых чисел [2, n]


if __name__ == "__main__": 
    print(generate(97))  # пример работы