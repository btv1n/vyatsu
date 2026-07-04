from math import sqrt, pow, gcd


def fermat(n: int):
    assert n > 1
    if n % 2 == 0:
        return 2, n // 2
    x = int(sqrt(n))
    y = 0
    r = pow(x, 2) - pow(y, 2) - n
    while r != 0:
        if r > 0:
            y += 1
        else:
            x += 1
        r = pow(x, 2) - pow(y, 2) - n

    return x - y, x + y


def sherman_leman(n: int):
    if n == 6:
        return 2, 3
    if n % 2 == 0:
        return 2, n // 2
    if n < 8:
        return 1, n        

    for a in range(2, int(pow(n, 1 / 3)) + 1):
        if n % a == 0:
            return a, n // a

    for k in range(1, int(pow(n, 1 / 3)) + 1):
        for d in range(int((pow(n, 1 / 6) / 4 / sqrt(k))) + 2):
            A = int(sqrt(4 * k * n)) + d
            sq = pow(A, 2) - 4 * k * n
            if not _is_2_power(sq):
                continue
            B = int(sqrt(sq))
            d = gcd(A - B, n)
            if d < n:
                return d, n // d
    return 1, n


def _is_2_power(x: int) -> bool:
    if x < 0:
        return False

    if pow(round(sqrt(x)), 2) == x:
        return True
    return False


def pollard(n: int):
    assert n > 0
    f = lambda x: int(pow(x, 2) + 1) % n
    x = [2]

    for i in range(1, n):
        x.append(f(x[-1]))
        for j in range(i + 1):
            for k in range(j):
                d = gcd(x[j] - x[k], n)
                if d > 1:
                    return d, n // d


def factorize(n: int):
    factors = {}
    
    def f(n):
        a, b = sherman_leman(n)
        if b == 1:
            a, b = b, a
        if a == 1:
            factors[b] = factors.get(b, 0) + 1
        else:
            f(a)
            f(b)

    f(n)
    return factors