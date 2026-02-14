def inverse_from_list(permutation_list):
    '''
    permutation_list[1-1=0] = 3
    inverted[3-1=2] = 1
    3 -> 1 (по индексу 3 стоит число 1)
    ...
    '''
    n = len(permutation_list) # кол-во элементов
    inverted = [0] * n # список длины n
    for i in range(1, n + 1):
        inverted[permutation_list[i - 1] - 1] = i # permutation_list[i-1] = куда переходит i
    return inverted

permutation_list = [3, 7, 5, 2, 6, 4, 1]
inverted_list = inverse_from_list(permutation_list)
print("Обратная подстановка:", inverted_list)