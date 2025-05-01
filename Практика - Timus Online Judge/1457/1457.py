import sys

n = int(sys.stdin.readline()) # вводим кол-во чисел

p = [int(x) for x in sys.stdin.readline().split()] # вводим сами числа

print(sum(p) / n) # выводим среднее арифметическое
