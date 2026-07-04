from cesar import encode, decode

alphabet = {}

for i in range(ord('а'), ord('я') + 1):
    alphabet[chr(i)] = i - ord('а')
    alphabet[i - ord('а')] = chr(i)



msg = 'Привет мир'
key = (13, 10)

encoded = encode(msg, alphabet, key)
decoded = decode(encoded, alphabet, key)

print(encoded)
print(decoded)