from cesar import encode, decode

alphabet = {}

for i in range(ord("a"), ord("z") + 1):
    alphabet[chr(i)] = i - ord("a") + 1
    alphabet[i - ord("a") + 1] = chr(i)

alphabet[' '] = 0
alphabet[0] = ' '


msg = "tmuxa"
key = (8, 3)

decoded = decode(msg, alphabet, key)

print(decoded)
