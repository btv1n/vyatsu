from viginere import *
from substitution import Substitution


msg = "Привет мир"

alphabet = {
    "a": 0,
    "b": 1,
    "c": 2,
    "d": 3,
    "e": 4,
    "f": 5,
    "g": 6,
    "h": 7,
    "i": 8,
    0: "a",
    1: "b",
    2: "c",
    3: "d",
    4: "e",
    5: "f",
    6: "g",
    7: "h",
    8: "i",
}

n = len(alphabet) // 2
s1 = [i for i in range(n)]
s2 = [i for i in range(n)]

s1[0] = 1
s1[1] = 0

s2[1] = 2
s2[2] = 1

key = [
    Substitution([4, 5, 6, 8, 7, 0, 1, 2, 3]),
    Substitution([3, 2, 1, 0, 4, 5, 6, 8, 7]),
    Substitution([7, 8, 6, 5, 3, 4, 2, 1, 0]),
]

msg = "IACHG"
decoded = decode(msg, alphabet, key)
print(decoded)
print(encode(decoded, alphabet, key))