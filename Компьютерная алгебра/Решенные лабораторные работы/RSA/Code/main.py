from rsa import *
from hash import *

p = 23
q = 101

public_key, private_key = get_keys(p, q)
print('public_key ', public_key)
print('private_key', private_key)

s = "abcd"
data = list(s.encode())
print(hash_sum(data, 200))

encoded = encode(public_key, data)
print(encoded)

decoded = decode(private_key, encoded)
print(bytes(decoded).decode())
