from math import gcd


def encode(msg: str, alphabet: dict, key):
    a, b = key
    n = len(alphabet) // 2

    if gcd(a, n) > 1:
        return
    msg = list(msg)
    for i in range(len(msg)):
        x = alphabet.get(msg[i].lower(), -1)
        if x >= 0:
            msg[i] = alphabet[(a * x + b) % n]

    return "".join(msg)


def decode(msg: str, alphabet: dict, key):
    a, b = key
    n = len(alphabet) // 2
    a %= n

    a_inv = invert(a, n)
    print("a^-1 =", a_inv)

    if gcd(a, n) > 1:
        return
    msg = list(msg)
    for i in range(len(msg)):
        x = alphabet.get(msg[i].lower(), -1)
        if x >= 0:
            msg[i] = alphabet[a_inv * (x - b) % n]

    return "".join(msg)


def invert(a, n):
    print(a, n)
    if a == 1:
        return 1
    return int((1 - invert(n % a, a) * n) / a + n)
