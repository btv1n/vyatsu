from itertools import cycle
from substitution import Substitution

def encode(msg: str, alphabet: dict, key: str|list[Substitution]):
    if type(key) is str:
        key_type = str 
    elif type(key) is list and len(key) > 0 and type(key[0]) is Substitution:
        key_type = Substitution
    else:
        raise TypeError(type(key))
    
    key = cycle(key)
    n = len(alphabet) // 2
    msg = list(msg)
    
    if key_type is str:
        for i in range(len(msg)):
            x = alphabet.get(msg[i].lower(), -1)
            if x >= 0:
                k = alphabet.get(next(key))
                msg[i] = alphabet[(x + k) % n]
    elif key_type is Substitution:
        for i in range(len(msg)):
            x = alphabet.get(msg[i].lower(), -1)
            if x >= 0:
                msg[i] = alphabet[next(key)[x]]

    return "".join(msg)


def decode(msg: str, alphabet: dict, key: str|list[Substitution]):
    if type(key) is str:
        key_type = str 
    elif type(key) is list and len(key) > 0 and type(key[0]) is Substitution:
        key_type = Substitution
        key = list(map(lambda s: s.inverted(), key))
        print(list(map(str,key)))
    else:
        raise TypeError(type(key))
    
    key = cycle(key)
    n = len(alphabet) // 2
    msg = list(msg)
    
    if key_type is str:
        for i in range(len(msg)):
            x = alphabet.get(msg[i].lower(), -1)
            if x >= 0:
                k = alphabet.get(next(key))
                msg[i] = alphabet[(x - k) % n]
    elif key_type is Substitution:
        for i in range(len(msg)):
            x = alphabet.get(msg[i].lower(), -1)
            if x >= 0:
                msg[i] = alphabet[next(key)[x]]

    return "".join(msg)