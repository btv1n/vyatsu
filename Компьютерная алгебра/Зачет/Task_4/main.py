from substitution import Substitution as S

# a. Ввод подстановки;
# print("a. Ввод подстановки")
# user_input_τ = input("Введите подстановку τ через пробел (например: 3 2 1 4): ")
# user_input_τ = S.from_string(user_input_τ)
# user_input_σ = input("Введите подстановку σ через пробел (например: 3 2 1 4): ")
# user_input_σ = S.from_string(user_input_σ)
# print()

τ = S([3,6,2,5,7,1,4]) # tau
σ = S([6,2,7,5,4,3,1]) # sigma

# τ = user_input_τ 
# σ = user_input_σ

# b. Вывод подстановки;
print("b. Вывод подстановки")
print("τ =", τ)
print("σ =", σ)
print()

# c. Нахождение композиции двух подстановок;
print("c. Нахождение композиции двух подстановок")
print("τ * σ =", τ * σ)
print("σ * τ =", σ * τ)
print()