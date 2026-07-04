from substitution import Substitution


tau = Substitution([0, 3, 7, 5, 2, 6, 4, 1])
print(tau.inverted())


tau = Substitution([0, 3, 6, 2, 5, 7, 1, 4])
sigma = Substitution([0, 6, 2, 7, 5, 4, 3, 1])
print(sigma * tau)
