import numpy as np

def hash_sum(data: list[int], mod: int) -> int:
    return int(np.sum(data) % mod)

def hash_gamma(data: list[int], t: int, a: int, b: int, mod: int) -> int:
    hash = 0
    for i in range(len(data)):
        hash += data[i] ^ t 
        t = (a * t + b) % mod 
    return hash % mod
    