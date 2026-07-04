from math import gcd


# p, q - prime numbers
# returns public_key, private_key
# returns (n, e), (n, d)
def get_keys(p: int, q: int) -> tuple[tuple[int, int], tuple[int, int]]:
    n = p * q
    phi = (p - 1) * (q - 1)

    e = 2
    while gcd(e, phi) > 1:
        e += 1

    gcd_, x, y = euclid_extended(e, phi)
    if gcd_ == 1:
        d = (x % phi + phi) % phi
    else:
        raise Exception("gcd is not 1")

    return (n, e), (n, d)


def encode(public_key: tuple[int, int], data: list[int]) -> list[int]:
    n, e = public_key
    for i in range(len(data)):
        if data[i] >= n:
            raise Exception("n is too small")
        data[i] = (data[i] ** e) % n
    return data


def decode(private_key: tuple[int, int], data: list[int]) -> list[int]:
    n, d = private_key
    for i in range(len(data)):
        data[i] = (data[i] ** d) % n
    return data


def euclid_extended(a: int, b: int):
    if a == 0:
        return b, 0, 1
    gcd, x, y = euclid_extended(b % a, a)
    return gcd, y - (b // a) * x, x
