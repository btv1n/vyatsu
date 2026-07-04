from rsa import *
from hash import *
from typing import Callable


def get_hash(document: str, n: int) -> int:
    data = list(document.encode())
    #return hash_gamma(data, 1, 3, 5, n)
    return hash_sum(data, 300)


p = 23
q = 101


def sign(
    private_key: tuple[int, int], document: str, hash_function: Callable[[str], int]
) -> int:
    hash = hash_function(document)
    s = encode(private_key, [hash])[0]
    return s


def check_sign(
    public_key: tuple[int, int],
    document: str,
    signature: int,
    hash_function: Callable[[str], int],
):
    hash = hash_function(document)
    hs = decode(public_key, [signature])[0]
    return hs == hash


public_key, private_key = get_keys(p, q)
(n, e), (_, d) = public_key, private_key
print("public_key ", public_key)
print("private_key", private_key)

hash_function = lambda x: get_hash(x, n)

original_document = "Si vis pacem, para bellum"
print("Документ:", original_document, sep="\n")
s = sign(private_key, original_document, hash_function)
print("Подпись:", s)


print()

#document = original_document
document = "Si vsi pacem, para bellum"

print("Полученный документ:", document, sep="\n")
if check_sign(public_key, document, s, hash_function):
    print("Подпись совпадает")
else:
    print("Подпись не совпадает")
