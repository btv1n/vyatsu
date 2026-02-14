def decrypt(ciphertext, a, b):
    # index 0  -> space
    # index 26 -> z
    alphabet = " abcdefghijklmnopqrstuvwxyz"
    m = len(alphabet)  # 27
    
    result = []
    
    for char in ciphertext.lower():
        if char in alphabet:
            y = alphabet.index(char)      # получаем индекс символа
            x = (a * (y + m - b)) % m     # формула дешифрования
            result.append(alphabet[x])    # возвращаем символ по индексу
    
    return ''.join(result) # объединяет все буквы в одно слово


# Данные
encrypted_text = "tmuxa"
a = 8
b = 3

# Дешифровка
decrypted_text = decrypt(encrypted_text, a, b)

print("Зашифрованное слово:", encrypted_text)
print("Расшифрованное слово:", decrypted_text)

# Вывод числовых значений
print("\nПодробная информация:")
alphabet = " abcdefghijklmnopqrstuvwxyz"
m = len(alphabet)

for char in encrypted_text.lower():
    if char in alphabet:
        y = alphabet.index(char)
        x = (a * (y + m - b)) % m
        print(f"'{char}' = {y} -> '{alphabet[x]}' = {x}")
