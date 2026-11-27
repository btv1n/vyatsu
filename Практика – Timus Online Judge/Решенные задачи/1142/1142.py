n = int(input())
MAXN = 12

# Заполнение массивов нулями
A = [[0 for i in range(MAXN)] for i in range(MAXN)]
ans = [0 for i in range(MAXN)]

A[1][1] = 1

# Заполнение массива A, с использованием формулы для нахождения всех отношений порядка
for i in range(1, MAXN - 1):
    for j in range(1, i+1):
        A[i + 1][j] += A[i][j] * j
        A[i + 1][j + 1] += A[i][j] * (j + 1)

for i in range(1, MAXN - 1):
    for j in range(1, i+1):
        ans[i] += A[i][j]

while n != -1:
    print(ans[n])
    n = int(input())
