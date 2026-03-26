import random
import os

class CreateAndFillFile:
    def create_and_fill_file(self, filename, num_numbers, lower_bound, upper_bound):

        # создаёт папку data если её нет
        os.makedirs(os.path.dirname(filename), exist_ok=True) # PyInstaller

        # открывает файл на запись (если файл не существует, он будет создан)
        with open(filename, "w") as f:
            for i in range(num_numbers - 1):
                # Генерирует случайное число в заданном диапазоне
                number = random.randint(lower_bound, upper_bound)
                f.write(f"{number}\n")

            # добавляем последнее число без пробела
            number = random.randint(lower_bound, upper_bound)
            f.write(f"{number}")
