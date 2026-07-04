from rsa import *
from hash import *

p = 7
q = 5

public_key, private_key = get_keys(p, q)
print('public_key ', public_key)
print('private_key', private_key)

s = [8, 15, 13, 10]

decoded = decode(private_key, s)
for i in decoded:
    c = ord('a') + i
    print(chr(c))
print(decoded)
