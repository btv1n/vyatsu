from __future__ import annotations # позволяет использовать ссылки на сам класс в самом классе
from math import lcm               # наименьшее общее кратное


class Substitution:
    # конструктор принимает кортеж или список чисел и сохраняет его как список self.s
    def __init__(self, s: tuple[int] | list[int]):
        self.s = list(s)
        for i in self.s:
            assert 0 < i and i <= len(self.s) # проверка все элементы от 1 до len(list)


    # создает подстановку из строки, где числа разделены пробелом: "1 3 2" -> [1, 3, 2]
    # static чтобы не создавать пустую подстановку
    @staticmethod
    def from_string(s: str, sep: str = " ") -> Substitution:
        return Substitution(map(int, s.split(sep)))
    

    # возвращает обратную подстановку (копия подстановки)
    def inverted(self) -> Substitution:
        return self.copy().invert()


    # возвращает обратную подстановку, инвертирует подстановку
    def invert(self) -> Substitution:
        s = self.s.copy()
        for i, e in enumerate(s):
            self.s[e - 1] = i + 1
        return self # возвращает сам объект
    

    # **
    # переопределение оператора возведение в степень -> означает применить подстановку 3 раза подряд
    def __pow__(self, n: int):
        if type(n) is float:
            raise TypeError("") # ошибка для не целых чисел
        
        # создается копия и она умносажется сама на себя n-1 раз
        result = self.copy()
        for i in range(abs(n) - 1):
            result *= self

        # если степень отрицательная - берётся обратная подстановка
        if n < 0:
            result.invert()
        return result


    # **=
    # возвести в степень и записать в ту же переменную, возвращает сам объект вместо копии, аналогично **
    def __ipow__(self, n: int):
        if type(n) is float:
            raise TypeError()
        copy = self.copy()
        for i in range(abs(n) - 1):
            self *= copy

        if n < 0:
            self.invert()
        return self


    # создает копию - т.е новый список с теми же числами, но в памяти - это уже другой объект
    # не указывает на один и тот же объект как при использовании =
    def copy(self) -> Substitution:
        return Substitution(self.s.copy())


    # определяет, как подстановка выводится (print)
    def __str__(self):
        return " ".join(map(str, self.s))


    # *
    # переопределение оператора * - умножение подстановок (композиция)
    # для каждого элемента пересчитывает результат подстановки и возвращает новую
    # a.__mul__(b) - сначала применяется b, потом подстановка a. Композиции подстановок операций идут справа налево
    def __mul__(self, other: Substitution):
        result = self.copy()
        for i in range(len(result)):
            # индекс с нуля, в подстановке числа начинаются 1, поэтому result[i]-1
            result.s[i] = other[result[i] - 1]
        return result

    # позволяет обращаться к элементам как к списку, σ[0] - вернет первый элемент
    def __getitem__(self, key: int):
        return self.s[key]


    # позволяет использовать len() для подстановок
    def __len__(self):
        return len(self.s)


    # метод разложение постановки на циклы
    def to_cycles(self) -> list[list[int]]:
        visited = [False] * len(self.s) # обработанные элементы
        cycles = [] # список найденных циклов
        # для каждого элемента строится цепочка переходов, пока не вернемся к началу, таким образом формируются циклы подстановки
        for i in range(len(self)):
            j = i
            cycle = []
            while not visited[j]:
                cycle.append(j + 1)
                visited[j] = True
                j = self[j] - 1
            if len(cycle) > 0:
                cycles.append(cycle)
        
        return cycles
    

    # ord() находит порядок подстановки - наименьшее число, степень, в которую нужно возвести подстановку, чтобы получить тождественную (-1)
    # * - распаковка
    # cчитается как НОК длин всех циклов
    def ord(self) -> int:
        cycles = self.to_cycles()
        return lcm(*map(len, cycles)) 