from viginere import *
from substitution import Substitution


msg = "Привет мир"

alphabet = {}

for i in range(ord('а'), ord('я') + 1):
    alphabet[chr(i)] = i - ord('а')
    alphabet[i - ord('а')] = chr(i)


key = "ключ"

n = len(alphabet) // 2
s1 = [i for i in range(n)]
s2 = [i + 1 for i in range(n)]

s1[0] = 1
s1[1] = 0

s2[-1] = 0

key = [Substitution(s1), Substitution(s2)]

s = encode(msg, alphabet, key)
print(s)

print(decode(s, alphabet, key))
