# Остатки и модули
c = [2, 2, 4, 5]
m = [3, 5, 11, 8]

# M = m1 * ... * mn
M = 1
for mod in m:
    M *= mod

print("M =", M)

# Вычисляет Mi: M/mi
Mi = [M // mod for mod in m]
print("Mi =", Mi)

# Функция поиска обратного элемента yi -> x: Mi * yi == 1 (mod m)
def mod_inverse(a, m):
    for x in range(1, m):
        if (a * x) % m == 1:
            return x

# Находим yi
yi = []
for i in range(len(m)):
    inverse = mod_inverse(Mi[i], m[i])
    yi.append(inverse)
print("yi =", yi)  
# yi[0] = обратный к 440 по модулю 3 = 2

# Sum: MiyiCi
x0 = sum(Mi[i] * yi[i] * c[i] for i in range(len(m))) % M

print("x0 =", x0)