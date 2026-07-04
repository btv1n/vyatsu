from lj import legendre, jacobi
from tabulate import tabulate

m = 11
rows = []
for x in range(1, 10 + 1):
    rows.append((x, legendre(x, m), jacobi(x, m)))

print("\n\n")
print(f"m = {m}")
print(tabulate(rows, headers=["x", "legendre", "jacobi"]))
print("\n\n")

print(jacobi(21, 55))