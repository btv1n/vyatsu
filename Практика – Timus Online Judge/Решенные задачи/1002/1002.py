num = ""
word = ""
n = 0
alphabet = {}

par = []
d = []
dict = []
A = []
q = []
r = []


def init_alphabet():
    alphabet['a'] = '2'
    alphabet['b'] = '2'
    alphabet['c'] = '2'
    alphabet['d'] = '3'
    alphabet['e'] = '3'
    alphabet['f'] = '3'
    alphabet['g'] = '4'
    alphabet['h'] = '4'
    alphabet['k'] = '5'
    alphabet['l'] = '5'
    alphabet['m'] = '6'
    alphabet['n'] = '6'
    alphabet['p'] = '7'
    alphabet['r'] = '7'
    alphabet['s'] = '7'
    alphabet['t'] = '8'
    alphabet['u'] = '8'
    alphabet['v'] = '8'
    alphabet['w'] = '9'
    alphabet['x'] = '9'
    alphabet['y'] = '9'
    alphabet['o'] = '0'
    alphabet['q'] = '0'
    alphabet['z'] = '0'
    alphabet['i'] = '1'
    alphabet['j'] = '1'


def init():
    dict.clear()
    dict.extend([None] * n)

    par.clear()
    par.extend([None] * (len(num) + 1))

    d.clear()
    d.extend([None] * (len(num) + 1))

    A.clear()
    for i in range(len(num) + 1):
        A.append([None] * (len(num) + 1))


def test(k, n):
    for i in range(k, k + len(dict[n])):
        if num[i] != alphabet.get(dict[n][i - k]):
            return
    A[k][k + len(dict[n])] = n + 1


def BFS(u):
    d[u] = 1
    q.append(u)

    while q:
        u = q.pop(0)

        for v in range(len(num) + 1):
            if not d[v] and A[u][v]:
                q.append(v)
                d[v] = d[u] + 1
                par[v] = u


def print_result():
    u = len(num)

    if d[u]:
        while u != 0:
            r.append(u)
            u = par[u]
        while u != len(num):
            v = r.pop()
            print(dict[A[u][v] - 1], end=' ')
            u = v
        print()
    else:
        print("No solution.")


if __name__ == "__main__":
    init_alphabet()

    while True:
        num = input()
        if num == "-1":
            break

        n = int(input())
        init()

        for i in range(1, n + 1):
            word = input()
            dict[i - 1] = word

            for j in range(len(num)):
                if num[j] == alphabet.get(word[0]) and j + len(word) - 1 < len(num):
                    test(j, i - 1)

        BFS(0)
        print_result()
