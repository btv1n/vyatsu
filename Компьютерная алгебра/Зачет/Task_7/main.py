# Данные задачи
n = 35
e = 5
cipher = [8, 15, 13, 10]

# Разложение n и функция Эйлера
# p = 5
# q = 7
# phi = (p - 1) * (q - 1)

# Функция для разложения числа на простые множители
def factorize(num):
    # Перебираем возможные делители от 2 до sqrt(n)
    for i in range(2, int(num**0.5) + 1):
        if num % i == 0:
            p = i
            q = num // i
            return p, q
    return None, None  # если не найдены
p, q = factorize(n)
print(f"Разложение: {n} = {p} * {q}")

phi = (p - 1) * (q - 1)


# Поиск d: e*d = 1 (mod phi)
for d in range(1, phi):
    if (e * d) % phi == 1:
        break

print("phi(n) =", phi)
print("Закрытый ключ (n, d) =", (n, d))

# Расшифрование
text_numbers = [] # список для расшифрованных чисел
for char in cipher:
    m = pow(char, d, n) # char^d mod n
    text_numbers.append(m) # добавляем в список

# Алфавит A=1 ... Z=26
alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
text = "".join(alphabet[m - 1] for m in text_numbers) # -1 поскольку индекс с 0 в алфавите

print("Расшифрованное сообщение:", text)