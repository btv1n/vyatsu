import sys  # Для получения имя файла из командной строки


EPS = 0.0000001


# Включает UTF-8, чтобы напечатать символ ∞
if hasattr(sys.stdout, "reconfigure"):  # Может ли консоль изменить кодировку
    sys.stdout.reconfigure(encoding="utf-8")


def read_task(filename):  
    """
    Читает задачу линейного программирования из текстового файла

    Параметр filename - путь к файлу задачи
    Возвращает direction (min или max), function - коэффициенты целевой функции
    и restrictions - список ограничений
    """
    file = open(filename, "r", encoding="utf-8")
    lines = []  # Непустые строки файла

    for line in file:  # Читаем файл построчно
        if line.strip() != "":  # Проверяем, что строка не пустая
            lines.append(line.split())  # Делим строку на слова и числа через пробел

    file.close()

    if len(lines) < 3:  # Проверяем, есть ли целевая функция и хотя бы одно ограничение
        raise ValueError("В файле недостаточно строк.")

    direction = lines[0][0].lower()  # Направление оптимизации из первой строки

    if direction not in ("min", "max"):
        raise ValueError("В первой строке должно быть слово min или max.")

    function = []  # Коэффициенты целевой функции

    for value in lines[0][1:]:  # Берем все числа после слова min или max
        function.append(float(value))  # Превращаем текст в число и добавляем в список

    restrictions_count = int(lines[1][0])  # Читаем количество ограничений из второй строки

    if len(lines) != restrictions_count + 2:  # Сверяем заявленное и реальное количество строк
        raise ValueError("Количество ограничений указано неверно.")

    restrictions = []  # Все ограничения задачи

    for line in lines[2:]:  # Перебираем строки с ограничениями
        if len(line) != len(function) + 2:  # Проверяем число коэффициентов свободного члена
            raise ValueError("В ограничении неверное количество чисел.")

        sign = line[-2]  # Берем знак ограничения: <=, >= или =

        if sign != "<=" and sign != ">=" and sign != "=":  # Проверяем правильность знака
            raise ValueError("В ограничении нужен знак <=, >= или =.")

        coefficients = []  # Коэффициенты одного ограничения

        for value in line[:-2]:  # Берем все числа до знака ограничения
            coefficients.append(float(value))  # Превращаем текст в число

        free_member = float(line[-1])  # Берем свободный член после знака
        restrictions.append([coefficients, sign, free_member])  # Сохраняем готовое ограничение

    return direction, function, restrictions


def add_column(table):
    """
    Добавляет во все строки симплекс-таблицы один нулевой столбец. Т.е. добавляет новый столбец перменной в таблицу

    Параметр table - список строк симплекс-таблицы
    """
    for row in table:  # Проходим по всем строкам таблицы
        row.insert(-1, 0.0)  # Вставляем ноль перед свободным членом b


def create_start_table(function, restrictions):
    """
    Строит начальную симплекс-таблицу для первого этапа двухэтапного метода
    Добавляет дополнительные s и искусственные a переменные, создает начальный базис

    Параметр function - список коэффициентов исходной целевой функции F
    Параметр restrictions - список ограничений задачи
    Возвращает table - таблицу ограничений, basis - номера базисных столбцов,
    artificial_columns - номера столбцов искусственных переменных
    """
    table = []  # Строки ограничений симплекс-таблицы
    basis = []  # Номера базисных столбцов
    artificial_columns = []  # Номера искусственных переменных
    original_count = len(function)  # Запоминаем количество исходных переменных x
    columns_count = original_count  # Пока в таблице есть только переменные x

    for restriction in restrictions:  # Обрабатываем ограничения
        coefficients = restriction[0][:]  # Создаем копию коэффициентов ограничения
        sign = restriction[1]  # Берем знак ограничения
        free_member = restriction[2]  # Берем свободный член ограничения

        if free_member < 0:  # Если свободный член отрицательный, меняем знак всей строки
            free_member = -free_member  # Делаем свободный член положительным

            for index in range(len(coefficients)):  # Проходим по коэффициентам ограничения
                coefficients[index] = -coefficients[index]  # Меняем знак каждого коэффициента

            if sign == "<=":
                sign = ">="  # после умножения на -1
            elif sign == ">=":
                sign = "<="

        row = coefficients[:]  # Строим новую строку с коэффициентов x
        added_before = columns_count - original_count  # Считаем переменные, добавленные раньше

        for unused in range(added_before):  # Для старых добавленных переменных ставим нули
            row.append(0.0)

        row.append(free_member)  # Добавляем свободный член в конец строки
        table.append(row)

        if sign == "<=":  # Обрабатываем ограничение вида a*x <= b.
            add_column(table)  # Добавляем дополнительную переменную s
            table[-1][-2] = 1.0  # В текущей строке ее коэффициент равен +1
            basis.append(columns_count)  # Эта s сразу является базисной переменной
            columns_count += 1

        elif sign == ">=":  # Обрабатываем ограничение вида a*x >= b
            add_column(table)  # Добавляем избыточную переменную s
            table[-1][-2] = -1.0  # В текущей строке коэффициент s равен -1
            columns_count += 1

            add_column(table)  # Добавляем искусственную переменную a
            table[-1][-2] = 1.0  # В текущей строке коэффициент a равен +1
            basis.append(columns_count)  # Искусственная переменная входит в начальный базис
            artificial_columns.append(columns_count)  # Запоминаем номер искусственного столбца
            columns_count += 1

        else:  # Обрабатываем ограничение вида a*x = b
            add_column(table)  # Добавляем искусственную переменную a
            table[-1][-2] = 1.0  # В текущей строке коэффициент a равен +1
            basis.append(columns_count)  # Искусственная переменная входит в начальный базис
            artificial_columns.append(columns_count)  # Запоминаем номер искусственного столбца
            columns_count += 1

    return table, basis, artificial_columns


def create_objective(costs, table, basis):
    """
    Создает строку целевой функции в каноническом виде
    Для этого исключает из нее все базисные переменные

    Параметр costs - коэффициенты целевой функции
    Параметр table - текущая симплекс-таблица ограничений
    Параметр basis - номера базисных переменных в строках table
    Возвращает objective - строку целевой функции в каноническом виде
    """
    objective = costs[:]  # Создаем копию коэффициентов целевой функции
    objective.append(0.0)  # Добавляем свободный член целевой функции

    for row_number in range(len(table)):  # Проходим по всем базисным строкам
        basic_column = basis[row_number]  # Находим столбец базисной переменной
        coefficient = objective[basic_column]  # Берем ее коэффициент

        for column in range(len(objective)):  # Проходим по всем элементам строки
            objective[column] -= coefficient * table[row_number][column]  # Исключаем базисную переменную

    return objective


def find_pivot_column(objective):
    """
    Ищет самый отрицательный коэффициент в строке целевой функции
    Т.е. ищет ведущий столбец

    Параметр objective - строка текущей целевой функции F или F1
    Возвращает номер ведущего столбца или -1, если отрицательных коэффициентов нет
    
    * Pivot - это ведущий элемент (разрешающий элемент)
    """
    pivot_column = -1  # -1 означает, что отрицательного коэффициента пока нет
    minimum = -EPS  # Текущий минимальный коэффициент

    for column in range(len(objective) - 1):  # Последний элемент - свободный член, его не проверяем
        if objective[column] < minimum:  # Если нашли более отрицательный коэффициент запоминаем его значение
            minimum = objective[column]
            pivot_column = column  # Запоминаем номер его столбца

    return pivot_column


def find_pivot_row(table, pivot_column):
    """
    Выбирает ведущую строку по минимальному отношению: b / разрещающий столбец

    Параметр table - текущая таблица ограничений
    Параметр pivot_column - номер ранее найденного ведущего столбца
    Возвращает номер ведущей строки или -1, если допустимой строки нет
    """
    pivot_row = -1  # -1 означает, что подходящей строки пока нет
    best_ratio = None  # Минимальное отношение: b / разрещающий столбец

    for row_number in range(len(table)):  # Перебираем строки таблицы
        coefficient = table[row_number][pivot_column]  # Берем элемент ведущего столбца

        if coefficient > EPS:  # Делить разрешается только на положительный элемент
            ratio = table[row_number][-1] / coefficient  # Находим отношение свободного члена к элементу

            if best_ratio is None or ratio < best_ratio:  # Если это первое или лучшее отношение, запоминаем его значение
                best_ratio = ratio
                pivot_row = row_number  # Запоминаем номер строки

    return pivot_row


def change_table(table, basis, pivot_row, pivot_column):
    """
    Выполняет один шаг метода Гаусса (преобразования Гаусса) в симплекс-таблице
    Нормирует ведущую строку, обнуляет ведущий столбец и обновляет базис.

    Параметр table - текущая таблица ограничений
    Параметр basis - номера базисных столбцов
    Параметр pivot_row - номер ведущей строки
    Параметр pivot_column - номер ведущего столбца
    """
    pivot_element = table[pivot_row][pivot_column]  # Находим ведущий элемент

    for column in range(len(table[pivot_row])):  # Проходим по элементам ведущей строки
        table[pivot_row][column] /= pivot_element  # Делим строку на ведущий элемент

    for row_number in range(len(table)):  # Проходим по всем строкам таблицы
        if row_number != pivot_row:  # Ведущую строку повторно не меняем
            factor = table[row_number][pivot_column]  # Берем элемент, который нужно обнулить

            for column in range(len(table[row_number])):  # Проходим по элементам текущей строки
                table[row_number][column] -= factor * table[pivot_row][column]  # Обнуляем ведущий столбец

    basis[pivot_row] = pivot_column  # Меняем базисную переменную в ведущей строке


def change_objective(objective, table, pivot_row, pivot_column):
    """
    Обнуляет ведущий столбец в строке целевой функции после шага Гаусса

    Параметр objective - строка F или F1, которую нужно изменить
    Параметр table - уже преобразованная симплекс-таблица
    Параметр pivot_row - номер ведущей строки
    Параметр pivot_column - номер ведущего столбца
    """
    factor = objective[pivot_column]  # Берем коэффициент ведущего столбца

    for column in range(len(objective)):  # Проходим по всем элементам строки
        objective[column] -= factor * table[pivot_row][column]  # Обнуляем ведущий столбец


def simplex_stage(table, objective, basis):
    """
    Выполняет интерации симплекс-метода до завершения одного этапа
    Используется для вспомогательной функции F1 и исходной функции F

    Параметр table - таблица ограничений
    Параметр objective - строка целевой функции этого этапа
    Параметр basis - номера базисных переменных
    Возвращает "optimal" при найденном минимуме или "unbounded" при неограниченности
    """
    while True:
        pivot_column = find_pivot_column(objective)  # Находим ведущий столбец

        if pivot_column == -1:  # Если отрицательных коэффициентов нет, минимум для текущего этапа найден
            return "optimal"

        pivot_row = find_pivot_row(table, pivot_column)  # Находим ведущую строку

        if pivot_row == -1:  # Если положительных элементов в столбце нет, целевая функция не ограничена
            return "unbounded"

        change_table(table, basis, pivot_row, pivot_column)  # Делаем шаг Гаусса для ограничений
        change_objective(objective, table, pivot_row, pivot_column)  # Обновляем целевую строку


def remove_artificial_variables(table, basis, artificial_columns):
    """
    Удаляет искусственные переменные после успешно завершенного первого этапа
    При необходимости заменяет искусственную базисную переменную обычной

    Параметр table - таблица ограничений после первого этапа
    Параметр basis - номера базисных столбцов
    Параметр artificial_columns - номера столбцов искусственных переменных
    """
    row_number = 0  # Начинаем с первой строки таблицы

    while row_number < len(table):  # Проверяем строки, пока они не закончатся
        if basis[row_number] not in artificial_columns:  # Если в строке уже обычная базисная переменная, переходим к следующей строке
            row_number += 1  
            continue  # Остальное пропускаем в текущей итерации

        replacement_column = -1  # Пока не нашли столбец для замены искусственной переменной

        for column in range(len(table[row_number]) - 1):  # Перебираем все столбцы, кроме свободного члена
            if column not in artificial_columns:  # Искусственный столбец не подходит для замены
                if abs(table[row_number][column]) > EPS:  # Ненулевой обычный столбец подходит
                    replacement_column = column  # Запоминаем его номер
                    break

        if replacement_column == -1:  # Если обычного ненулевого столбца нет
            table.pop(row_number)  # Yдаляем лишнюю строку вида 0 = 0
            basis.pop(row_number)  # Удаляем соответствующую запись о базисе
        else:  # Если обычный столбец найден, выводим искусственную переменную из базиса
            change_table(table, basis, row_number, replacement_column)
            row_number += 1  # Переходим к следующей строке

    artificial_columns.sort(reverse=True)  # Сортируем номера столбцов от большего к меньшему

    for column in artificial_columns:  # Удаляем искусственные столбцы по одному
        for row in table:  # Проходим по всем строкам таблицы
            row.pop(column)  # Удаляем искусственный столбец

        for row_number in range(len(basis)):  # Исправляем номера базисных столбцов
            if basis[row_number] > column:  # Если базисный столбец был правее удаленного, его номер уменьшается на 1
                basis[row_number] -= 1


def solve(function, restrictions):
    """
    Полностью решает задачу двухэтапным симплекс-методом
    Выполняет первый этап, проверяет ограничения и выполняет второй этап

    Параметр function - коэффициенты исходной целевой функции Fmin
    Параметр restrictions - список ограничений исходной задачи
    Возвращает [minimum, point] и "optimal", либо None и статус "infeasible" или "unbounded"
    """
    table, basis, artificial_columns = create_start_table(function, restrictions)  # Создаем начальную таблицу

    first_costs = []  # Коэффициенты вспомогательной функции F1
    columns_count = len(table[0]) - 1  # Считаем все столбцы переменных

    for column in range(columns_count):  # Назначаем коэффициенты F1
        if column in artificial_columns:  # Искусственная переменная входит в F1, ее коэффициент равен 1
            first_costs.append(1.0)
        else:  # Обычная переменная не входит в F1, ее коэффициент равен 0
            first_costs.append(0.0)

    first_objective = create_objective(first_costs, table, basis)  # Строим строку F1
    first_status = simplex_stage(table, first_objective, basis)  # Полностью выполняем первый этап
    first_minimum = -first_objective[-1]  # Находим минимум вспомогательной функции

    if first_status == "unbounded" or first_minimum > EPS:  # Если F1 не равна нулю, исходная система ограничений противоречива
        return None, "infeasible"

    remove_artificial_variables(table, basis, artificial_columns)  # Убираем искусственные переменные

    second_costs = function[:]  # Копируем коэффициенты исходной целевой функции

    if len(table) > 0:  # Если после удаления остались ограничения
        all_columns_count = len(table[0]) - 1  # Считаем все оставшиеся столбцы переменных
        extra_count = all_columns_count - len(function)  # Считаем дополнительные и избыточные переменные

        for unused in range(extra_count):  # Для каждой такой переменной добавляем нулевой коэффициент в исходную функцию
            second_costs.append(0.0)

        second_objective = create_objective(second_costs, table, basis)  # Строим строку исходной функции F
    else:  # Если все ограничения были лишними, копируем коэффициенты исходной функции
        second_objective = second_costs[:]
        second_objective.append(0.0)  # Добавляем свободный член F

    second_status = simplex_stage(table, second_objective, basis)  # Полностью выполняем второй этап

    if second_status == "unbounded":  # Если минимум можно уменьшать бесконечно, сообщаем о неограниченности функции
        return None, "unbounded"

    point = []  # Точка минимума по исходным переменным x

    for unused in range(len(function)):  # Все небазисные переменные равны нулю
        point.append(0.0)  # Сначала записываем нули для всех x

    for row_number in range(len(table)):  # Перебираем базисные строки
        basic_column = basis[row_number]  # Берем столбец базисной переменной

        if basic_column < len(function):  # Если это исходная переменная x, записываем ее значение из свободного члена
            point[basic_column] = table[row_number][-1]

    minimum = -second_objective[-1]  # Находим значение Fmin
    return [minimum, point], "optimal"  # Возвращаем минимум и точку, где он достигается


def number_to_text(number):  # Эта функция красиво печатает вещественное число.
    """
    Преобразует число float в удобную текстовую запись.

    Параметр number — число, которое нужно напечатать.
    Возвращает строку без лишнего .0 у целых чисел.
    """
    if abs(number) < EPS:  # Если число почти ноль,
        number = 0.0  # считаем его обычным нулем.

    if abs(number - round(number)) < EPS:  # Если число почти целое,
        return str(round(number))  # печатаем его без .0.

    return f"{number:.6f}"  # В остальных случаях печатаем шесть знаков после запятой.


def main():
    """
    Получает имя входного файла, запускает решение задачи и печатает результат
    """
    if len(sys.argv) != 2:  # Проверяем, передал ли пользователь один файл
        print("Запуск: python main.py <файл_с_задачей.txt>")
        return

    try:  # Пытаемся прочитать и решить задачу
        direction, function, restrictions = read_task(sys.argv[1])  # Читаем задачу из указанного файла

        # Симплекс-метод в этой реализации ищет минимум. Максимум F ищем как
        # минимум -F, но в ответе возвращаем исходное значение F.
        internal_function = function[:]

        if direction == "max":
            internal_function = [-coefficient for coefficient in function]

        result, status = solve(internal_function, restrictions)  # Решаем задачу двухэтапным методом
    except (OSError, ValueError) as error:  # Если файл или формат неверны
        print("Ошибка:", error)
        return

    if status == "infeasible":  # Если система ограничений противоречива
        print("Решений нет. Ограничения задачи противоречивы.")
    elif status == "unbounded":  # Если целевая функция не ограничена
        if direction == "min":
            print("Целевая функция не ограничена. Fmin = -∞")
        else:
            print("Целевая функция не ограничена. Fmax = ∞")
    else:  # Иначе найден оптимум
        value = result[0]  # Берем значение внутренней функции
        point = result[1]  # Берем оптимальную точку

        if direction == "max":
            value = -value  # Возвращаем знак исходной целевой функции
            function_name = "Fmax"
            point_name = "Точка максимума"
        else:
            function_name = "Fmin"
            point_name = "Точка минимума"

        print(function_name + " =", number_to_text(value))
        print(point_name + ": (", end="")  # Начинаем печатать координаты точки

        for index in range(len(point)):  # Перебираем координаты точки
            print(number_to_text(point[index]), end="")  # Печатаем очередную координату

            if index != len(point) - 1:  # Если координата не последняя, печатаем запятую
                print(", ", end="")

        print(")")  # Закрываем скобку и переходим на новую строку


if __name__ == "__main__":
    main()





'''
Общая логика работы программы

1. main() получает имя файла задачи из команды запуска и вызывает остальные части программы.

2. read_task() читает целевую функцию Fmin и ограничения из файла .txt.

3. solve() запускает весь двухэтапный симплекс-метод.

4. create_start_table() строит начальную таблицу: добавляет дополнительные переменные s и искусственные переменные a, если они нужны.

5. create_objective() строит строку вспомогательной функции F1 = a1 + a2 + ...

6. simplex_stage() решает первый этап. Внутри него:
   - find_pivot_column() выбирает ведущий столбец;
   - find_pivot_row() выбирает ведущую строку;
   - change_table() выполняет преобразование Гаусса в таблице;
   - change_objective() обновляет строку F1.

7. Если минимум F1 не равен нулю, solve() сообщает: "Решений нет. Ограничения задачи противоречивы".

8. Если F1 = 0, функция remove_artificial_variables() удаляет искусственные переменные.

9. Затем create_objective() создает строку исходной функции F, а simplex_stage() выполняет второй этап теми же действиями.

10. Если на втором этапе нет ведущей строки, выводится: "Целевая функция не ограничена. Fmin = -∞".

11. Если минимум найден, solve() формирует точку минимума и значение Fmin.

12. number_to_text() выводит числа, а main() печатает окончательный ответ.
'''
