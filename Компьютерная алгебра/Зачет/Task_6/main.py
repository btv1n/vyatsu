alphabet = ['A','B','C','D','E','F','G','H','I']

A0 = [5, 6, 7, 9, 8, 1, 2, 3, 4]
A1 = [4, 3, 2, 1, 5, 6, 7, 9, 8]
A2 = [8, 9, 7, 6, 4, 5, 3, 2, 1]

# Собираем все подстановки в список
subs = [A0, A1, A2]

# Находим обратные подстановки
inverse_subs = []
for sub in subs:
    inverted = [0]*9 # список из 9 элементов для хранения обратной подстановки
    for i, v in enumerate(sub): 
        inverted[v-1] = i+1
    inverse_subs.append(inverted)

cipher = "IACHG"
result = ""

# Цикл дешифрования
# i - индекс, ch - символ ; enumerate
for i, ch in enumerate(cipher):
    # Находим номер буквы в алфавите (1..9)
    idx = alphabet.index(ch) + 1

    
    # Номер позиции i считается с 1 поэтому используем (i+1) % 3
    sub_number = (i + 1) % 3
    
    # Находим соответствующую обратную подстановку
    # idx-1 — потому что индексация списков с 0
    plain_idx = inverse_subs[sub_number][idx - 1]
    
    # Преобразуем номер обратно в букву
    result += alphabet[plain_idx - 1]

print(result)