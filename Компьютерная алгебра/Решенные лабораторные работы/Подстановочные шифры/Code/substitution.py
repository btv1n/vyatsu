from __future__ import annotations
from math import lcm


class Substitution:
    def __init__(self, s: tuple[int] | list[int]):
        self.s = list(s)
        for i in self.s:
            assert i < len(self.s)

    @staticmethod
    def from_string(s: str, sep: str = " ") -> Substitution:
        return Substitution(map(int, s.split(sep)))

    def inverted(self) -> Substitution:
        return self.copy().invert()

    def invert(self) -> Substitution:
        s = self.s.copy()
        for i, e in enumerate(s):
            self.s[e] = i
        return self

    def __pow__(self, n: int):
        if type(n) is float:
            raise TypeError("")
        result = self.copy()
        for i in range(abs(n) - 1):
            result *= self

        if n < 0:
            result.invert()
        return result

    def __ipow__(self, n: int):
        if type(n) is float:
            raise TypeError()
        cp = self.copy()
        for i in range(abs(n) - 1):
            self *= cp

        if n < 0:
            self.invert()
        return self

    def copy(self) -> Substitution:
        return Substitution(self.s.copy())

    def __str__(self):
        return " ".join(map(str, self.s))

    def __mul__(self, other: Substitution):
        result = other.copy()
        result *= self
        return result

    def __imul__(self, other):
        if self is other:
            other = other.copy()

        for i in range(len(self.s)):
            self.s[i] = other[self[i]]

        return self

    def __getitem__(self, key: int):
        return self.s[key]

    def __len__(self):
        return len(self.s)

    def to_cycles(self) -> list[list[int]]:
        visited = [False] * len(self.s)
        cycles = []
        for i in range(len(self)):
            j = i
            cycle = []
            while not visited[j]:
                cycle.append(j)
                visited[j] = True
                j = self[j]
            if len(cycle) > 0:
                cycles.append(cycle)
        
        return cycles
    
    def ord(self) -> int:
        cycles = self.to_cycles()
        return lcm(*map(len, cycles))